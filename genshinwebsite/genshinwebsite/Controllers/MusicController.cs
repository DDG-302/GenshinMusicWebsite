using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using genshinwebsite.Models;
using genshinwebsite.ViewModels;
using genshinwebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace genshinwebsite.Controllers
{
    public class MusicController : Controller
    {
        private readonly string _data_root;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly ICommentDB<CommentModel, CommentViewModel> _commentDBHelper;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;
        private readonly IConfiguration _configuration;

        public MusicController(IWebHostEnvironment env, IMusicDB<MusicModel, MusicViewModel> musicDBHelper, SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, ICommentDB<CommentModel, CommentViewModel> commentDBHelper, IConfiguration configuration)
        {
            _data_root = Path.Combine(env.ContentRootPath, "music_save");
            _musicDBHelper = musicDBHelper;
            _signInManager = signInManager;
            _userManager = userManager;
            _commentDBHelper = commentDBHelper;
            _configuration = configuration;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete_music(int muid)
        {
            MusicModel music_model = await _musicDBHelper.get_by_id(muid);
            if (music_model == null)
            {
                return Ok();
            }
            string user_music_root = Path.Combine(_data_root, music_model.User_id.ToString());
            string music_save_file = Path.Combine(user_music_root, music_model.Id.ToString());
            string music_file_save_path = Path.Combine(music_save_file, music_model.MusicTitle);
            if (!System.IO.File.Exists(music_file_save_path))
            {
                return Ok();
            }
            try
            {
                System.IO.File.Delete(music_file_save_path);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
    



        }
    
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit_comment(string comment, int muid)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Forbid("用户未登录");
            }
            var user = await _userManager.GetUserAsync(User);
            
            CommentModel commentModel = new CommentModel()
            {
                Uid = user.Id,
                Muid = muid,
                CommentContent = comment,
                UpdateDate = DateTime.Now,
                UploadDate = DateTime.Now,
                UploadIP = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()
            };
            var select_result = await _commentDBHelper.get_comment_by_uid_muid(user.Id, muid);
            DBOperationResult result = DBOperationResult.OK;
            if(select_result == null)
            {
                result = await _commentDBHelper.add_comment(commentModel);
            }
            else
            {
                commentModel.UploadDate = select_result.UploadDate;
                result = await _commentDBHelper.update_comment(commentModel);
            }

            if(result == DBOperationResult.DbUpdateException)
            {
                return StatusCode(500, "评论失败，数据库错误");
            }
            else if(result == DBOperationResult.ERROR)
            {
                return StatusCode(500, "评论失败，服务器错误");
            }
            return Ok("评论成功");
        }

        public async Task<IActionResult> get_comment_list(int muid, int page_offset, int page_num = 10, COMMENT_SELECT_ORDER select_order = COMMENT_SELECT_ORDER.UPDATE_DATE)
        {
            var result = await _commentDBHelper.get_comment_list(muid, page_num, page_offset, select_order);
            return Ok(result.ToList());
        }

        public async Task<IActionResult> get_user_comment(int muid)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Forbid("未登录");
            }
            var user = await _userManager.GetUserAsync(User);
            var result = await _commentDBHelper.get_comment_by_uid_muid(user.Id, muid);
            if(result != null)
            {
                CommentViewModel comment_view_model = new CommentViewModel
                {
                    Uid = result.Uid,
                    Muid = result.Muid,
                    UpdateDate = result.UpdateDate,
                    UpdateDateStr = result.UpdateDate.ToString("f"),
                    UploadDate = result.UploadDate,
                    UploadDateStr = result.UploadDate.ToString("f"),
                    UserName = User.Identity.Name,
                    CommentContent = result.CommentContent
                };
                return Ok(comment_view_model);
            }
            else
            {
                return Ok(null);
            }
            
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> delete_user_comment(int muid)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Forbid("未登录");
            }
            var user = await _userManager.GetUserAsync(User);
            CommentModel commentModel = new CommentModel
            {
                Uid = user.Id,
                Muid = muid
            };
            var result = await _commentDBHelper.delete_comment(commentModel);
            if(result == DBOperationResult.OK)
            {
                return Ok("删除成功");
            }
            else
            {
                return StatusCode(500, "删除失败，服务器或数据库错误");
            }
            
        }

    }
}
