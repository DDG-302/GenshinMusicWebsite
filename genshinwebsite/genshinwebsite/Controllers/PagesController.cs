using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;
using genshinwebsite.Controllers.music;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using genshinwebsite.Models;
using genshinwebsite.ViewModels;
using genshinwebsite.Services;
using Microsoft.Extensions.Configuration;

namespace genshinwebsite.Controllers
{
    public class PagesController : Controller
    {

        private readonly string _data_root;
        private readonly IMusicDB<MusicModel> _musicDBHelper;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;

        public PagesController(IWebHostEnvironment env
            , IMusicDB<MusicModel> musicDBHelper, UserManager<UserModel> userManager, IConfiguration configuration)
        {
            _data_root = env.ContentRootPath;
            _musicDBHelper = musicDBHelper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int page_offset, string music_title)
        {
            List<MusicViewModel> musicViewModels = new List<MusicViewModel>();
            var num_per_page = _configuration.GetValue("NumPerPage", 10);
            ViewData["page_offset"] = Math.Max(1, page_offset);
            page_offset = Math.Max(0, page_offset - 1);
            var user_count = _musicDBHelper.get_music_count();
            if (user_count % num_per_page != 0)
            {
                ViewData["max_page"] = user_count / num_per_page + 1;
            }
            else
            {
                ViewData["max_page"] = user_count / num_per_page;
            }

            foreach (var m in _musicDBHelper.get_music_by_offset(num_per_page, page_offset))
            {
                MusicViewModel music = new MusicViewModel()
                {
                    Id = m.Id,
                    Uploader = (await _userManager.FindByIdAsync(m.User_id.ToString())).UserName,
                    Abstract_content = m.Abstract_content,
                    MusicTitle = m.MusicTitle,
                    Like_num = m.Like_num,
                };
                if(music.Abstract_content.Length > 60)
                {
                    music.Abstract_content = music.Abstract_content.Substring(0, 57) + "...";
                }
                else
                {
                    music.Abstract_content.PadRight(60);
                }
                musicViewModels.Add(music);
            }
            return View(musicViewModels);
        }

      
    }
}
