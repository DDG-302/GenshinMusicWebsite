using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace genshinmusic
{
    public partial class MainWindow : Window
    {


        private void WindowWheelHandler(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer viewer = null;
            double offset = 0.0; // 希望的滚动条的偏移
            double viewer_current_offset = 0;
            double num = Math.Abs(e.Delta / 2);
            double scrollable_limit = 0;
            if (shift_down || SV_with_ver_bar.ScrollableHeight == 0)
            {
                viewer = SV_with_ho_bar;
                viewer_current_offset = viewer.HorizontalOffset;
                scrollable_limit = viewer.ScrollableWidth;
            }
            else
            {
                viewer = SV_with_ver_bar;
                viewer_current_offset = viewer.VerticalOffset; ;
                scrollable_limit = viewer.ScrollableHeight;
            }

            if (e.Delta > 0)
            {
                offset = Math.Max(0.0, viewer_current_offset - num);
            }
            else
            {
                offset = Math.Min(scrollable_limit, viewer_current_offset + num);
            }

            if (offset != viewer_current_offset)
            {
                if (shift_down || SV_with_ver_bar.ScrollableHeight == 0)
                    viewer.ScrollToHorizontalOffset(offset);
                else
                {
                    viewer.ScrollToVerticalOffset(offset);

                }

                e.Handled = true;
            }
        }

        private void window_key_down_handler(object sender, KeyEventArgs e)
        {
            //Console.WriteLine("key="+ e.Key.ToString());

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shift_down = true;
            }
            else if(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Oem3)
            {
                delete_chosen_condition();
                
            }
            else if(e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                ctrl_down = true;
            }
            else if(e.Key == Key.S && ctrl_down)
            {
                SaveLoadProj saveLoadProj = new SaveLoadProj();
                if (proj_save_path != null && proj_save_path != "")
                {
                    if(saveLoadProj.save_to_existed_file(music_sheet, int.Parse(bpm_inputbox.Text), proj_save_path))
                    {
                        this.is_proj_saved = true;
                        this.ctrl_down = false;
                        //this.proj_save_path = saveLoadProj.File_path;
                        //this.Title = saveLoadProj.Proj_name + Window_title_suffix;
                    }
                }
                else
                {
                    if(saveLoadProj.save_to_file(music_sheet, int.Parse(bpm_inputbox.Text)))
                    {
                        this.is_proj_saved = true;
                        this.proj_save_path = saveLoadProj.File_path;
                        this.Title = saveLoadProj.Proj_name + Window_title_suffix;
                    }
                }
            }
            else if((e.Key == Key.W || e.Key == Key.Up) && chosen_block_list.Count != 0)
            {
                List<Note> target_note_list = new List<Note>();
                List<Note> origin_note_list = new List<Note>();
                foreach(var rec in chosen_block_list)
                {
                    origin_note_list.Add(make_note(rec));
                }
                foreach(var note in origin_note_list)
                {
                    Note target_note = new Note(note.Begin_bar_idx, note.Semi_offset, note.Key_idx - 1, note.Continuous_semi, note.End_bar_idx, beats_per_bar, note.get_music_rectangle_block());
                    target_note_list.Add(target_note);
                }
                if(music_sheet.move_notes(origin_note_list, target_note_list))
                {
                    this.is_proj_saved = false;
                    for (int i = 0; i < chosen_block_list.Count; i++)
                    {
                        chosen_block_list[i].Margin = new Thickness(chosen_block_list[i].Margin.Left,
                                                            chosen_block_list[i].Margin.Top - basic_attribution.White_key_height,
                                                            0,0);
                    }
                }
            }
            else if ((e.Key == Key.S || e.Key == Key.Down) && chosen_block_list.Count != 0)
            {
                List<Note> target_note_list = new List<Note>();
                List<Note> origin_note_list = new List<Note>();
                foreach (var rec in chosen_block_list)
                {
                    origin_note_list.Add(make_note(rec));
                }
                foreach (var note in origin_note_list)
                {
                    Note target_note = new Note(note.Begin_bar_idx, note.Semi_offset, note.Key_idx + 1, note.Continuous_semi, note.End_bar_idx, beats_per_bar, note.get_music_rectangle_block());
                    target_note_list.Add(target_note);
                }
                if (music_sheet.move_notes(origin_note_list, target_note_list))
                {
                    this.is_proj_saved = false;
                    for (int i = 0; i < chosen_block_list.Count; i++)
                    {
                        chosen_block_list[i].Margin = new Thickness(chosen_block_list[i].Margin.Left,
                                                            chosen_block_list[i].Margin.Top + basic_attribution.White_key_height,
                                                            0, 0);
                    }
                }
            }
            else if ((e.Key == Key.A || e.Key == Key.Left) && chosen_block_list.Count != 0)
            {
                List<Note> target_note_list = new List<Note>();
                List<Note> origin_note_list = new List<Note>();
                foreach (var rec in chosen_block_list)
                {
                    origin_note_list.Add(make_note(rec));
                }
                foreach (var note in origin_note_list)
                {
                    Note target_note = new Note(note.Begin_bar_idx, note.Semi_offset, note.Key_idx, note.Continuous_semi, note.End_bar_idx, beats_per_bar, note.get_music_rectangle_block()); ;
                    if(note.Semi_offset == 0)
                    {
                        target_note.Begin_bar_idx -= 1;
                        target_note.Semi_offset = beats_per_bar * 4 - 1;
                        target_note.Absolute_semi_offset -= 1;
                    }
                    else
                    {
                        target_note.Semi_offset -= 1;
                        target_note.Absolute_semi_offset -= 1;
                    }
                    if((note.Absolute_semi_offset + note.Continuous_semi) % (beats_per_bar * 4) == 1)
                    {
                        target_note.End_bar_idx -= 1;
                    }
                    
                    target_note_list.Add(target_note);
                }
                if (music_sheet.move_notes(origin_note_list, target_note_list))
                {
                    this.is_proj_saved = false;
                    for (int i = 0; i < chosen_block_list.Count; i++)
                    {
                        if(target_note_list[i].Begin_bar_idx != origin_note_list[i].Begin_bar_idx)
                        {
                            int bar_idx = (int)chosen_block_list[i].Tag;
                            ((Canvas)bar_dock_panel.Children[bar_idx-1]).Children.Remove(chosen_block_list[i]);
                            ((Canvas)bar_dock_panel.Children[bar_idx - 2]).Children.Add(chosen_block_list[i]);

                            chosen_block_list[i].Margin = new Thickness((beats_per_bar * 4 - 1) * basic_attribution.Semiquaver_width,
                                                            chosen_block_list[i].Margin.Top,
                                                            0, 0);
                            chosen_block_list[i].Tag = bar_idx - 1;
                        }
                        else
                        {
                            chosen_block_list[i].Margin = new Thickness(chosen_block_list[i].Margin.Left - basic_attribution.Semiquaver_width,
                                                            chosen_block_list[i].Margin.Top,
                                                            0, 0);
                        }
                    }
                }

            }
            else if ((e.Key == Key.D || e.Key == Key.Right) && chosen_block_list.Count != 0)
            {
                List<Note> target_note_list = new List<Note>();
                List<Note> origin_note_list = new List<Note>();
                foreach (var rec in chosen_block_list)
                {
                    origin_note_list.Add(make_note(rec));
                }
                foreach (var note in origin_note_list)
                {
                    Note target_note = new Note(note.Begin_bar_idx, note.Semi_offset, note.Key_idx, note.Continuous_semi, note.End_bar_idx, beats_per_bar, note.get_music_rectangle_block()); ;
                    if (note.Semi_offset == beats_per_bar * 4 - 1)
                    {
                        target_note.Begin_bar_idx += 1;
                        target_note.Semi_offset = 0;
                        target_note.Absolute_semi_offset += 1;
                    }
                    else
                    {
                        target_note.Semi_offset += 1;
                        target_note.Absolute_semi_offset += 1;
                    }
                    if ((note.Absolute_semi_offset + note.Continuous_semi) % (beats_per_bar * 4) == 0)
                    {
                        target_note.End_bar_idx += 1;
                        if(target_note.End_bar_idx > bar_num)
                        {
                            return;
                        }
                    }

                    target_note_list.Add(target_note);
                }
                if (music_sheet.move_notes(origin_note_list, target_note_list))
                {
                    this.is_proj_saved = false;
                    for (int i = 0; i < chosen_block_list.Count; i++)
                    {
                        if (target_note_list[i].Begin_bar_idx != origin_note_list[i].Begin_bar_idx)
                        {
                            int bar_idx = (int)chosen_block_list[i].Tag;
                            
                            ((Canvas)bar_dock_panel.Children[bar_idx - 1]).Children.Remove(chosen_block_list[i]);
                            ((Canvas)bar_dock_panel.Children[bar_idx]).Children.Add(chosen_block_list[i]);

                            chosen_block_list[i].Margin = new Thickness(0,
                                                            chosen_block_list[i].Margin.Top,
                                                            0, 0);
                            chosen_block_list[i].Tag = bar_idx + 1;
                        }
                        else
                        {
                            chosen_block_list[i].Margin = new Thickness(chosen_block_list[i].Margin.Left + basic_attribution.Semiquaver_width,
                                                            chosen_block_list[i].Margin.Top,
                                                            0, 0);
                        }
                    }
                }

            }
            else if(piano_grid.Children.Count > 0)
            {
                // 第一排
                if (e.Key == Key.U && is_play_key_down['u' - 'a'] == false)
                {
                    is_play_key_down['u' - 'a'] = true;
                    midiplayer.play_on_keyboard(0, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.Y && is_play_key_down['y' - 'a'] == false)
                {
                    is_play_key_down['y' - 'a'] = true;
                    midiplayer.play_on_keyboard(1, basic_attribution.Eight_degree_num);

                }
                else if (e.Key == Key.T && is_play_key_down['t' - 'a'] == false)
                {
                    is_play_key_down['t' - 'a'] = true;
                    midiplayer.play_on_keyboard(2, basic_attribution.Eight_degree_num);

                }
                else if (e.Key == Key.R && is_play_key_down['r' - 'a'] == false)
                {
                    is_play_key_down['r' - 'a'] = true;
                    midiplayer.play_on_keyboard(3, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.E && is_play_key_down['e' - 'a'] == false)
                {
                    is_play_key_down['e' - 'a'] = true;
                    midiplayer.play_on_keyboard(4, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.W && is_play_key_down['w' - 'a'] == false)
                {
                    is_play_key_down['w' - 'a'] = true;
                    midiplayer.play_on_keyboard(5, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.Q && is_play_key_down['q' - 'a'] == false)
                {
                    is_play_key_down['q' - 'a'] = true;
                    midiplayer.play_on_keyboard(6, basic_attribution.Eight_degree_num);
                }
                // 第二排
                else if (e.Key == Key.J && is_play_key_down['j' - 'a'] == false)
                {
                    is_play_key_down['j' - 'a'] = true;
                    midiplayer.play_on_keyboard(7, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.H && is_play_key_down['h' - 'a'] == false)
                {
                    is_play_key_down['h' - 'a'] = true;
                    midiplayer.play_on_keyboard(8, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.G && is_play_key_down['g' - 'a'] == false)
                {
                    is_play_key_down['g' - 'a'] = true;
                    midiplayer.play_on_keyboard(9, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.F && is_play_key_down['f' - 'a'] == false)
                {
                    is_play_key_down['f' - 'a'] = true;
                    midiplayer.play_on_keyboard(10, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.D && is_play_key_down['d' - 'a'] == false)
                {
                    is_play_key_down['d' - 'a'] = true;
                    midiplayer.play_on_keyboard(11, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.S && is_play_key_down['s' - 'a'] == false)
                {
                    is_play_key_down['s' - 'a'] = true;
                    midiplayer.play_on_keyboard(12, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.A && is_play_key_down['a' - 'a'] == false)
                {
                    is_play_key_down['a' - 'a'] = true;
                    midiplayer.play_on_keyboard(13, basic_attribution.Eight_degree_num);
                }
                // 第三排
                else if (e.Key == Key.M && is_play_key_down['m' - 'a'] == false)
                {
                    is_play_key_down['m' - 'a'] = true;
                    midiplayer.play_on_keyboard(14, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.N && is_play_key_down['n' - 'a'] == false)
                {
                    is_play_key_down['n' - 'a'] = true;
                    midiplayer.play_on_keyboard(15, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.B && is_play_key_down['b' - 'a'] == false)
                {
                    is_play_key_down['b' - 'a'] = true;
                    midiplayer.play_on_keyboard(16, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.V && is_play_key_down['v' - 'a'] == false)
                {
                    is_play_key_down['v' - 'a'] = true;
                    midiplayer.play_on_keyboard(17, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.C && is_play_key_down['c' - 'a'] == false)
                {
                    is_play_key_down['c' - 'a'] = true;
                    midiplayer.play_on_keyboard(18, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.X && is_play_key_down['x' - 'a'] == false)
                {
                    is_play_key_down['x' - 'a'] = true;
                    midiplayer.play_on_keyboard(19, basic_attribution.Eight_degree_num);
                }
                else if (e.Key == Key.Z && is_play_key_down['z' - 'a'] == false)
                {
                    is_play_key_down['z' - 'a'] = true;
                    midiplayer.play_on_keyboard(20, basic_attribution.Eight_degree_num);
                }
            }
           
        }

        private void window_key_up_handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shift_down = false;
            }
            else if(e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                ctrl_down = false;
            }
            // 第一排
            else if (e.Key == Key.U)
            {
                is_play_key_down['u' - 'a'] = false;
            }
            else if (e.Key == Key.Y)
            {
                is_play_key_down['y' - 'a'] = false;

            }
            else if (e.Key == Key.T)
            {
                is_play_key_down['t' - 'a'] = false;

            }
            else if (e.Key == Key.R)
            {
                is_play_key_down['r' - 'a'] = false;
            }
            else if (e.Key == Key.E)
            {
                is_play_key_down['e' - 'a'] = false;
            }
            else if (e.Key == Key.W)
            {
                is_play_key_down['w' - 'a'] = false;
            }
            else if (e.Key == Key.Q)
            {
                is_play_key_down['q' - 'a'] = false;
            }
            // 第二排
            else if (e.Key == Key.J)
            {
                is_play_key_down['j' - 'a'] = false;
            }
            else if (e.Key == Key.H)
            {
                is_play_key_down['h' - 'a'] = false;
            }
            else if (e.Key == Key.G)
            {
                is_play_key_down['g' - 'a'] = false;
            }
            else if (e.Key == Key.F)
            {
                is_play_key_down['f' - 'a'] = false;
            }
            else if (e.Key == Key.D)
            {
                is_play_key_down['d' - 'a'] = false;
            }
            else if (e.Key == Key.S)
            {
                 is_play_key_down['s' - 'a'] = false;
            }
            else if (e.Key == Key.A)
            {
                is_play_key_down['a' - 'a'] = false;
            }
            // 第三排
            else if (e.Key == Key.M)
            {
                 is_play_key_down['m' - 'a'] = false;
            }
            else if (e.Key == Key.N)
            {
                  is_play_key_down['n' - 'a'] = false;
            }
            else if (e.Key == Key.B)
            {
               is_play_key_down['b' - 'a'] = false;
            }
            else if (e.Key == Key.V)
            {
                is_play_key_down['v' - 'a'] = false;
            }
            else if (e.Key == Key.C)
            {
                is_play_key_down['c' - 'a'] = false;
            }
            else if (e.Key == Key.X)
            {
                is_play_key_down['x' - 'a'] = false;
            }
            else if (e.Key == Key.Z)
            {
                is_play_key_down['z' - 'a'] = false;
            }
        }

        private void ver_scroll_changed(object sender, ScrollChangedEventArgs e)
        {
            var viewer = (ScrollViewer)sender;
            bar_lbl_dockpanel.Margin = new Thickness(0, viewer.VerticalOffset, 0, 0);
        }

        private void Add_btn_handler(object sender, RoutedEventArgs e)
        {
            if(music_sheet != null)
                add_bar();
        }

        private void Create_new_proj_btn_handler(object sender, RoutedEventArgs e)
        {
            if (piano_grid.Children.Count != 0)
            {
                if (is_proj_saved == false)
                {
                    var result = MessageBox.Show("当前工程尚未保存，是否确认关闭？",
                            "警告",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }
                piano_grid.Children.Clear();
                bar_lbl_dockpanel.Children.Clear();
                bar_dock_panel.Children.Clear();
                app_data_init();
            }
            else
            {
                piano_keyboard_init();
                switch (temple_type_combobox.SelectedIndex)
                {
                    case 0:
                        this.beats_per_bar = 4;
                        break;
                    case 1:
                        this.beats_per_bar = 3;
                        break;
                    case 2:
                        this.beats_per_bar = 2;
                        break;
                }
                temple_type_combobox.IsEnabled = false;

                this.music_sheet = new MusicSheet(basic_attribution, beats_per_bar);

            }


        }

        private void BPMTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char val = e.Text[0];
            if (val >= '0' && val <= '9')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void BPMLoseFocus(object sender, RoutedEventArgs e)
        {
            var txt_box = sender as TextBox;
            if (txt_box.IsFocused == false)
            {
                if (txt_box.Text.Length == 0)
                {
                    txt_box.Text = "75";
                }
                else if (int.Parse(txt_box.Text) < 10)
                {
                    txt_box.Text = "10";
                }
                else if (int.Parse(txt_box.Text) > 400)
                {
                    txt_box.Text = "400";
                }
            }
        }


        private void Play_btn_click(object sender, RoutedEventArgs e)
        {
            // 避免空工程时按下按钮
            if(music_sheet == null)
            {
                return;
            }
            switch (midiplayer.current_status)
            {
                case MIDIPlayer.PlayStatus.STOP:
                    int start_semi = int.MaxValue;
                    if(chosen_block_list.Count == 0)
                    {
                        start_semi = 0;
                    }
                    else
                    {
                        for (int i = 0; i < chosen_block_list.Count; i++)
                        {
                            var note = make_note(chosen_block_list[i]);
                            start_semi = Math.Min(start_semi, note.Absolute_semi_offset);
                        }
                    }
                    
                    midiplayer.set_midi_player(music_sheet.Music_sheet, int.Parse(bpm_inputbox.Text), start_semi, 3);
                    music_play_btn.Content = "暂停";
                    midiplayer.start_play();
                    break;
                case MIDIPlayer.PlayStatus.PAUSE:
                    music_play_btn.Content = "暂停";
                    midiplayer.continue_play();
                    break;
                case MIDIPlayer.PlayStatus.PLAYING:
                    music_play_btn.Content = "播放";
                    midiplayer.pause_play();
                    break;
            }

        }
        private void Stop_btn_handler(object sender, RoutedEventArgs e)
        {
            midiplayer.stop_play();
            music_play_btn.Content = "Play";
        }

        private void Delete_bar_handler(object sender, RoutedEventArgs e)
        {
            int idx = bar_dock_panel.Children.Count - 1;
            if(idx < 0)
            {

                MessageBox.Show("当前已无小节可删除",
                                       "提示",
                                       MessageBoxButton.OK,
                                       MessageBoxImage.Information);
            }
            else if (end_bar_count_list[idx] == 0)
            {
                bar_dock_panel.Children.RemoveAt(idx);
                end_bar_count_list.RemoveAt(idx);
                bar_lbl_dockpanel.Children.RemoveAt(idx);
                bar_num--;


                
            }
            else
            {
                MessageBox.Show("末尾小节中有音符块存在，无法删除",
                                       "提示",
                                       MessageBoxButton.OK,
                                       MessageBoxImage.Information);
            }
        }




        private void mouse_up_music_block_handler(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            bool existed_flag = false;
            if (chosen_block_list.FindIndex(a => a == rectangle) != -1)
            {
                existed_flag = true;
            }
            if (ctrl_down == false)
            {
                clear_chosen_condition();
            }

            if (!existed_flag)
            {
                set_rectangle_binding(ref basic_attribution, "Chosen_music_block_style", ref rectangle, Rectangle.StyleProperty);
                chosen_block_list.Add(rectangle);
                
            }
            else
            {
                set_rectangle_binding(ref basic_attribution, "Music_block_style", ref rectangle, Rectangle.StyleProperty);
                chosen_block_list.Remove(rectangle);
            }

        }

        /// <summary>
        /// 根据rec生成note
        /// </summary>
        /// <param name="rectangle">已经完成的方块</param>
        /// <returns></returns>
        private Note make_note(Rectangle rectangle)
        {
            int begin_bar = (int)rectangle.Tag;
            int semi_offset = (int)(rectangle.Margin.Left / basic_attribution.Semiquaver_width);
            int continuous_semi = (int)(rectangle.Width / basic_attribution.Semiquaver_width);
            int key_idx = (int)(rectangle.Margin.Top / basic_attribution.White_key_height);
            int temp = beats_per_bar * 4 - semi_offset; // 计算距离当前小节尾的距离
            int cross_bar = 0;
            if (temp < continuous_semi)
            {
                temp = continuous_semi - temp; // 减去非整数小节
                cross_bar = temp / (beats_per_bar * 4) + 1;// 计算小节
                if (temp % (beats_per_bar * 4) == 0)
                {
                    cross_bar -= 1;
                }
            }
            Note note = new Note(begin_bar, semi_offset, key_idx, continuous_semi, begin_bar + cross_bar, beats_per_bar, rectangle);
      
            return note;
        }

        /// <summary>
        /// 向musicsheet和ui加入音符块
        /// </summary>
        private void add_note()
        {
            if (putting_music_block != null)
            {
                putting_music_block.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                putting_music_block.MouseUp += new MouseButtonEventHandler(mouse_up_music_block_handler);
                Note note = make_note(putting_music_block);
                if (this.music_sheet.add_note(note))
                {
                    end_bar_count_list[note.End_bar_idx-1]++;
                    is_proj_saved = false;
                }
                else
                {
                    Canvas canvas = (Canvas)bar_dock_panel.Children[(int)putting_music_block.Tag - 1];
                    canvas.Children.Remove(putting_music_block);
                }

            }
        }
        
        /// <summary>
        /// 鼠标按下时创建音符块；鼠标按下时清空选择的音符块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_down_handler(object sender, MouseButtonEventArgs e)
        {
            clear_chosen_condition();

            var rec = sender as Rectangle;
            int bar_idx = (int)rec.Tag;
            int key_idx = 0;
            Canvas canvas = (Canvas)bar_dock_panel.Children[bar_idx - 1];
            int note_idx = (int)(e.GetPosition(canvas).X / basic_attribution.Semiquaver_width);
            key_idx = (int)(e.GetPosition(canvas).Y / basic_attribution.White_key_height);
            if (putting_music_block != null && (putting_music_block.Width < basic_attribution.Semiquaver_width || Double.IsNaN(putting_music_block.Width)))
            {
                Canvas c = (Canvas)bar_dock_panel.Children[(int)putting_music_block.Tag - 1];
                c.Children.Remove(putting_music_block);
            }
            else if (putting_music_block != null)
            {
                add_note();
            }

            putting_music_block = new Rectangle();
            putting_music_block.Margin = new Thickness(note_idx * basic_attribution.Semiquaver_width, key_idx * basic_attribution.White_key_height, 0, 0);
            putting_music_block.Height = basic_attribution.White_key_height;
            putting_music_block.Width = 0;
            putting_music_block.MouseMove += new MouseEventHandler(mouse_over_handler);
            putting_music_block.MouseLeftButtonUp += new MouseButtonEventHandler(mouse_up_handler);
            putting_music_block.Tag = bar_idx;
            set_rectangle_binding(ref basic_attribution, "Music_block_style", ref putting_music_block, Rectangle.StyleProperty);
            putting_continuous_limit = music_sheet.get_putting_continuous_semi_limit(note_idx + (bar_idx - 1) * beats_per_bar * 4, key_idx);
            canvas.Children.Add(putting_music_block);

        }


        private void mouse_up_handler(object sender, MouseButtonEventArgs e)
        {

            if (putting_music_block != null && (putting_music_block.Width < basic_attribution.Semiquaver_width || Double.IsNaN(putting_music_block.Width)))
            {
                Canvas canvas = (Canvas)bar_dock_panel.Children[(int)putting_music_block.Tag - 1];
                canvas.Children.Remove(putting_music_block);
            }
            else if (putting_music_block != null)
            {
                add_note();
            }


            putting_music_block = null;

        }

        /// <summary>
        /// 鼠标悬浮时行提示；如果在创建音符块会使其拉长
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_over_handler(object sender, MouseEventArgs e)
        {
            var rec = sender as Rectangle;
            int bar_idx = (int)rec.Tag;

            var y = e.GetPosition((Canvas)bar_dock_panel.Children[bar_idx - 1]).Y;
            int key_idx = (int)(y / basic_attribution.White_key_height);

            if (putting_music_block != null)
            {
                Canvas canvas = (Canvas)bar_dock_panel.Children[bar_idx - 1];
                int origin_bar_idx = (int)putting_music_block.Tag;
                int cross_bar = bar_idx - origin_bar_idx;
                double compensation_x = 0.0;
                if(cross_bar < 0)
                {
                    putting_music_block.Width = 0;
                }
                else
                {
                    if (cross_bar > 0)
                    {
                        // 首先用于去除非整数小节部分
                        compensation_x = beats_per_bar * 4 * basic_attribution.Semiquaver_width - putting_music_block.Margin.Left;
                        cross_bar--;
                        // 若去除后还跨小节
                        while (cross_bar > 0)
                        {
                            compensation_x += beats_per_bar * 4 * basic_attribution.Semiquaver_width;
                            cross_bar--;
                        }
                    }

                    //e.GetPosition(canvas).X + (bar_idx - origin_bar_idx) * beats_per_bar * 4 * basic_attribution.Semiquaver_width - putting_music_block.Margin.Left
                    var x_offset = 0.0;
                    if (compensation_x == 0)
                    {
                        x_offset = Math.Max(0, e.GetPosition(canvas).X - putting_music_block.Margin.Left) + basic_attribution.Semiquaver_width / 2;
                    }
                    else
                    {
                        x_offset = Math.Max(0, compensation_x + e.GetPosition(canvas).X) + basic_attribution.Semiquaver_width / 2;
                    }

                    int true_offset = (int)(x_offset / basic_attribution.Semiquaver_width) * basic_attribution.Semiquaver_width;
                    if ((int)(x_offset / basic_attribution.Semiquaver_width) <= putting_continuous_limit)
                        putting_music_block.Width = true_offset;
                    else
                        putting_music_block.Width = putting_continuous_limit * basic_attribution.Semiquaver_width;

                }





            }

            // 避免重复绘制
            if (key_idx == pre_mouse_over_keyrow_idx && bar_idx == pre_mouse_over_bar_idx ||
                key_idx == pre_mouse_over_keyrow_idx && Math.Abs(bar_idx - pre_mouse_over_bar_idx) < 3)
            {
                return;
            }

            // 删除上一次的提示框
            for (int i = 0; i < mouse_over_canvas_list.Count; i++)
            {
                if(mouse_over_canvas_list[i] < bar_dock_panel.Children.Count)
                {
                    var c = (Canvas)bar_dock_panel.Children[mouse_over_canvas_list[i]];

                    c.Children.Remove(mouse_over_rec_list[i]);
                }
                
            }
            mouse_over_rec_list.Clear();
            mouse_over_canvas_list.Clear();

            // 处理
            double rec_width = beats_per_bar * 4 * basic_attribution.Semiquaver_width;
            for (int i = bar_idx - 1; i >= 0 && i > bar_idx - 8; i--)
            {
                Canvas canvas = (Canvas)bar_dock_panel.Children[i];
                Rectangle rectangle = new Rectangle();
                rectangle.Tag = i + 1;
                rectangle.SetValue(Rectangle.StyleProperty, Application.Current.Resources["hover_bar"]);
                rectangle.Margin = new Thickness(0, key_idx * basic_attribution.White_key_height, 0, 0);
                rectangle.Height = basic_attribution.White_key_height;
                rectangle.Width = rec_width;
                rectangle.MouseMove += new MouseEventHandler(mouse_over_handler);
                rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(mouse_down_handler);
                rectangle.MouseLeftButtonUp += new MouseButtonEventHandler(mouse_up_handler);
                canvas.Children.Add(rectangle);
                mouse_over_canvas_list.Add(i);
                mouse_over_rec_list.Add(rectangle);
            }
            for (int i = bar_idx; i < bar_dock_panel.Children.Count && i < bar_idx + 7; i++)
            {
                Canvas canvas = (Canvas)bar_dock_panel.Children[i];
                Rectangle rectangle = new Rectangle();
                rectangle.Tag = i + 1;
                rectangle.SetValue(Rectangle.StyleProperty, Application.Current.Resources["hover_bar"]);
                rectangle.Margin = new Thickness(0, key_idx * basic_attribution.White_key_height, 0, 0);
                rectangle.Height = basic_attribution.White_key_height;
                rectangle.Width = rec_width;
                rectangle.MouseMove += new MouseEventHandler(mouse_over_handler);
                rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(mouse_down_handler);
                rectangle.MouseLeftButtonUp += new MouseButtonEventHandler(mouse_up_handler);
                canvas.Children.Add(rectangle);
                mouse_over_canvas_list.Add(i);
                mouse_over_rec_list.Add(rectangle);
            }


            // 记录上次信息
            pre_mouse_over_bar_idx = bar_idx;
            pre_mouse_over_keyrow_idx = key_idx;

        }

        private void Save_btn_click(object sender, RoutedEventArgs e)
        {
            SaveLoadProj saveLoadProj = new SaveLoadProj();
            if(int.Parse(((MenuItem)sender).Tag.ToString()) == 1)
            {
                saveLoadProj.save_to_file(music_sheet, int.Parse(bpm_inputbox.Text));
            }
            else if (proj_save_path != null && proj_save_path != "")
            {
                if (saveLoadProj.save_to_existed_file(music_sheet, int.Parse(bpm_inputbox.Text), proj_save_path))
                {
                    this.is_proj_saved = true;
                }
            }
            else
            {
                if (saveLoadProj.save_to_file(music_sheet, int.Parse(bpm_inputbox.Text)))
                {
                    this.is_proj_saved = true;
                    this.proj_save_path = saveLoadProj.File_path;
                    this.Title = saveLoadProj.Proj_name + Window_title_suffix;
                }
            }
        }
        private void Load_btn_click(object sender, RoutedEventArgs e)
        {
            if (!is_proj_saved)
            {
                var result = MessageBox.Show("当前工程尚未保存，是否要关闭当前并加载其他工程？",
                        "警告",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Warning);
                if(result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            SaveLoadProj saveLoadProj = new SaveLoadProj();
            SaveFileTemplate save_file = saveLoadProj.load_file();
            if(save_file == null)
            {
                return;
            }
            app_data_init();
            this.proj_save_path = saveLoadProj.File_path;

            this.music_sheet = new MusicSheet(this.basic_attribution, save_file.Beats_per_bar);
            this.music_sheet.Music_sheet = save_file.Music_sheet;
            if (!music_sheet.from_music_sheet_load_notes_to_seq_list())
            {
                MessageBox.Show("工程文件音符块位置错误，无法加载！",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                return;
            }

            this.beats_per_bar = save_file.Beats_per_bar;
            switch (beats_per_bar)
            {
                case 4:
                    temple_type_combobox.SelectedIndex = 0;
                    break;
                case 3:
                    temple_type_combobox.SelectedIndex = 1;
                    break;
                case 2:
                    temple_type_combobox.SelectedIndex = 2;
                    break;
            }
            temple_type_combobox.IsEnabled = false;
            bpm_inputbox.Text = save_file.Bpm.ToString();
            this.Title = saveLoadProj.Proj_name + Window_title_suffix;

            // UI加载
            piano_grid.Children.Clear();
            piano_keyboard_init();
            foreach(Note note in save_file.Music_sheet)
            {
                var rec = new Rectangle();
                
                while(bar_dock_panel.Children.Count < note.End_bar_idx)
                {
                    add_bar();
                }
                Canvas canvas = (Canvas)bar_dock_panel.Children[note.Begin_bar_idx - 1];
                
                rec.Margin = new Thickness(note.Semi_offset * basic_attribution.Semiquaver_width, note.Key_idx * basic_attribution.White_key_height, 0, 0);
                rec.Height = basic_attribution.White_key_height;
                rec.Width = note.Continuous_semi * basic_attribution.Semiquaver_width;
                rec.MouseMove += new MouseEventHandler(mouse_over_handler);
                rec.MouseLeftButtonUp += new MouseButtonEventHandler(mouse_up_music_block_handler);
                rec.Tag = note.Begin_bar_idx;
                set_rectangle_binding(ref basic_attribution, "Music_block_style", ref rec, Rectangle.StyleProperty);

                rec.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                canvas.Children.Add(rec);
                note.set_music_rectangle_block(rec);
            }
            foreach(Note note in music_sheet.Music_sheet)
            {
                end_bar_count_list[note.End_bar_idx - 1]++;
            }
            this.is_proj_saved = true;
        }

        /// <summary>
        /// 删除所有选中方块
        /// </summary>
        private void delete_chosen_condition()
        {
            List<Note> delete_list = new List<Note>();

            foreach (var rec in chosen_block_list)
            {
                is_proj_saved = false;
                // 记录note
                delete_list.Add(make_note(rec)); 

                // 删去UI
                int canvas_idx = (int)rec.Tag - 1;
                Canvas canvas = (Canvas)bar_dock_panel.Children[canvas_idx];
                canvas.Children.Remove(rec);
                end_bar_count_list[delete_list[delete_list.Count - 1].End_bar_idx-1]--;
            }
            music_sheet.delete_notes(delete_list);
            chosen_block_list.Clear();
        }

        /// <summary>
        /// 将chosenlist中的所有选中方块恢复，同时清空选中
        /// </summary>
        private void clear_chosen_condition()
        {
            foreach (var rec in chosen_block_list)
            {
                
                if(rec != null)
                {
                    Rectangle rectangle = rec;
                    set_rectangle_binding(ref basic_attribution, "Music_block_style", ref rectangle, Rectangle.StyleProperty);
                }
                
            }
            chosen_block_list.Clear();
        }

        /// <summary>
        /// 清空选中的音符块
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_chosen_notes_mouse_down_handler(object sender, MouseButtonEventArgs e)
        {
            clear_chosen_condition();
            
        }

        private void Close_project(object sender, RoutedEventArgs e)
        {
            if (!is_proj_saved)
            {
                var result = MessageBox.Show("当前工程尚未保存，是否确认关闭？",
                        "警告",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Warning);
                if(result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            piano_grid.Children.Clear();
            app_data_init();
            //piano_keyboard_init();

        }

        private void Window_closing_handler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(is_proj_saved == false)
            {
                var result = MessageBox.Show("当前工程尚未保存，是否确认关闭？",
                        "警告",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Warning);
                if(result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            GlobalConfigController globalConfigController = new GlobalConfigController();
            globalConfigController.Theme = current_theme;
            globalConfigController.Window_size = current_window_size;
            globalConfigController.save_config();
        }

        private void Create_midi_file(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            string filename = "";
            dialog.Filter = "midi保存(*.midi)|*.midi";
            dialog.Title = "保存";
            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;
            }
            else
            {
                return;
            }
            if(midiplayer.create_midi_file(music_sheet.Music_sheet, int.Parse(bpm_inputbox.Text), filename, basic_attribution.Eight_degree_num))
            {
                MessageBox.Show("保存完成",
                        "提示",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("保存失败，查看log以获得详细信息",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            
        }

        private void change_size_btn_handler(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if((WINDOW_SIZE)mi.Tag == current_window_size)
            {
                return;
            }
            
            change_to_small_size_btn.Header = "小";
            change_to_norm_size_btn.Header = "普通";
            change_to_big_size_btn.Header = "大";

            switch ((WINDOW_SIZE)mi.Tag)
            {
                case WINDOW_SIZE.SMALL:
                    set_size(WINDOW_SIZE.SMALL);
                    change_to_small_size_btn.Header = "->" + change_to_small_size_btn.Header;
                    break;
                case WINDOW_SIZE.NORMAL:
                    set_size(WINDOW_SIZE.NORMAL);
                    change_to_norm_size_btn.Header = "->" + change_to_norm_size_btn.Header;
                    break;
                case WINDOW_SIZE.BIG:
                    set_size(WINDOW_SIZE.BIG);
                    change_to_big_size_btn.Header = "->" + change_to_big_size_btn.Header;
                    break;
            }
            current_window_size = (WINDOW_SIZE)mi.Tag;
            if(piano_grid.Children.Count <= 0)
            {
                return;
            }
            piano_grid.Children.Clear();
            piano_keyboard_init();
            // UI加载
            bar_dock_panel.Children.Clear();
            bar_lbl_dockpanel.Children.Clear();


            pre_mouse_over_bar_idx = -1;
            pre_mouse_over_keyrow_idx = -1;
            mouse_over_canvas_list = new List<int>();
            mouse_over_rec_list = new List<Rectangle>();
            putting_music_block = null;
            putting_continuous_limit = 0;
            chosen_block_list = new List<Rectangle>();
            end_bar_count_list = new List<int>();

            int bar_num_backup = bar_num;
            bar_num = 0;
            for (int i = 0; i < bar_num_backup; i++)
            {
                add_bar();
            }
            if(music_sheet != null)
            {
                foreach (Note note in music_sheet.Music_sheet)
                {
                    var rec = new Rectangle();

                    while (bar_dock_panel.Children.Count < note.End_bar_idx)
                    {
                        add_bar();
                    }
                    Canvas canvas = (Canvas)bar_dock_panel.Children[note.Begin_bar_idx - 1];

                    rec.Margin = new Thickness(note.Semi_offset * basic_attribution.Semiquaver_width, note.Key_idx * basic_attribution.White_key_height, 0, 0);
                    rec.Height = basic_attribution.White_key_height;
                    rec.Width = note.Continuous_semi * basic_attribution.Semiquaver_width;
                    rec.MouseMove += new MouseEventHandler(mouse_over_handler);
                    rec.MouseLeftButtonUp += new MouseButtonEventHandler(mouse_up_music_block_handler);
                    rec.Tag = note.Begin_bar_idx;
                    set_rectangle_binding(ref basic_attribution, "Music_block_style", ref rec, Rectangle.StyleProperty);

                    rec.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    note.set_music_rectangle_block(rec);
                    canvas.Children.Add(rec);
                }
            }
            

        }

        /// <summary>
        /// 更换主题配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void change_theme_click_handler(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BasicAttribution.THEME theme = (BasicAttribution.THEME)menuItem.Tag;
            if(theme == current_theme)
            {
                return;
            }
            basic_attribution.set_theme(theme);
            current_theme = theme;

            change_to_norm_theme_btn.Header = "普通";
            change_to_tianyi_theme_btn.Header = "洛天依";
            change_to_sakura_theme_btn.Header = "樱花";
            switch (theme)
            {
                case BasicAttribution.THEME.NORMAL:
                    change_to_norm_theme_btn.Header = "->" + change_to_norm_theme_btn.Header;
                    break;
                case BasicAttribution.THEME.TIAN_YI:
                    change_to_tianyi_theme_btn.Header = "->" + change_to_tianyi_theme_btn.Header;
                    break;
                case BasicAttribution.THEME.SAKURA:
                    change_to_sakura_theme_btn.Header = "->" + change_to_sakura_theme_btn.Header;
                    break;
            }
        }

        private void piano_mouse_down_handler(object sender, MouseButtonEventArgs e)
        {
            int key_idx = 0;
            if(sender is Rectangle)
            {
                var rec = sender as Rectangle;
                key_idx = (int)rec.Tag;
            }
            else if(sender is Grid)
            {
                var grid = sender as Grid;
                key_idx = (int)grid.Tag;
            }
            else
            {
                return;
            }
            //Console.WriteLine(key_idx);
            midiplayer.play_on_keyboard(key_idx, basic_attribution.Eight_degree_num);

        }

        private void Play_in_genshin_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "工程加载(*.genmujson)|*.genmujson";
            string filename = "";
            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;
            }
            else
            {
                return;
            }
         
            Process.Start(new ProcessStartInfo("cmd", "/c GenshinPlayer " + filename)
            {
                UseShellExecute = true,
            });
        }

        private void Save_img_btn_click(object sender, RoutedEventArgs e)
        {
            var g = new ImgGenerator();
            if (this.music_sheet == null || this.music_sheet.Music_sheet == null)
            {
                MessageBox.Show("该功能会把当前加载的工程输出为键盘谱，但是您还没有加载工程",
                           "警告",
                           MessageBoxButton.OK,
                           MessageBoxImage.Warning);
                return;
            }
            g.generate_img(this.music_sheet.Music_sheet, this.proj_save_path, this.bpm_inputbox.Text, this.beats_per_bar);


        }

    }
}
