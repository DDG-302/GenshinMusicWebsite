using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using genshinwebsite.Models;
using genshinwebsite.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using genshinwebsite.Services;
using Microsoft.AspNetCore.Http.Extensions;

namespace genshinwebsite.Controllers
{
    //[Authorize(Policy = "仅限管理员")]
    //[Authorize(Policy = "仅限God")]
    [Authorize(Roles = "God,Admin")]

    public class AdminController : Controller
    {

        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;
        private readonly ICommentDB<CommentModel, CommentViewModel> _commentDBHelper;
        public AdminController(
            SignInManager<UserModel> signInManager,
            UserManager<UserModel> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration, IMusicDB<MusicModel, MusicViewModel> musicDBHelper, ICommentDB<CommentModel, CommentViewModel> commentDBHelper)

        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _musicDBHelper = musicDBHelper;
            _commentDBHelper = commentDBHelper;
        }

        public IActionResult Index(int page_offset, string username)
        {
            if(!User.IsInRole("Admin") && !User.IsInRole("God"))
            {
                return RedirectToAction("index", "home");
            }
            var page_num = _configuration.GetValue("NumPerPage", 10);
            ViewData["page_offset"] = Math.Max(1, page_offset);
            page_offset = Math.Max(0, page_offset-1);
            var user_count = _userManager.Users.Count();
            if(user_count % page_num != 0)
            {
                ViewData["max_page"] = user_count / page_num + 1;
            }
            else
            {
                ViewData["max_page"] = user_count / page_num;
            }
            if(username == null || username == "")
            {
                return View(_userManager.Users.Skip(page_offset * page_num).Take(page_num).OrderBy(x => x.Id));
            }
            else
            {
                ViewData["username"] = username;
                return View(_userManager.Users.Skip(page_offset * page_num).Take(page_num).OrderBy(x => x.Id).Where(x => x.UserName.Contains(username)));
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("God"))
            {
                RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            var role = await _userManager.GetRolesAsync(user);

            if (role.Count != 0 && (int)Enum.Parse(typeof(Role_type), role[0]) > 0 && User.IsInRole("God") == false)
            {
                return StatusCode(403, "不允许删除拥有高级权限的用户");
            }
            else if (role.Count != 0 && (int)Enum.Parse(typeof(Role_type), role[0]) == 2)
            {
                return StatusCode(403, "不允许删除拥有God权限的用户");
            }

            
            if(user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "删除出错");
                }
            }
            else
            {
                return StatusCode(410, "目标用户不存在");
            }
            
           
        }
        
        public IActionResult UserManage(int id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("God"))
            {
                RedirectToAction("index", "home");
            }
            var userModel = _userManager.FindByIdAsync(id.ToString()).Result;
            if(userModel == null)
            {
                ModelState.AddModelError(string.Empty, "用户已不存在");
                var empty_view_model = new UserManageViewModel
                {
                    Id = -1,
                    Account = "",
                    Name = "",
                    Role = Role_type.Not_Found
                };
                return View(empty_view_model);
            }
            var role = _userManager.GetRolesAsync(userModel).Result;
            var user_view_model = new UserManageViewModel
            {
                Id = userModel.Id,
                Account = userModel.Email,
                Name = userModel.UserName,
                Role = Role_type.Not_Found
            };
            if (role.Count != 0)
            {
                user_view_model.Role = (Role_type)Enum.Parse(typeof(Role_type), role[0]);
            }
            return View(user_view_model);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userManageViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserManage(UserManageViewModel userManageViewModel)
        {
            if(!User.IsInRole("Admin") && !User.IsInRole("God"))
            {
                return RedirectToAction("home", "index");
            }
            if (!ModelState.IsValid)
            {
                return View("usermanage", userManageViewModel);
            }
            if(userManageViewModel.Role == Role_type.Not_Found)
            {
                userManageViewModel.Role = Role_type.User;
            }

            
            var user = await _userManager.FindByIdAsync(userManageViewModel.Id.ToString());
            var role = _userManager.GetRolesAsync(user).Result;
            
            
            if (role.Count != 0 && (int)Enum.Parse(typeof(Role_type), role[0]) > 0 && User.IsInRole("God") == false)
            {
                ModelState.AddModelError(string.Empty, "不允许修改拥有高级权限的用户");
                return View(userManageViewModel);
            }
            else if(role.Count != 0 && (int)Enum.Parse(typeof(Role_type), role[0]) == 2)
            {
                ModelState.AddModelError(string.Empty, "不允许修改拥有God权限的用户");
                return View(userManageViewModel);
            }

            var result_list = new List<string>();
            ViewData["result"] = result_list;
            if (user != null)
            {
                if(userManageViewModel.Password != null && userManageViewModel.Password.Length > 0)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userManageViewModel.Password);
                }
                
                user.UserName = userManageViewModel.Name;
                user.Email = userManageViewModel.Account;
                user.UploadLimit = userManageViewModel.UploadLimit;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    result_list.Add("用户「基本」信息修改成功");
                    if (!User.IsInRole("God"))
                    {
                        return View(userManageViewModel);
                    }
                    // 判断角色是否已经存在
                    if(role.Count > 0)
                    {
                        if(role.Count == 1)
                        {
                            if(role[0] != userManageViewModel.Role.ToString() || role[0] == "Not_Found")
                            {
                                result = await _userManager.RemoveFromRolesAsync(user, role);// 已经存在的角色就删掉
                                if (!result.Succeeded)
                                {
                                    foreach (var error in result.Errors)
                                    {
                                        ModelState.AddModelError(string.Empty, error.Description);
                                    }
                                    return View(userManageViewModel);
                                }
                                result = await _userManager.AddToRoleAsync(user, userManageViewModel.Role.ToString());
                            }
                        }
                        else
                        {
                            // 不存在则直接添加
                            result = await _userManager.AddToRoleAsync(user, userManageViewModel.Role.ToString());
                        }
                        

                    }



                    if (result.Succeeded)
                    {
                        result_list.Add("用户「角色」信息修改完成");
                        return View(userManageViewModel);
                    }
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "用户已不存在");
            }
            
            return View("usermanage", userManageViewModel);
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManageViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleManage(UserManageViewModel userManageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userManageViewModel);
            }
            if (userManageViewModel.Role == Role_type.Not_Found)
            {
                userManageViewModel.Role = Role_type.User;
            }
            var role = new IdentityRole<int>
            {
                Name = userManageViewModel.Role.ToString()
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(userManageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserRegisterViewModel user_model)
        {
            if (ModelState.IsValid)
            {
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
                    return RedirectToAction("index", "admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(user_model);
        }

        public IActionResult AddUser()
        {
           
            return View();
        }

        [Authorize(Roles = ("Admin, God"))]
        public async Task<IActionResult> MusicManage(int page_offset, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            ViewData["url"] = HttpContext.Request.GetDisplayUrl();
            ViewData["filter_idx"] = (int)select_order;
            if (music_title != null && music_title != string.Empty && music_title != "")
            {
                ViewData["music_title"] = music_title;
            }

            var num_per_page = _configuration.GetValue("NumPerPage", 10);
            page_offset = Math.Max(1, page_offset);

            int item_count = _musicDBHelper.get_item_count(music_title);
            var music_list = await _musicDBHelper.get_music_by_offset(num_per_page, page_offset - 1, music_title, select_order);
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

            return View(music_list.ToList());
        }


        [Authorize(Roles = ("Admin, God"))]
        [Route("Admin/MusicManager/Detail/{muid}")]
        public async Task<IActionResult> MusicManageDetail(int muid)
        {
            var music_model = await _musicDBHelper.get_by_id(muid);
            return View(music_model);
        }

        [Authorize(Roles = ("Admin, God"))]
        public async Task<IActionResult> CommentManage(int page_offset, string comment_context = "", COMMENT_SELECT_ORDER select_order = COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER)
        {
            ViewData["url"] = HttpContext.Request.GetDisplayUrl();
            ViewData["filter_idx"] = (int)select_order;
            var num_per_page = _configuration.GetValue("NumPerPage", 10);
            page_offset = Math.Max(1, page_offset);
            if (comment_context != null && comment_context != string.Empty && comment_context != "")
            {
                ViewData["search_data"] = comment_context;
            }
            int item_count = _commentDBHelper.get_item_count(comment_context);
            var comment_list = await _commentDBHelper.search_comment_list(comment_context, num_per_page, page_offset - 1, select_order);
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

            return View(comment_list.ToList());
        }

        [Authorize(Roles = ("Admin, God"))]
        [Route("Admin/CommentManage/Detail")]
        public async Task<IActionResult> CommentManageDetail(int muid, int uid)
        {
            var comment_view_model = await _commentDBHelper.get_comment_by_uid_muid(uid, muid);

            return View(comment_view_model);
        }
    }
}
