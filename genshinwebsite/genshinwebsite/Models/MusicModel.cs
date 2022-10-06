using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace genshinwebsite.Models
{
    public class MusicModel:DbContext
    {
        public int Id { get; set; }

        public DateTime Datetime { get; set; } = DateTime.Now;
        public string MusicTitle { get; set; }

        //public string Music_proj_addr { get; set; }
        //public string Keyboard_music_addr { get; set; }
        //public string Midi_addr { get; set; }
        public string Abstract_content { get; set; }
        public int User_id { get; set; }
        public int View_num { get; set; }
        public int Download_num { get; set; }
    }
}
