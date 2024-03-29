﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Services;
using genshinwebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using genshinwebsite.Controllers.music;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;



namespace genshinwebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly string _data_root;
        private readonly string _img_root;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;
        private readonly IEmailService _EmailService;
        private readonly IEmailVCodeDB _emailVCodeDB;
        public UserController(
            SignInManager<UserModel> signInManager,
            UserManager<UserModel> userManager,
            IWebHostEnvironment env,
            IMusicDB<MusicModel, MusicViewModel> musicDBHelper,
            IEmailVCodeDB emailVCodeDB, IConfiguration configuration, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _data_root = Path.Combine(env.ContentRootPath, "music_save");
            _musicDBHelper = musicDBHelper;
            _img_root = Path.Combine(env.WebRootPath, "img/musicsheet");
            _emailVCodeDB = emailVCodeDB;
            _configuration = configuration;
            _EmailService = emailService;
        }



        /// <summary>
        /// 用户视图控制
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("space");
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult Space()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult test_ip()
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            return StatusCode(200, ip.ToString());
        }

        [Authorize]
        public async Task<IActionResult> Setting()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user is null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
            ChangeUserInfoViewModel changeUserInfoViewModel = new ChangeUserInfoViewModel()
            {
                Uid = user.Id,
                Account = user.Email,
                Name = user.UserName
            };

            return View(changeUserInfoViewModel);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Setting(ChangeUserInfoViewModel changeUserInfoViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            changeUserInfoViewModel.Uid = user.Id;
            changeUserInfoViewModel.Account = user.Email;
            changeUserInfoViewModel.Name = user.UserName;
            if(user is null)
            {
                await _signInManager.SignOutAsync();
                return Redirect("Login");
            }
            if (ModelState.IsValid)
            {
                if(!_emailVCodeDB.is_code_verified(user.Email, changeUserInfoViewModel.VCode))
                {
                    ModelState.AddModelError(string.Empty, "邮箱验证码错误");
                    return View(changeUserInfoViewModel);
                }
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changeUserInfoViewModel.Password);
                var result =  await _userManager.UpdateAsync(user);
                var result_list = new List<string>();
                ViewData["result"] = result_list;
                if (result.Succeeded)
                {
                    result_list.Add("更新完成");
                }
                else
                {
                    result_list.Add("更新失败，服务器错误");
                }
                
                
                return View(changeUserInfoViewModel);

            }
            else
            {
                return View(changeUserInfoViewModel);
            }

            
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [Authorize]
        public IActionResult UploadManager(int page_offset, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            
            ViewData["url"] = HttpContext.Request.GetDisplayUrl();
            ViewData["filter_idx"] = (int)select_order;
            if (music_title != null && music_title != string.Empty && music_title != "")
            {
                ViewData["music_title"] = music_title;
            }
            List<MusicViewModel> musicViewModels = new List<MusicViewModel>();
            var num_per_page = _configuration.GetValue("NumPerPage", 10);
            int item_count;
            var music_list = _musicDBHelper.get_by_uid(int.Parse(_userManager.GetUserId(HttpContext.User)), 
                out item_count,
                num_per_page,
                page_offset - 1,
                select_order).ToList();
            page_offset = Math.Max(1, page_offset);

           
            if (item_count % num_per_page != 0)
            {
                page_offset = Math.Min(item_count / num_per_page + 1, page_offset);
                ViewData["max_page"] = item_count / num_per_page + 1;
            }
            else
            {
                page_offset = Math.Min(item_count / num_per_page, page_offset);
                ViewData["max_page"] = item_count / num_per_page;
            }
            ViewData["page_offset"] = page_offset;


            foreach (var m in music_list)
            {
                MusicViewModel music = new MusicViewModel()
                {
                    Id = m.Id,
                    Uploader = User.Identity.Name,
                    Abstract_content = m.Abstract_content,
                    Datetime = m.Datetime,
                    MusicTitle = m.MusicTitle,
                    View_num = m.View_num,
                    Download_num = m.Download_num
                };
               
                musicViewModels.Add(music);
            }
            return View(musicViewModels);
        }



        [Authorize]
        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Space");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel user_model)
        {

            // 验证modelstate(required是否已经填写)
            if (!ModelState.IsValid)
            {
                return View("login", user_model);
            }


            var user = await _userManager.FindByEmailAsync(user_model.Account);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, user_model.Password, true, true);
                if (result.Succeeded)
                {
                    //if (_signInManager.IsSignedIn(User))
                    //{
                    //    return Redirect("home/index");
                    //}
                    return RedirectToAction("index", "home");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "用户已被锁定，请等待解锁");
                    return View("login", user_model);
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "管理员已禁止该账号登录");
                    return View("login", user_model);
                }

            }


            // 没找到则返回一个model级错误
            ModelState.AddModelError(string.Empty, "用户名或密码错误");
            return View("login", user_model);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel user_model)
        {
            if (ModelState.IsValid)
            {
                var user_check = await _userManager.FindByEmailAsync(user_model.Account);
                if(user_check != null)
                {
                    ModelState.AddModelError(string.Empty, "用户已存在");
                    return View(user_model);
                }
                if (_emailVCodeDB.is_code_verified(user_model.Account, user_model.VCode) == false)
                {
                    ModelState.AddModelError("VCode", "邮箱验证码错误");
                    user_model.VCode = string.Empty;
                    return View("register", user_model);
                }
                var user = new UserModel
                {
                    UserName = user_model.Name,
                    Email = user_model.Account

                };

                var result = await _userManager.CreateAsync(user, user_model.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(_userManager.FindByEmailAsync(user_model.Account).Result, Role_type.User.ToString());

                }
                if (result.Succeeded)
                {

                     var signin_result = await _signInManager.PasswordSignInAsync(user, user_model.Password, true, false);
                    if (signin_result.Succeeded)
                    {
                        return RedirectToAction("index", "home");
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "无法登陆");

                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View("register", user_model);
        }


     

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
          
            return RedirectToAction("index", "home");
            
        }

        [Route("User/UploadManager/{muid:int}")]
        public async Task<IActionResult> UploadManagerDetail(int muid)
        {
            var music = await _musicDBHelper.get_by_id(muid);
            return View(music);
        }


    }
}
