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
using Microsoft.AspNetCore.Http.Extensions;

namespace genshinwebsite.Controllers
{
    public class PagesController : Controller
    {

        private readonly string _data_root;
        private readonly IMusicDB<MusicModel, MusicViewModel> _musicDBHelper;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;

        public PagesController(IWebHostEnvironment env
            , IMusicDB<MusicModel, MusicViewModel> musicDBHelper, UserManager<UserModel> userManager, IConfiguration configuration)
        {
            _data_root = env.ContentRootPath;
            _musicDBHelper = musicDBHelper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int page_offset, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            ViewData["url"] = HttpContext.Request.GetDisplayUrl();
            if (music_title != null && music_title != string.Empty && music_title != "")
            {
                ViewData["music_title"] = music_title;
            }
            List<MusicViewModel> musicViewModels = new List<MusicViewModel>();
            var num_per_page = _configuration.GetValue("NumPerPage", 10);
            page_offset = Math.Max(1, page_offset);

            int item_count = 0;
            var music_list = _musicDBHelper.get_music_by_offset(out item_count, num_per_page, page_offset - 1, music_title, select_order);
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
                    Uploader = m.Uploader,
                    Abstract_content = m.Abstract_content,
                    MusicTitle = m.MusicTitle,
                    View_num = m.View_num,
                    Download_num = m.Download_num
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

        public FileResult download(int muid)
        {
            var music_model = _musicDBHelper.get_by_id(muid);
            string fileName = "music_save/" + music_model.User_id.ToString() + "/" + muid.ToString() + "/" + music_model.MusicTitle + ".genmujson";
            
            var stream = System.IO.File.OpenRead(fileName);

            //string suffix = Path.GetExtension(fileName);
            //var provider = new FileExtensionContentTypeProvider();
            //var contentType = provider.Mappings[suffix];

            //var contentType = MimeMapping.GetMimeMapping(fileName);
            _musicDBHelper.add_or_set_download_num(muid);
            return File(stream, "text/plain", music_model.MusicTitle + ".genmujson");
        }
    }
}
