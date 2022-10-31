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
using Microsoft.AspNetCore.Authorization;

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

        public async Task<FileResult> download(int muid)
        {
            var music_model = await _musicDBHelper.get_by_id(muid);
            if(music_model == null)
            {
                return null;
            }
            string fileName = "music_save/" + music_model.User_id.ToString() + "/" + muid.ToString() + "/" + music_model.MusicTitle + ".genmujson";
            if (System.IO.File.Exists(fileName)){
                var stream = System.IO.File.OpenRead(fileName);
                _musicDBHelper.add_or_set_download_num(muid);
                return File(stream, "text/plain", music_model.MusicTitle + ".genmujson");
            }
            else
            {
                return null;
            }

            //string suffix = Path.GetExtension(fileName);
            //var provider = new FileExtensionContentTypeProvider();
            //var contentType = provider.Mappings[suffix];
            //var contentType = MimeMapping.GetMimeMapping(fileName);
            
        }

        
        [Route("Pages/{muid:int}")]
        public async Task<IActionResult> Detail(int muid)
        {
            ViewData["muid"] = muid;
            var music = await _musicDBHelper.get_by_id(muid);
            if(music != null)
            {
                MusicDetailViewModel musicDetailViewModel = new MusicDetailViewModel()
                {
                    Id = music.Id,
                    MusicTitle = music.MusicTitle,
                    Datetime = music.Datetime,
                    Abstract_content = music.Abstract_content,
                    Download_num = music.Download_num,
                    View_num = music.View_num + 1

                };
                music.View_num++;
                _musicDBHelper.view_one(music);
                
                return View(musicDetailViewModel);
            }
            else
            {
                return Redirect("Home");
            }
            
        }
    }
}
