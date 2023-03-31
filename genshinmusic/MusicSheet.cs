using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genshinmusic
{
    class MusicSheet
    {
        /// <summary>
        /// 音符表
        /// </summary>
        List<Note> music_sheet;

        List<List<Note>> music_with_key_sequence;

        BasicAttribution basic_attribution;

        int beat_per_bar;

        public List<Note> Music_sheet { get => music_sheet; set => music_sheet = value; }
        public List<List<Note>> Music_with_key_sequence { get => music_with_key_sequence; }
        public BasicAttribution Basic_attribution { get => basic_attribution;  }
        public int Beat_per_bar { get => beat_per_bar;  }

        public MusicSheet(BasicAttribution basic_attribution, int beat_per_bar)
        {
            music_sheet = new List<Note>();
            music_with_key_sequence = new List<List<Note>>(basic_attribution.Eight_degree_num * 7);
            for(int i = 0; i < basic_attribution.Eight_degree_num * 7; i++)
            {
                music_with_key_sequence.Add(new List<Note>());
            }
            this.basic_attribution = basic_attribution;
            this.beat_per_bar = beat_per_bar;
            this.music_sheet = new List<Note>();
            
        }

        /// <summary>
        /// 对给定的乐谱进行排序
        /// </summary>
        /// <param name="given_music_sheet">用户给定乐谱</param>
        private void sort_given_music_sheet(ref List<List<Note>> given_music_sheet, int key_idx)
        {
            given_music_sheet[key_idx].Sort(
                (x, y) =>
                {
                    // 1大于，0等于，-1小于
                    if (x.Absolute_semi_offset > y.Absolute_semi_offset)
                        return 1;
                    if (x.Absolute_semi_offset < y.Absolute_semi_offset)
                        return -1;
                    if (x.Absolute_semi_offset == y.Absolute_semi_offset)
                    {
                        // 按Key_idx从小到大排序，即从高音到低音
                        if (x.Key_idx > y.Key_idx)
                        {
                            return 1;
                        }
                        if (x.Key_idx < y.Key_idx)
                        {
                            return -1;
                        }
                    }

                    return 0;
                }
               );
        }

        /// <summary>
        /// 对乐谱排序，request_key_idx表示对music_with_key_sequence的哪些行排序
        /// </summary>
        /// <param name="request_key_idx">要求排序的键盘行索引，为null表示只对music_sheet排序</param>
        private void sort_music_sheet(List<int> request_key_idx=null)
        {
            music_sheet.Sort(
                (x, y) =>
                {
                    // 1大于，0等于，-1小于
                    if (x.Absolute_semi_offset > y.Absolute_semi_offset)
                        return 1;
                    if (x.Absolute_semi_offset < y.Absolute_semi_offset)
                        return -1;
                    if (x.Absolute_semi_offset == y.Absolute_semi_offset)
                    {
                        // 按Key_idx从小到大排序，即从高音到低音
                        if (x.Key_idx > y.Key_idx)
                        {
                            return 1;
                        }
                        if (x.Key_idx < y.Key_idx)
                        {
                            return -1;
                        }
                    }

                    return 0;
                }
               );
            if(request_key_idx != null)
            {
                foreach (var key_idx in request_key_idx)
                {
                    music_with_key_sequence[key_idx].Sort(
                      (x, y) =>
                      {
                      // 1大于，0等于，-1小于
                      if (x.Absolute_semi_offset < y.Absolute_semi_offset)
                          {
                              return -1;
                          }
                          if (x.Absolute_semi_offset > y.Absolute_semi_offset)
                          {
                              return 1;
                          }
                          return 0;
                      });
                }
            }
           
           

        }

        /// <summary>
        /// 添加单个音符
        /// </summary>
        /// <param name="note"></param>
        /// <returns>添加成功返回true；若添加的音符不合法则返回false</returns>
        public bool add_note(Note note)
        {
            if (is_note_valid(ref note))
            {
                music_sheet.Add(note);
                music_with_key_sequence[note.Key_idx].Add(note);
                sort_music_sheet(new List<int> { note.Key_idx });
                return true;
            }
            return false;
        }

        /// <summary>
        /// 先决条件：已经完成music_sheet的加载！
        /// 把music_sheet加载到music_with_key_sequence中添加note，同时会校验每个note的正确性
        /// </summary>
        /// <param name="note"></param>
        /// <returns>如果音符位置不合法，则返回false</returns>
        public bool from_music_sheet_load_notes_to_seq_list()
        {
            List<int> sort_key_list = new List<int>(); // 等待索引排序的列表
            int[] key_t = new int[basic_attribution.Eight_degree_num * 7];
            sort_music_sheet();
            foreach(var note in this.music_sheet)
            {
                Note note1 = note;
                if (!this.is_note_valid(ref note1))
                {
                    return false;
                }
                if (key_t[note.Key_idx] == 0)
                {
                    sort_key_list.Add(note.Key_idx);
                    key_t[note.Key_idx] = 1;
                }
                music_with_key_sequence[note.Key_idx].Add(note);

            }
            sort_music_sheet(sort_key_list);
            return true;
        }

        /// <summary>
        /// 重要：使用二分法，必须保证琴谱有序！判断音符在已知乐谱中是否合法（没有重叠），也会判断音符本身是否正确
        /// </summary>
        /// <param name="note"></param>
        /// <returns>如果合法，返回true; 如果不合法，返回false</returns>
        public bool is_note_valid(ref Note note)
        {
            // note越界错误
            if(note.Begin_bar_idx > note.End_bar_idx || 
                note.Begin_bar_idx <= 0 || 
                note.End_bar_idx <= 0||
                note.Absolute_semi_offset < 0 ||
                note.Key_idx < 0 ||
                note.Key_idx >= basic_attribution.Eight_degree_num * 7 ||
                note.Semi_offset < 0 || 
                note.Continuous_semi <= 0 
                )
            {
                Console.WriteLine("越界错误");
                return false;
                
            }

            // note 起始位置错误
            if(beat_per_bar * 4 * (note.Begin_bar_idx - 1) + note.Semi_offset != note.Absolute_semi_offset)
            {
                Console.WriteLine("起始位置错误");
                return false;
                
            }

            // note 结束小节号错误
            if((note.Absolute_semi_offset + note.Continuous_semi)/(beat_per_bar * 4) != note.End_bar_idx - 1)
            {
                if((note.Absolute_semi_offset + note.Continuous_semi) % (beat_per_bar * 4) == 0
                    &&
                (note.Absolute_semi_offset + note.Continuous_semi) / (beat_per_bar * 4) != note.End_bar_idx)
                {
                    Console.WriteLine("结束小节号错误");
                    Console.WriteLine("endin:" + note.End_bar_idx.ToString());
                    Console.WriteLine("calcin:" + ((note.Absolute_semi_offset + note.Semi_offset) / (beat_per_bar * 4)).ToString()+1);
                    return false;
                }
               
                
            }

            // note 起始小节号错误
            if(note.Absolute_semi_offset/(beat_per_bar*4) != note.Begin_bar_idx - 1)
            {
                Console.WriteLine("起始小节号错误");
                return false;
             
            }

/*********************临时：遍历法*********************/
            //for(int i = 0; i < music_with_key_sequence[note.Key_idx].Count - 1; i++)
            //{
            //    var mid_note = music_with_key_sequence[note.Key_idx][i];
            //    if(mid_note.Begin_bar_idx > note.End_bar_idx)
            //    {
            //        break;
            //    }
            //    if (mid_note.Begin_bar_idx < note.Begin_bar_idx)
            //    {
            //        if (mid_note.End_bar_idx > note.Begin_bar_idx ||
            //            mid_note.End_bar_idx == note.Begin_bar_idx &&
            //            mid_note.Continuous_semi + mid_note.Absolute_semi_offset >
            //            note.Absolute_semi_offset)
            //        {
            //            return false;
            //        }
            //    }
            //    else if (mid_note.Begin_bar_idx > note.Begin_bar_idx)
            //    {
            //        if (mid_note.Begin_bar_idx < note.End_bar_idx ||
            //            mid_note.Begin_bar_idx == note.End_bar_idx &&
            //            note.Continuous_semi + note.Absolute_semi_offset >
            //            mid_note.Absolute_semi_offset)
            //        {
            //            return false;
            //        }
            //    }
            //    else if(mid_note.Absolute_semi_offset < note.Absolute_semi_offset)
            //    {
            //        if(mid_note.Absolute_semi_offset + mid_note.Continuous_semi > note.Absolute_semi_offset)
            //        {
            //            return false;
            //        }
            //    }
            //    else if(mid_note.Absolute_semi_offset > note.Absolute_semi_offset)
            //    {
            //        if (note.Absolute_semi_offset + note.Continuous_semi > mid_note.Absolute_semi_offset)
            //        {
            //            return false;
            //        }
            //    }
            //    else if (mid_note.Absolute_semi_offset == note.Absolute_semi_offset)
            //    {
            //        return false;
            //    }
            //}
            /******************二分法：有BUG!!********************/
            int low = 0;
            int high = music_with_key_sequence[note.Key_idx].Count - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                var mid_note = music_with_key_sequence[note.Key_idx][mid];
                int mid_note_begin_bar = mid_note.Begin_bar_idx;

                if (mid_note_begin_bar < note.Begin_bar_idx)
                {
                    if (mid_note.End_bar_idx > note.Begin_bar_idx ||
                        mid_note.End_bar_idx == note.Begin_bar_idx &&
                        mid_note.Continuous_semi + mid_note.Absolute_semi_offset >
                        note.Absolute_semi_offset)
                    {
                        return false;
                    }
                    low = mid + 1;
                }
                else if (mid_note_begin_bar > note.Begin_bar_idx)
                {
                    if (mid_note.Begin_bar_idx < note.End_bar_idx ||
                        mid_note.Begin_bar_idx == note.End_bar_idx &&
                        note.Continuous_semi + note.Absolute_semi_offset >
                        mid_note.Absolute_semi_offset)
                    {
                        return false;
                    }

                    high = mid - 1;
                }
                else
                {
                    int i = mid;
                    int j = mid + 1;
                    while (i >= 0 && music_with_key_sequence[note.Key_idx][i].Begin_bar_idx == note.Begin_bar_idx)
                    {
                        mid_note = music_with_key_sequence[note.Key_idx][i];
                        Note a, b; // a小b大
                        if (note.Semi_offset < mid_note.Semi_offset)
                        {
                            a = note;
                            b = mid_note;
                        }
                        else if (note.Semi_offset > mid_note.Semi_offset)
                        {
                            a = mid_note;
                            b = note;
                        }
                        else
                        {
                            return false;
                        }
                        if (a.Semi_offset + a.Continuous_semi > b.Semi_offset)
                        {
                            return false;
                        }
                        i--;
                    }

                    while (j <= high && music_with_key_sequence[note.Key_idx][j].Begin_bar_idx == note.Begin_bar_idx)
                    {
                        mid_note = music_with_key_sequence[note.Key_idx][j];
                        Note a, b; // a小b大
                        if (note.Semi_offset < mid_note.Semi_offset)
                        {
                            a = note;
                            b = mid_note;
                        }
                        else if (note.Semi_offset > mid_note.Semi_offset)
                        {
                            a = mid_note;
                            b = note;
                        }
                        else
                        {
                            return false;
                        }
                        if (a.Semi_offset + a.Continuous_semi > b.Semi_offset)
                        {
                            return false;
                        }
                        j++;
                    }
                    if (i >= 0 && music_with_key_sequence[note.Key_idx][i].Absolute_semi_offset + music_with_key_sequence[note.Key_idx][i].Continuous_semi > note.Absolute_semi_offset)
                    {
                        return false;
                    }
                    else if (j <= high && note.Absolute_semi_offset + note.Continuous_semi > music_with_key_sequence[note.Key_idx][j].Absolute_semi_offset)
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }


                }

            }


            return true;
        }

        /// <summary>
        /// 获取当前放置的音符块长度限制（16分音符，可以取等）
        /// </summary>
        /// <param name="absolute_semi"></param>
        /// <param name="key_idx"></param>
        /// <returns></returns>
        public int get_putting_continuous_semi_limit(int absolute_semi, int key_idx)
        {
            var notes = this.music_with_key_sequence[key_idx];
            int low = 0;
            int high = notes.Count - 1;
            int rtn = 20000;
            int mid = 0;
            while(low <= high)
            {
                mid = (low + high) / 2;
                if(notes[mid].Absolute_semi_offset <= absolute_semi)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid-1;
                    rtn = Math.Min(notes[mid].Absolute_semi_offset - absolute_semi, rtn);
                }
            }

            return rtn;
        }
    
        /// <summary>
        /// 批量删去音符
        /// </summary>
        /// <param name="target"></param>
        public void delete_notes(List<Note> target)
        {
            foreach(var note in target)
            {
                music_sheet.Remove(note);
                music_with_key_sequence[note.Key_idx].Remove(note);
            }
        }


        /// <summary>
        /// 看note是否合法，且不在origin里
        /// </summary>
        /// <param name="note">判断目标</param>
        /// <param name="temp_seq_note">去除原有音符的临时轨道索引序列谱</param>
        /// <returns></returns>
        private bool is_note_valid_in_moving(Note note, List<List<Note>> temp_seq_note)
        {
            // note越界错误
            if (note.Begin_bar_idx > note.End_bar_idx ||
                note.Begin_bar_idx <= 0 ||
                note.End_bar_idx <= 0 ||
                note.Absolute_semi_offset < 0 ||
                note.Key_idx < 0 ||
                note.Key_idx >= basic_attribution.Eight_degree_num * 7 ||
                note.Semi_offset < 0 ||
                note.Semi_offset >= beat_per_bar * basic_attribution.Semiquaver_width ||
                note.Continuous_semi <= 0
                )
            {
                Console.WriteLine("越界错误");
                return false;

            }

            // note 起始位置错误
            if (beat_per_bar * 4 * (note.Begin_bar_idx - 1) + note.Semi_offset != note.Absolute_semi_offset)
            {
                Console.WriteLine("起始位置错误");
                return false;

            }

            // note 结束小节号错误
            if ((note.Absolute_semi_offset + note.Continuous_semi) / (beat_per_bar * 4) != note.End_bar_idx - 1)
            {
                if ((note.Absolute_semi_offset + note.Continuous_semi) % (beat_per_bar * 4) == 0
                    &&
                (note.Absolute_semi_offset + note.Continuous_semi) / (beat_per_bar * 4) != note.End_bar_idx)
                {
                    Console.WriteLine("结束小节号错误");
                    Console.WriteLine("endin:" + note.End_bar_idx.ToString());
                    Console.WriteLine("calcin:" + ((note.Absolute_semi_offset + note.Semi_offset) / (beat_per_bar * 4)).ToString() + 1);
                    return false;
                }


            }

            // note 起始小节号错误
            if (note.Absolute_semi_offset / (beat_per_bar * 4) != note.Begin_bar_idx - 1)
            {
                Console.WriteLine("起始小节号错误");
                return false;

            }

            int low = 0;
            int high = temp_seq_note[note.Key_idx].Count - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                var mid_note = temp_seq_note[note.Key_idx][mid];
                int mid_note_begin_bar = mid_note.Begin_bar_idx;

                if (mid_note_begin_bar < note.Begin_bar_idx)
                {


                    if (mid_note.End_bar_idx > note.Begin_bar_idx ||
                        mid_note.End_bar_idx == note.Begin_bar_idx &&
                        mid_note.Continuous_semi + mid_note.Absolute_semi_offset >
                        note.Absolute_semi_offset)
                    {
                        return false;
                    }
                    low = mid + 1;
                }
                else if (mid_note_begin_bar > note.Begin_bar_idx)
                {

                    if (mid_note.Begin_bar_idx < note.End_bar_idx ||
                        mid_note.Begin_bar_idx == note.End_bar_idx &&
                        note.Continuous_semi + note.Absolute_semi_offset >
                        mid_note.Absolute_semi_offset)
                    {
                        return false;
                    }

                    high = mid - 1;
                }
                else
                {

                    int i = mid;
                    int j = mid + 1;
                    while (i >= 0 && temp_seq_note[note.Key_idx][i].Begin_bar_idx == note.Begin_bar_idx)
                    {
                        mid_note = temp_seq_note[note.Key_idx][i];
                        Note a, b; // a小b大
                        if (note.Semi_offset < mid_note.Semi_offset)
                        {
                            a = note;
                            b = mid_note;
                        }
                        else if (note.Semi_offset > mid_note.Semi_offset)
                        {
                            a = mid_note;
                            b = note;
                        }
                        else
                        {
                            return false;
                        }
                        if (a.Semi_offset + a.Continuous_semi > b.Semi_offset)
                        {
                            return false;
                        }
                        i--;
                    }
             

                    while (j <= high && temp_seq_note[note.Key_idx][j].Begin_bar_idx == note.Begin_bar_idx)
                    {
                        mid_note = temp_seq_note[note.Key_idx][j];
                        Note a, b; // a小b大
                        if (note.Semi_offset < mid_note.Semi_offset)
                        {
                            a = note;
                            b = mid_note;
                        }
                        else if (note.Semi_offset > mid_note.Semi_offset)
                        {
                            a = mid_note;
                            b = note;
                        }
                        else
                        {

                            return false;
                        }
                        if (a.Semi_offset + a.Continuous_semi > b.Semi_offset)
                        {
                            return false;
                        }
                        j++;
                    }
  
                    if(i >= 0 && temp_seq_note[note.Key_idx][i].Absolute_semi_offset + temp_seq_note[note.Key_idx][i].Continuous_semi > note.Absolute_semi_offset)
                    {
                        return false;
                    }
                    else if(j <= high && note.Absolute_semi_offset + note.Continuous_semi > temp_seq_note[note.Key_idx][j].Absolute_semi_offset)
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }


                }

            }

            return true;
        }

        public bool move_notes(List<Note> origin, List<Note> target)
        {
            List<List<Note>> temp_note = new List<List<Note>>();
            for(int i = 0; i < this.music_with_key_sequence.Count; i++)
            {
                temp_note.Add(new List<Note>());
                for (int j = 0; j < this.music_with_key_sequence[i].Count; j++)
                {
                    temp_note[i].Add(new Note(this.music_with_key_sequence[i][j].Begin_bar_idx,
                       this.music_with_key_sequence[i][j].Semi_offset,
                       this.music_with_key_sequence[i][j].Key_idx,
                       this.music_with_key_sequence[i][j].Continuous_semi,
                       this.music_with_key_sequence[i][j].End_bar_idx,
                       this.beat_per_bar,
                       this.music_with_key_sequence[i][j].get_music_rectangle_block()));
                }
            }
            foreach(var note in origin)
            {
                temp_note[note.Key_idx].Remove(note);
                
            }
            foreach(var note in target)
            {
                if (!is_note_valid_in_moving(note, temp_note))
                {
                    return false;
                }
            }
            delete_notes(origin);


            for (int i = 0; i < target.Count; i++)
            {
                add_note(target[i]);
            }

            return true;
        }

        /// <summary>
        /// QE修改音符块长度
        /// </summary>
        /// <param name="origin">被修改的原始音符</param>
        /// <param name="target">修改后的目标音符</param>
        /// <returns></returns>
        public bool extend_notes(List<Note> origin, List<Note> target)
        {
            List<List<Note>> temp_note = new List<List<Note>>();
            for (int i = 0; i < this.music_with_key_sequence.Count; i++)
            {
                temp_note.Add(new List<Note>());
                for (int j = 0; j < this.music_with_key_sequence[i].Count; j++)
                {
                    temp_note[i].Add(new Note(this.music_with_key_sequence[i][j].Begin_bar_idx,
                       this.music_with_key_sequence[i][j].Semi_offset,
                       this.music_with_key_sequence[i][j].Key_idx,
                       this.music_with_key_sequence[i][j].Continuous_semi,
                       this.music_with_key_sequence[i][j].End_bar_idx,
                       this.beat_per_bar,
                       this.music_with_key_sequence[i][j].get_music_rectangle_block()));
                }
            }

            for(int i = 0; i < origin.Count; i++)
            {
                var org_note = origin[i];
               
                temp_note[org_note.Key_idx].Remove(org_note);
                if (!is_note_valid_in_moving(target[i], temp_note))
                {
                    return false;
                }
                temp_note[org_note.Key_idx].Add(org_note);
                this.sort_given_music_sheet(ref temp_note, org_note.Key_idx);
            }
            delete_notes(origin);


            for (int i = 0; i < target.Count; i++)
            {
                add_note(target[i]);
            }

            return true;
        }


    }
}
