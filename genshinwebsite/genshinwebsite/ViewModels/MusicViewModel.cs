using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genshinwebsite.ViewModels
{
    public class MusicViewModel
    {
        public int Id { get; set; }

        public DateTime Datetime { get; set; } = DateTime.Now;
        public string MusicTitle { get; set; }
        //public string Music_proj_addr { get; set; }
        //public string Keyboard_music_addr { get; set; }
        //public string Midi_addr { get; set; }
        public string Abstract_content { get; set; }
        public string Uploader { get; set; }
        public int Uid { get; set; }
        public int View_num { get; set; }
        public int Download_num { get; set; }
    }
}
