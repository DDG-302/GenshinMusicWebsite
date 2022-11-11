﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace genshinmusic
{
    class Note
    {
        int begin_bar_idx;
        int semi_offset;
        int key_idx;
        int continuous_semi;
        int end_bar_idx;
        int absolute_semi_offset;

        Rectangle music_rectangle_block;// 音符块

        public int Begin_bar_idx { get => begin_bar_idx; set => begin_bar_idx = value; }
        public int Semi_offset { get => semi_offset; set => semi_offset = value; }
        public int Key_idx { get => key_idx; set => key_idx = value; }
        public int Continuous_semi { get => continuous_semi; set => continuous_semi = value; }
        public int End_bar_idx { get => end_bar_idx; set => end_bar_idx = value; }
        public int Absolute_semi_offset { get => absolute_semi_offset; set => absolute_semi_offset = value; }

        /// <summary>
        /// 获取音符块对应UI上的引用
        /// </summary>
        /// <returns></returns>
        public Rectangle get_music_rectangle_block()
        {
            return this.music_rectangle_block;
        }

        /// <summary>
        /// 设置UI音符块
        /// </summary>
        /// <param name="music_rectangle">UI上对应的音符块</param>
        public void set_music_rectangle_block(Rectangle music_rectangle)
        {
            this.music_rectangle_block = music_rectangle;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin_bar_idx">开始小节号</param>
        /// <param name="semi_offset">小节内16分音符偏移</param>
        /// <param name="key_idx">键盘索引（自高音到低音从0增大）</param>
        /// <param name="continuous_semi">持续时值（16分音符）</param>
        /// <param name="end_bar_idx">末尾小节号</param>
        /// <param name="beats_per_bar">每小节拍数（4分音符数）</param>
        /// <param name="music_block">对应的UI音符块</param>
        public Note(int begin_bar_idx, int semi_offset, int key_idx, int continuous_semi, int end_bar_idx, int beats_per_bar, Rectangle music_block = null)
        {
            this.begin_bar_idx = begin_bar_idx;
            this.semi_offset = semi_offset;
            this.key_idx = key_idx;
            this.continuous_semi = continuous_semi;
            this.end_bar_idx = end_bar_idx;
            this.absolute_semi_offset = (begin_bar_idx - 1) * beats_per_bar * 4 + semi_offset;
            this.music_rectangle_block = music_block;
        }

        public Note()
        {

        }

        public override bool Equals(object obj)
        {
            Note note = obj as Note;
            if(note.continuous_semi == this.continuous_semi &&
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