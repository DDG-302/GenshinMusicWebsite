using Microsoft.AspNetCore.Mvc;
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
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;




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
        private readonly IEmailVCodeDB _emailVCodeDB;
        public UserController(
            SignInManager<UserModel> signInManager,
            UserManager<UserModel> userManager,
            IWebHostEnvironment env,
            IMusicDB<MusicModel, MusicViewModel> musicDBHelper,
            IEmailVCodeDB emailVCodeDB, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _data_root = Path.Combine(env.ContentRootPath, "music_save");
            _musicDBHelper = musicDBHelper;
            _img_root = Path.Combine(env.WebRootPath, "img/musicsheet");
            _emailVCodeDB = emailVCodeDB;
            _configuration = configuration;
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

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] string abs, IFormFile file)
        {
            if (!User.Identity.IsAuthenticated || file == null)
            {
                return StatusCode(403, "上传失败：文件为空");
            }
            
            string rtn_str = "上传成功";
            int status_code = 200;
            

            var readstream = new StreamReader(file.OpenReadStream());
            //var data = readstream.ReadToEnd();

            string json_str = readstream.ReadToEnd();
            
            try
            {
                SaveFileTemplate save_file = null;
                save_file = JsonSerializer.Deserialize<SaveFileTemplate>(json_str);
                var music_sheet = save_file.Music_sheet;
                // 路径格式为 {用户id}/{乐谱id}/{title}.genmujson(这是为了确保重名乐谱也可以上传)
                if (MusicValidator.from_music_sheet_load_notes_to_seq_list(ref music_sheet, save_file.Beats_per_bar))
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        int uid = user.Id;
                        string user_music_root = Path.Combine(_data_root, uid.ToString());
                        if (!Directory.Exists(user_music_root))
                        {
                            Directory.CreateDirectory(user_music_root);
                        }


                        MusicModel musicModel = new MusicModel()
                        {
                            MusicTitle = Path.GetFileNameWithoutExtension(file.FileName),
                            User_id = uid,
                            Abstract_content = abs,
                        };

                        StringBuilder sb = new StringBuilder();

                        var result = _musicDBHelper.add_one(musicModel);
                        if (result != DBOperationResult.OK)
                        {
                            rtn_str = "上传失败：数据库错误";
                            status_code = 500;
                        }
                        else
                        {
                            // 真正包含文件名的存放路径
                            string music_save_file = Path.Combine(user_music_root, musicModel.Id.ToString());
                            Directory.CreateDirectory(music_save_file);
                            string music_file_save_path = Path.Combine(music_save_file, file.FileName);
                            using (var fileStream = new FileStream(music_file_save_path, FileMode.Create, FileAccess.Write))
                            {
                                file.CopyTo(fileStream);
                            }
                            //using (var fileStream = new FileStream(Path.Combine(_img_root, file.FileName), FileMode.Create, FileAccess.Write))
                            //{
                            //    file.CopyTo(fileStream);
                            //}
                            
                        }

                    }
                    else
                    {
                        rtn_str = "上传失败：用户不存在";
                        status_code = 400;
                    }
                    

                }
                else
                {
                    rtn_str = "上传失败：乐谱未通过校验，请检查乐谱内容正确";
                    status_code = 500;
                }
            }
            catch (Exception e)
            {
                string err = e.Message;
                rtn_str = "上传失败，服务器程序错误：" + err;
                status_code = 500;
            }
            finally
            {
                readstream.Close();
            }
            return StatusCode(status_code, rtn_str);
        }

        [Route("Space/Setting")]
        [Authorize]
        public IActionResult Setting()
        {
            return View();
        }

        [Route("Space/Setting")]
        [HttpPost]
        [Authorize]
        public IActionResult Setting(UserManageViewModel userManageViewModel)
        {
            if (ModelState.IsValid)
            {
               
                

            }

            return Ok();
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

        [HttpPost]
        [Authorize]
        [Route("DeleteMyMusic")]
        public async Task<IActionResult> DeleteMyMusic(int muid)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _musicDBHelper.delete_one_by_id(muid, user.Id);
            // 路径格式为 {用户id}/{乐谱id}/{title}.genmujson(这是为了确保重名乐谱也可以上传)
            if (result == DBOperationResult.OK)
            {
                string music_sheet_file = user.Id.ToString() + "/" + muid.ToString();
                if (Directory.Exists(music_sheet_file))
                {
                    Directory.Delete(music_sheet_file, true);
                }
                return Ok("删除完成");
            }
            else if(result == DBOperationResult.ERROR)
            {
                return StatusCode(403, "禁止此请求");
            }
            else
            {
                return StatusCode(500, "出错");
            }
            return View();
        }

        [Authorize]
        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Login()
        {
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
               
                var result = await _signInManager.PasswordSignInAsync(user, user_model.Password, false, true);
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
                if(_emailVCodeDB.is_code_verified(user_model.Account, user_model.VCode) == false)
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


    }
}
