using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genshinwebsite.Controllers.music

{
    public class Note
    {
        int begin_bar_idx;
        int semi_offset;
        int key_idx;
        int continuous_semi;
        int end_bar_idx;
        int absolute_semi_offset;

        public int Begin_bar_idx { get => begin_bar_idx; set => begin_bar_idx = value; }
        public int Semi_offset { get => semi_offset; set => semi_offset = value; }
        public int Key_idx { get => key_idx; set => key_idx = value; }
        public int Continuous_semi { get => continuous_semi; set => continuous_semi = value; }
        public int End_bar_idx { get => end_bar_idx; set => end_bar_idx = value; }
        public int Absolute_semi_offset { get => absolute_semi_offset; set => absolute_semi_offset = value; }

        public override bool Equals(object obj)
        {
            Note note = obj as Note;
            if (note.continuous_semi == this.continuous_semi &&
                note.absolute_semi_offset == this.absolute_semi_offset &&
                note.key_idx == this.key_idx

                )
            {
                return true;
            }

            return false;
        }

    }

 
}
