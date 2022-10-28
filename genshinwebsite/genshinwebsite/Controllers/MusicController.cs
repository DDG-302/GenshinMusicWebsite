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

namespace genshinwebsite.Controllers
{
    public class MusicController : Controller
    {
        private readonly string _data_root;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;

        public MusicController(IWebHostEnvironment env, IMusicDB<MusicModel, MusicViewModel> musicDBHelper)
        {
            _data_root = Path.Combine(env.ContentRootPath, "music_save");
            _musicDBHelper = musicDBHelper;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_music(int muid)
        {
            MusicModel music_model = _musicDBHelper.get_by_id(muid);
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
        public IActionResult Submit_comment(string comment)
        {

            return Ok();
        }
    }
}
