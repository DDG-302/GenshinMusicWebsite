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
using Microsoft.AspNetCore.Http;
using genshinwebsite.Controllers.music;
using System.Text;
using System.Text.Json;
using System.Threading;


namespace genshinwebsite.Controllers
{
    public class MusicController : Controller
    {
        private readonly string _data_root;
        private readonly string _img_root;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly ICommentDB<CommentModel, CommentViewModel> _commentDBHelper;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MusicController(IWebHostEnvironment env, IMusicDB<MusicModel, MusicViewModel> musicDBHelper, SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, ICommentDB<CommentModel, CommentViewModel> commentDBHelper, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _data_root = Path.Combine(env.ContentRootPath, "music_save");
            _img_root = _img_root = Path.Combine(_hostingEnvironment.WebRootPath, "music_img_save");
            _musicDBHelper = musicDBHelper;
            _signInManager = signInManager;
            _userManager = userManager;
            _commentDBHelper = commentDBHelper;
            _configuration = configuration;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] string abs, IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return StatusCode(400, "上传失败：用户不存在");
            }
            if (!User.IsInRole("Admin") && !User.IsInRole("God"))
            {
                var num = await _musicDBHelper.get_user_today_upload_num(user.Id);
                if(num >= user.UploadLimit)
                {
                    return StatusCode(403, "上传失败：当日上传次数过多，每天只能上传" + user.UploadLimit.ToString() + "个乐谱\n您今天已经上传了" + num.ToString()+"个");
                }
            }
            if (!User.Identity.IsAuthenticated || file == null)
            {
                return StatusCode(403, "上传失败：文件为空");
            }

            string rtn_str = "上传成功";
            int status_code = 200;


            var readstream = new StreamReader(file.OpenReadStream());
            //var data = readstream.ReadToEnd();

            string json_str = await readstream.ReadToEndAsync();
            MusicModel result = null;
            try
            {
                SaveFileTemplate save_file = null;
                save_file = JsonSerializer.Deserialize<SaveFileTemplate>(json_str);
                var music_sheet = save_file.Music_sheet;
               
                
                // 路径格式为 {用户id}/{乐谱id}/{title}.genmujson(这是为了确保重名乐谱也可以上传)
                if (MusicValidator.from_music_sheet_load_notes_to_seq_list(ref music_sheet, save_file.Beats_per_bar))
                {
                    
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

                        result = await _musicDBHelper.add_one(musicModel);
                        if (result == null)
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
                                await file.CopyToAsync(fileStream);
                            }
                            

                            Thread mythread = new Thread(new ThreadStart(delegate ()
                            {
                                var img_generator = new ImgGenerator();
                                string img_folder = Path.Combine(_img_root, musicModel.Id.ToString());
                                img_generator.generate_img(music_sheet, musicModel.MusicTitle, img_folder, save_file.Bpm.ToString(), save_file.Beats_per_bar);
                            }));
                            mythread.SetApartmentState(ApartmentState.STA);
                            mythread.Start();
                            mythread.Join();
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
            if(status_code == 200 && result != null)
            {
                return StatusCode(status_code, result.Id);
            }

            return StatusCode(status_code, rtn_str);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, God")]
        public async Task<IActionResult> DeleteMusic(int muid)
        {
            // 注意这里还没有实现删除乐谱图片
            var music_model = await _musicDBHelper.get_by_id(muid);
            string user_music_root = Path.Combine(_data_root, music_model.Uid.ToString());
            string music_save_file = Path.Combine(user_music_root, music_model.Id.ToString());

            if (!Directory.Exists(music_save_file))
            {
                if (music_model != null)
                {
                    var result = _musicDBHelper.delete_one_by_id(music_model.Id, music_model.Uid);
                    if(result != DBOperationResult.OK)
                    {
                        return StatusCode(500, "数据库操作失败");
                    }
                }
                return Ok();
            }
            try
            {
                Directory.Delete(music_save_file, true);
                if (music_model != null)
                {
                    var result = _musicDBHelper.delete_one_by_id(music_model.Id, music_model.Uid);
                    if (result != DBOperationResult.OK)
                    {
                        return StatusCode(500, "数据库操作失败");
                    }
                }
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteMyMusic(int muid)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                string user_music_root = Path.Combine(_data_root, user.Id.ToString());
                string music_save_file = Path.Combine(user_music_root, muid.ToString());

                if (Directory.Exists(music_save_file))
                {
                    Directory.Delete(music_save_file, true);
                }
            }
            catch
            {
                return StatusCode(500, "文件系统删除出错");
            }
         

           
            var result = _musicDBHelper.delete_one_by_id(muid, user.Id);
            // 路径格式为 {用户id}/{乐谱id}/{title}.genmujson(这是为了确保重名乐谱也可以上传)
            if (result == DBOperationResult.OK)
            {
                return Ok("删除完成");
            }
            else if (result == DBOperationResult.ERROR)
            {
                return StatusCode(403, "禁止此请求");
            }
            else
            {
                return StatusCode(500, "出错");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit_comment(string comment, int muid)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return StatusCode(403, "用户未登录");
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
                return StatusCode(403, "未登录");
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
                return StatusCode(403, "未登录");
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
