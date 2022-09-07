using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genshinwebsite.Controllers.music { 
    public class MusicVlidator
    {
        static private bool is_note_valid(ref Note note, ref List<List<Note>> music_with_key_sequence, int beat_per_bar, int eight_degree_num=3)
        {
            // note越界错误
            if (note.Begin_bar_idx > note.End_bar_idx ||
                note.Begin_bar_idx <= 0 ||
                note.End_bar_idx <= 0 ||
                note.Absolute_semi_offset < 0 ||
                note.Key_idx < 0 ||
                note.Key_idx >= eight_degree_num * 7 ||
                note.Semi_offset < 0 ||
                note.Continuous_semi <= 0
                )
            {
                return false;

            }

            // note 起始位置错误
            if (beat_per_bar * 4 * (note.Begin_bar_idx - 1) + note.Semi_offset != note.Absolute_semi_offset)
            {
                return false;

            }

            // note 结束小节号错误
            if ((note.Absolute_semi_offset + note.Continuous_semi) / (beat_per_bar * 4) != note.End_bar_idx - 1)
            {
                if ((note.Absolute_semi_offset + note.Continuous_semi) % (beat_per_bar * 4) == 0
                    &&
                (note.Absolute_semi_offset + note.Continuous_semi) / (beat_per_bar * 4) != note.End_bar_idx)
                {
                
                    return false;
                }


            }

            // note 起始小节号错误
            if (note.Absolute_semi_offset / (beat_per_bar * 4) != note.Begin_bar_idx - 1)
            {
                return false;

            }


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

                    while (j < music_with_key_sequence[note.Key_idx].Count && music_with_key_sequence[note.Key_idx][j].Begin_bar_idx == note.Begin_bar_idx)
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
                    if (i < music_with_key_sequence[note.Key_idx].Count - j)
                    {

                        high = i;
                    }
                    else
                    {
                        low = j;
                    }


                }

            }


            return true;
        }

        /// <summary>
        /// 对乐谱排序，request_key_idx表示对music_with_key_sequence的哪些行排序
        /// </summary>
        /// <param name="request_key_idx">要求排序的键盘行索引</param>
        static private void sort_music_sheet(ref List<Note> music_sheet, ref List<List<Note>> music_with_key_sequence, List<int> request_key_idx)
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
            if (request_key_idx != null)
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

        static private void sort_music_sheet(ref List<Note> music_sheet)
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


        /// 把music_sheet加载到music_with_key_sequence中添加note，同时会校验每个note的正确性
        /// </summary>
        /// <param name="music_sheet">引用传参，会将其排序</param>
        /// <param name="beat_per_bar">每小节几拍</param>
        /// <param name="eight_degree_num">八度数量，默认为3（目前的genshin乐器配置）</param>
        /// <returns>如果音符位置不合法，则返回false</returns>
        static public bool from_music_sheet_load_notes_to_seq_list(ref List<Note> music_sheet, int beat_per_bar, int eight_degree_num = 3)
        {
            List<int> sort_key_list = new List<int>(); // 等待索引排序的列表
            int[] key_t = new int[eight_degree_num * 7];
            List<List<Note>> music_with_key_sequence = new List<List<Note>>();
            sort_music_sheet(ref music_sheet);
            for(int i = 0; i < eight_degree_num * 7; i++)
            {
                music_with_key_sequence.Add(new List<Note>());
            }
            foreach (var note in music_sheet)
            {
                Note note1 = note;
                if (!is_note_valid(ref note1, ref music_with_key_sequence, beat_per_bar))
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
            sort_music_sheet(ref music_sheet, ref music_with_key_sequence, sort_key_list);
            return true;
        }
    }
}
