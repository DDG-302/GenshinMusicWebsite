using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace genshinwebsite.ViewModels
{
    public class MusicUploadModel
    {
        [Required(ErrorMessage = "请输入一个标题")]
        public string MusicTitle { get; set; }
        //public string Music_proj_addr { get; set; }
        //public string Keyboard_music_addr { get; set; }
        //public string Midi_addr { get; set; }

        [MaxLength(400, ErrorMessage = "简介好长呀，不要超过400个字符捏")]
        public string Abstract_content { get; set; }


        [Required(ErrorMessage = "请选择一个项目文件")]
        public IFormFile File { get; set; }

    }
}
