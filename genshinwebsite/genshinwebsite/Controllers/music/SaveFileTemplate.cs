using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genshinwebsite.Controllers.music

{
    /// <summary>
    /// 工程保存模板
    /// </summary>
    class SaveFileTemplate
    {
        string music_name;
        string program_ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        int beats_per_bar;
        int bpm;
        List<Note> music_sheet;

        public string Music_name { get => music_name; set => music_name = value; }
        public string Program_ver { get => program_ver; set => program_ver = value; }
        public int Beats_per_bar { get => beats_per_bar; set => beats_per_bar = value; }
        public int Bpm { get => bpm; set => bpm = value; }
        public List<Note> Music_sheet { get => music_sheet; set => music_sheet = value; }

        public SaveFileTemplate()
        {

        }
    }
}
