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




namespace genshinwebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly string _data_root;
        private readonly string _img_root;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;
        private readonly IEmailVCodeDB _emailVCodeDB;
        public UserController(
            SignInManager<UserModel> signInManager,
            UserManager<UserModel> userManager,
            IWebHostEnvironment env, 
            IMusicDB<MusicModel, MusicViewModel> musicDBHelper,
            IEmailVCodeDB emailVCodeDB)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _data_root = Path.Combine(env.ContentRootPath, "music_save");
            _musicDBHelper = musicDBHelper;
            _img_root = Path.Combine(env.WebRootPath, "img/musicsheet");
            _emailVCodeDB = emailVCodeDB;
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

        //[HttpPost]
        //public async Task<string> Space([FromForm] IFormFile file)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return string.Empty;
        //    }
        //    string rtn_str = "save ok!!!";


        //    var readstream = new StreamReader(file.OpenReadStream());
        //    //var data = readstream.ReadToEnd();

        //    string json_str = readstream.ReadToEnd();
        //    SaveFileTemplate save_file = null;
        //    try
        //    {
        //        save_file = JsonSerializer.Deserialize<SaveFileTemplate>(json_str);
        //        var music_sheet = save_file.Music_sheet;
        //        // 路径格式为 {用户id}/{乐谱id}/{title}.genmujson(这是为了确保重名乐谱也可以上传)
        //        if (MusicVlidator.from_music_sheet_load_notes_to_seq_list(ref music_sheet, save_file.Beats_per_bar))
        //        {
        //            var user = await _userManager.GetUserAsync(User);
        //            if (user != null)
        //            {
        //                int uid = user.Id;
        //                string user_music_root = Path.Combine(_data_root, uid.ToString());
        //                if (!Directory.Exists(user_music_root))
        //                {
        //                    Directory.CreateDirectory(user_music_root);
        //                }


        //                MusicModel musicModel = new MusicModel()
        //                {
        //                    MusicTitle = save_file.Music_name,
        //                    User_id = uid,
        //                    Abstract_content = "测试乐谱"
        //                };
        //                var result = _musicDBHelper.add_one(musicModel);
        //                if (result != DBOperationResult.OK)
        //                {
        //                    rtn_str = "DB_ERROR";
        //                }
        //                else
        //                {
        //                    // 真正包含文件名的存放路径
        //                    string music_save_file = Path.Combine(user_music_root, musicModel.Id.ToString());
        //                    Directory.CreateDirectory(music_save_file);
        //                    string music_file_save_path = Path.Combine(music_save_file, save_file.Music_name + ".genmujson");
        //                    using (var fileStream = new FileStream(music_file_save_path, FileMode.Create, FileAccess.Write))
        //                    {
        //                        file.CopyTo(fileStream);
        //                    }
        //                    using (var fileStream = new FileStream(_img_root, FileMode.Create, FileAccess.Write))
        //                    {
        //                        file.CopyTo(fileStream);
        //                    }
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string err = e.Message;
        //        rtn_str = "error...";
        //    }
        //    finally
        //    {
        //        readstream.Close();
        //    }
        //    return rtn_str;
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Upload([FromForm] string abs, IFormFile file)
        {
            if (!User.Identity.IsAuthenticated || file == null)
            {
                return string.Empty;
            }
            
            string rtn_str = "save ok!!!";

            
            var readstream = new StreamReader(file.OpenReadStream());
            //var data = readstream.ReadToEnd();

            string json_str = readstream.ReadToEnd();
            SaveFileTemplate save_file = null;
            try
            {
                save_file = JsonSerializer.Deserialize<SaveFileTemplate>(json_str);
                var music_sheet = save_file.Music_sheet;
                // 路径格式为 {用户id}/{乐谱id}/{title}.genmujson(这是为了确保重名乐谱也可以上传)
                if (MusicVlidator.from_music_sheet_load_notes_to_seq_list(ref music_sheet, save_file.Beats_per_bar))
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
                            rtn_str = "DB_ERROR";
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
                            using (var fileStream = new FileStream(Path.Combine(_img_root, file.FileName), FileMode.Create, FileAccess.Write))
                            {
                                file.CopyTo(fileStream);
                            }
                        }

                    }

                }
            }
            catch (Exception e)
            {
                string err = e.Message;
                rtn_str = "error...";
            }
            finally
            {
                readstream.Close();
            }
            return rtn_str;
        }

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
                var result = await _signInManager.PasswordSignInAsync(user, user_model.Password, true, false);
                if (result.Succeeded)
                {
                    //if (_signInManager.IsSignedIn(User))
                    //{
                    //    return Redirect("home/index");
                    //}
                    return RedirectToAction("index", "home");
                }

            }


            // 没找到则返回一个model级错误
            ModelState.AddModelError(string.Empty, "用户名或密码错误");
            return View("login", user_model);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel user_model, string confirm_code)
        {
            if(confirm_code != "任何邪恶终将绳之以法")
            {
                ModelState.AddModelError(string.Empty, "暂时禁止注册");
                return View("register", user_model);
            }
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdatePost(UserLoginViewModel user_model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new UserModel
        //        {
        //            Id = 1,
        //            Email = user_model.Account,
        //            SecurityStamp = "2WX2MOTEBLVWSFZF55GQX5RP63ZHRGVL"

        //        };
        //        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user_model.Password);
        //        var result = await _userManager.UpdateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("index", "home");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View("login", user_model);
        //}
    }
}
