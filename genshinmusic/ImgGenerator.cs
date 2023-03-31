using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;


namespace genshinmusic
{
    class ImgGenerator
    {
        /// <summary>
        /// 整个图像的主stackpanel
        /// </summary>
        StackPanel stackPanel = new StackPanel();
        /// <summary>
        /// 承载每一行的stackpanel
        /// </summary>
        StackPanel main_stack_panel = new StackPanel();
        /// <summary>
        /// 每一行中承载音符的stackpanel
        /// </summary>
        List<StackPanel> row_stack_panel_list = new List<StackPanel>();

        private int keyboard_lbl_width = 60;
        private int keyboard_lbl_font_size = 42;

        private char[] key_idx_to_char = new char[21]{
            'U',
            'Y',
            'T',
            'R',
            'E',
            'W',
            'Q',
            'J',
            'H',
            'G',
            'F',
            'D',
            'S',
            'A',
            'M',
            'N',
            'B',
            'V',
            'C',
            'X',
            'Z',
        };

        private int row_grid_top_height = 20;

        private Size img_size = new Size(1940,
                3310);

        /// <summary>
        /// 每一拍的音符数
        /// </summary>
        private int notes_per_row = 24;

        private int row_num_in_first_page = 5;
        private int row_num_in_ordinary_page = 6;

        // 一个音符块的参数

        private int note_height = 80;
        private int note_width = 70;
        private int note_bottom_height = 24;
        private int note_right_width = 15;
        private int note_margin_bot = 0;

        private int note_char_font_size = 50;
        private string note_font_family = "Courier New";

        

        private int note_num_in_one_note_grid = 5; // 一个note_grid中能防止多少个音符

        /// <summary>
        /// 重置grid，注意重置后还要手动插入行
        /// </summary>
        private void reset_grid()
        {
            stackPanel = new StackPanel();
            main_stack_panel = new StackPanel();
            main_stack_panel.Margin = new Thickness(100, 100, 100, 100);
            row_stack_panel_list = new List<StackPanel>();
        }


        /// <summary>
        /// 生成装载一个note的grid
        /// </summary>
        /// <returns>
        /// 顶部连接线区 (r, c) = (0, 0);
        /// 音符区 (r, c) = (0, 1);
        /// 底部下划线区 (r, c) = (1, 1);
        /// 右部附点区 (r, c) = (1, 1);
        /// 注意右部附点区应当设置内部置于底部</returns>
        private Grid make_notes_grid()
        {
            var note_grid = new Grid();
            note_grid.HorizontalAlignment = HorizontalAlignment.Center;
            note_grid.VerticalAlignment = VerticalAlignment.Center;
            note_grid.Width = this.note_width;

            // 顶部连线区 (r, c) = (0, 0)
            var row_def = new RowDefinition();
            row_def.Height = new GridLength(this.row_grid_top_height);
            note_grid.RowDefinitions.Add(row_def);

            // 音符区 (r, c) = (1, 0)
            row_def = new RowDefinition();
            row_def.Height = GridLength.Auto;
            note_grid.RowDefinitions.Add(row_def);

            // 底部下划线区 (r, c) = (2, 0)
            var row_def_bottom = new RowDefinition();
            row_def_bottom.Height = new GridLength(this.note_bottom_height);
            note_grid.RowDefinitions.Add(row_def_bottom);

            // 音符区
            var col_def = new ColumnDefinition();
            col_def.Width = new GridLength(this.note_width - this.note_right_width);
            note_grid.ColumnDefinitions.Add(col_def);

            // 右部附点区 (r, c) = (1, 1)
            col_def = new ColumnDefinition();
            col_def.Width = new GridLength(this.note_right_width);
            note_grid.ColumnDefinitions.Add(col_def);
            note_grid.VerticalAlignment = VerticalAlignment.Bottom;
            note_grid.Margin = new Thickness(0, 0, 0, this.note_margin_bot);
            return note_grid;
        }

        /// <summary>
        /// 生成一个音符grid，其中音符区已经设置
        /// </summary>
        /// <param name="note_list">要塞入的音符</param>
        /// <returns>顶部连接线区 (r, c) = (0, 0);
        /// 音符区 (r, c) = (0, 1);
        /// 底部下划线区 (r, c) = (1, 1);
        /// 右部附点区 (r, c) = (1, 1);
        /// 注意右部附点区应当设置内部置于底部</returns>
        private Grid make_note_text_block(List<Note>note_list)
        {
            Grid note_text_grid = make_notes_grid();
            var text_block = new TextBlock();
            text_block.FontFamily = new FontFamily(this.note_font_family);
            for(int i = 0; i < note_list.Count; i++)
            {
                text_block.Text += this.key_idx_to_char[note_list[i].Key_idx].ToString();
                if(i != note_list.Count - 1)
                {
                    text_block.Text += "\n";
                }
                text_block.FontSize = this.note_char_font_size;
                text_block.HorizontalAlignment = HorizontalAlignment.Center;
                text_block.VerticalAlignment = VerticalAlignment.Center;
            }
            note_text_grid.Children.Add(text_block);
            Grid.SetRow(text_block, 1);
            Grid.SetColumn(text_block, 0);

            //Border border = new Border();
            //border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            //note_text_grid.Children.Add(border);
            //Grid.SetRow(border, 0);
            //Grid.SetColumn(border, 1);

            //border = new Border();
            //border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            //note_text_grid.Children.Add(border);
            //Grid.SetRow(border, 1);
            //Grid.SetColumn(border, 0);
            return note_text_grid;
        }


        /// <summary>
        /// 生成一个音符grid，其中音符区已经设置
        /// </summary>
        /// <param name="note_list">要塞入的音符</param>
        /// <returns>音符区 (r, c) = (0, 0);
        /// 底部下划线区 (r, c) = (1, 0);
        /// 右部附点区 (r, c) = (1, 1);
        /// 注意右部附点区应当设置内部置于底部</returns>
        private Grid make_note_text_block(char note_ch)
        {
            Grid rest_note_text_grid = make_notes_grid();
            var text_block = new TextBlock();
            text_block.FontFamily = new FontFamily(this.note_font_family);
            text_block.Text = note_ch.ToString();
            text_block.FontSize = this.note_char_font_size;
            text_block.HorizontalAlignment = HorizontalAlignment.Center;
            text_block.VerticalAlignment = VerticalAlignment.Center;
            rest_note_text_grid.Children.Add(text_block);
            Grid.SetRow(text_block, 1);
            Grid.SetColumn(text_block, 0);
            return rest_note_text_grid;
        }

        /// <summary>
        /// 生成一个音符grid，其中音符区已经设置为休止符
        /// </summary>
        /// <returns>音符区 (r, c) = (0, 0);
        /// 底部下划线区 (r, c) = (1, 0);
        /// 右部附点区 (r, c) = (1, 1);
        /// 注意右部附点区应当设置内部置于底部</returns>
        private Grid make_rest_note_text_block()
        {
            Grid rest_note_text_grid = make_notes_grid();
            var text_block = new TextBlock();
            text_block.FontFamily = new FontFamily(this.note_font_family);
            text_block.Text = "0";
            text_block.FontSize = this.note_char_font_size;
            text_block.HorizontalAlignment = HorizontalAlignment.Center;
            text_block.VerticalAlignment = VerticalAlignment.Center;
            rest_note_text_grid.Children.Add(text_block);
            Grid.SetRow(text_block, 1);
            Grid.SetColumn(text_block, 0);
            return rest_note_text_grid;
        }

        /// <summary>
        /// 插入一行，同时插入stackpanel作为音符承载容器；
        /// </summary>
        /// <param name="row_stack_panel_list">记录插入的stackpanel，后续音符都应插入到这个list中</param>
        /// <returns></returns>
        private Grid make_row_grid(ref List<StackPanel> row_stack_panel_list)
        {
            var row_grid = new Grid();

            // top用于放置连接线
            //var top_row_def = new RowDefinition();
            //top_row_def.Height = new GridLength(this.row_grid_top_height);
            //row_grid.RowDefinitions.Add(top_row_def);

            var row_def = new RowDefinition();
            row_def.Height = new GridLength(this.note_height * this.note_num_in_one_note_grid + note_margin_bot);
            row_grid.RowDefinitions.Add(row_def);

            // 键盘标识
            var col_def = new ColumnDefinition();
            col_def.Width = new GridLength(this.keyboard_lbl_width);
            row_grid.ColumnDefinitions.Add(col_def);

            // 音符panel
            col_def = new ColumnDefinition();
            col_def.Width = new GridLength(note_width * notes_per_row);
            row_grid.ColumnDefinitions.Add(col_def);


            // 「键盘」标签
            var keyboard_txtbox = new TextBlock();
            keyboard_txtbox.FontSize = this.keyboard_lbl_font_size;
            keyboard_txtbox.Text = "键\n\n\n盘";
            keyboard_txtbox.HorizontalAlignment = HorizontalAlignment.Center;
            keyboard_txtbox.VerticalAlignment = VerticalAlignment.Bottom;
            row_grid.Children.Add(keyboard_txtbox);
            Grid.SetColumn(keyboard_txtbox, 0);
            Grid.SetRow(keyboard_txtbox, 0);

            // 所有音符摆放位置
            var stack_panel = new StackPanel();
            stack_panel.Orientation = Orientation.Horizontal;
            stack_panel.Width = note_width * notes_per_row;
            stack_panel.MinHeight = 100;
            stack_panel.MinWidth = 100;
            stack_panel.Height = note_height * 5;
            row_grid.Children.Add(stack_panel);
            Grid.SetColumn(stack_panel, 1);
            Grid.SetRow(stack_panel, 0);
            row_stack_panel_list.Add(stack_panel);



         
           
            return row_grid;
        }

        /// <summary>
        /// 插入下划线
        /// </summary>
        /// <param name="target_note_grid">目标音符grid，注意必须是该类函数中生成的grid</param>
        /// <param name="line_num">下划线数量，最低为1，最大为2</param>
        private void add_bottom_line(ref Grid target_note_grid, int line_num)
        {
            Line line = new Line();
            line.X1 = 0;
            line.X2 = this.note_width;
            line.Y1 = this.note_bottom_height / 3;
            line.Y2 = this.note_bottom_height / 3;
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 2;
            target_note_grid.Children.Add(line);
            Grid.SetRow(line, 2);
            Grid.SetColumn(line, 0);
            if (line_num > 1)
            {

                line = new Line();
                line.X1 = 0;
                line.X2 = this.note_width;
                line.Y1 = 2 * this.note_bottom_height / 3;
                line.Y2 = 2 * this.note_bottom_height / 3;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 2;
                target_note_grid.Children.Add(line);
                Grid.SetRow(line, 2);
                Grid.SetColumn(line, 0);
            }
        }
        

        private void add_addtional_dot(ref Grid target_note_grid)
        {
            int dotSize = 10;
            Ellipse currentDot = new Ellipse();
            currentDot.Stroke = new SolidColorBrush(Colors.Black);
            currentDot.StrokeThickness = 3;
            currentDot.Height = dotSize;
            currentDot.Width = dotSize;
            currentDot.Fill = new SolidColorBrush(Colors.Black);
            currentDot.Margin = new Thickness(-5,0,0,10);
            currentDot.HorizontalAlignment = HorizontalAlignment.Left;
            currentDot.VerticalAlignment = VerticalAlignment.Bottom;
            target_note_grid.Children.Add(currentDot);
            Grid.SetRow(currentDot, 1);
            Grid.SetColumn(currentDot, 1);
        }

        /// <summary>
        /// 添加垂直方向的连接线
        /// </summary>
        /// <param name="note">音符块grid</param>
        /// <param name="is_begin">true:开始音符; false:结束音符</param>
        private void add_vertical_line(ref Grid note, bool is_begin)
        {
            int x1 = (this.note_width - this.note_right_width) / 2;
            int x2 = x1;
            int y1 = this.row_grid_top_height;
            int y2 = this.row_grid_top_height / 2;
            Line line = new Line();
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 4;
            note.Children.Add(line);
            Grid.SetRow(line, 0);
            Grid.SetColumn(line, 0);

            if (is_begin)
            {
                x2 = this.note_width;
                y1 = y2;
            }
            else
            {
                x2 = 0;
                y1 = y2;
            }
            line = new Line();
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            line.StrokeThickness = 4;
            line.Stroke = new SolidColorBrush(Colors.Black);
            note.Children.Add(line);
            Grid.SetRow(line, 0);
            Grid.SetColumn(line, 0);
        }

        /// <summary>
        /// 添加水平方向的连接线
        /// </summary>
        /// <param name="note">音符块grid</param>
        private void add_horizontal_line(ref Grid note)
        {
            int x1 = 0;
            int x2 = this.note_width;
            int y1 = this.row_grid_top_height / 2;
            int y2 = y1;
            Line line = new Line();
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            line.StrokeThickness = 4;
            line.Stroke = new SolidColorBrush(Colors.Black);
            note.Children.Add(line);
            Grid.SetRow(line, 0);
            Grid.SetColumn(line, 0);
        }

        /// <summary>
        /// 把所有全保存到1页
        /// </summary>
        private void save_all_in_one_page(string folder_path, TextBlock head, TextBlock config_info)
        {
            string filename = System.IO.Path.Combine(folder_path, "1.jpg");

            stackPanel.Children.Add(head);
            stackPanel.Children.Add(config_info);
            stackPanel.Children.Add(main_stack_panel);
            stackPanel.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));


            stackPanel.Measure(this.img_size);
            stackPanel.Arrange(new Rect(this.img_size));

            // Render control to an image
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)stackPanel.ActualWidth, (int)stackPanel.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(stackPanel);



            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var file = File.OpenWrite(filename))
            {
                encoder.Save(file);
            }

        }

        /// <summary>
        /// 保存第一页
        /// </summary>
        private void save_first_page(string folder_path, TextBlock head, TextBlock config_info )
        {
            string filename = System.IO.Path.Combine(folder_path, "1.jpg");

            var page_text = new TextBlock();
            page_text.HorizontalAlignment = HorizontalAlignment.Center;
            page_text.VerticalAlignment = VerticalAlignment.Bottom;
            page_text.FontFamily = new FontFamily(this.note_font_family);
            page_text.Text = "1";
            page_text.FontSize = 45;
            page_text.Margin = new Thickness(0, 50, 0, 0);

            stackPanel.Children.Add(head);
            stackPanel.Children.Add(config_info);
            stackPanel.Children.Add(main_stack_panel);
            stackPanel.Children.Add(page_text);
            stackPanel.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));


  
            stackPanel.Measure(this.img_size);
            stackPanel.Arrange(new Rect(this.img_size));

            // Render control to an image
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)stackPanel.ActualWidth, (int)stackPanel.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(stackPanel);

            FormatConvertedBitmap convertImg = new FormatConvertedBitmap(rtb, PixelFormats.Gray8, null, 0);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(convertImg));

            using (var file = File.OpenWrite(filename))
            {
                encoder.Save(file);
            }

        }

        private void save_img_with_page(string folder_path, int page_num)
        {
            string filename = System.IO.Path.Combine(folder_path, page_num.ToString() + ".jpg");
            var page_text = new TextBlock();
            page_text.HorizontalAlignment = HorizontalAlignment.Center;
            page_text.VerticalAlignment = VerticalAlignment.Bottom;
            page_text.FontFamily = new FontFamily(this.note_font_family);
            page_text.Text = page_num.ToString();
            page_text.FontSize = 45;
            page_text.Margin = new Thickness(0, 50, 0, 0);
            
            stackPanel.Children.Add(main_stack_panel);
            stackPanel.Children.Add(page_text);
            stackPanel.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));


            stackPanel.Measure(this.img_size);
            stackPanel.Arrange(new Rect(this.img_size));

            // Render control to an image
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)stackPanel.ActualWidth, (int)stackPanel.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(stackPanel);
            FormatConvertedBitmap convertImg = new FormatConvertedBitmap(rtb, PixelFormats.Gray8, null, 0);


            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(convertImg));


            using (var file = File.OpenWrite(filename))
            {
                encoder.Save(file);
            }
        }

        public void generate_img(List<Note> music_sheet, string title, string bpm, int beats_per_bar)
        {
            if(music_sheet == null)
            {
                return;
            }
            // 单一文件的情况
            //SaveFileDialog dialog = new SaveFileDialog();
            //title = System.IO.Path.GetFileNameWithoutExtension(title);
            //string filename = "";
            //dialog.Filter = "乐谱图片保存(*.jpg)|*.jpg";
            //dialog.Title = "选择乐谱图片保存文件";
            //dialog.FileName = title;
            //if (dialog.ShowDialog() == true)
            //{
            //    Console.WriteLine(dialog.FileName);
            //    filename = dialog.FileName;
            //    if (filename == "")
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    return;
            //}

            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            string folder_path = "";
            folderBrowserDialog.Description = "选择要导出的文件夹";
            if(folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folder_path = folderBrowserDialog.SelectedPath;
            }
            else
            {
                return;
            }

            reset_grid();
            int page = 1;
            if(title != "" && title != null)
            {
                title = System.IO.Path.GetFileNameWithoutExtension(title);
            }
            else
            {
                title = System.IO.Path.GetFileNameWithoutExtension(folder_path);
            }
            int temp_dir_num = 1;
            string target_folder_path = System.IO.Path.Combine(folder_path, title);
            while (Directory.Exists(target_folder_path))
            {
                target_folder_path = System.IO.Path.Combine(folder_path, title + " (" + temp_dir_num.ToString() + ")");
                temp_dir_num++;
            }
            Directory.CreateDirectory(target_folder_path);
            folder_path = target_folder_path;
           
            var head = new TextBlock();
            // top 100  bot 100 height 80
            head.Margin = new Thickness(100, 100, 100, 0);
            head.Height = 110;
            head.TextWrapping = TextWrapping.Wrap;
            head.FontWeight = FontWeights.Bold;
            head.FontSize = 71;
            head.TextAlignment = TextAlignment.Center;
            head.VerticalAlignment = VerticalAlignment.Center;
            head.HorizontalAlignment = HorizontalAlignment.Center;
            head.TextTrimming = TextTrimming.CharacterEllipsis;
            head.Text = title;

            var config_info = new TextBlock();
            config_info.FontSize = 50;
            config_info.Margin = new Thickness(100, 100, 100, 0);
            config_info.Text = "曲速：" + bpm + "  " + 
                                "拍号：" + beats_per_bar.ToString() + "/4"+ "  " + "总音符量：" + music_sheet.Count;
            config_info.Height = 60;

            
            
            
         
            // 无小节设置
            main_stack_panel.Children.Add(this.make_row_grid(ref row_stack_panel_list));

            int current_semi = 0; // 当前正准备插入的音符的绝对时间偏移

            
            int remain_vacancy_num = this.notes_per_row;// 一排还可以放置的的音符数
            int row_idx = 0; // 正在插入的音符对应的行号

            // 塞入音符

            
            
            for (int note_idx = 0 ; note_idx < music_sheet.Count ; note_idx++)
            {
                int duration = 0; // 还需要插入的剩余音符时值
                bool need_connect_sign = false; // 控制是否需要连接线
                var note = music_sheet[note_idx];
                int max_duration_time = note.Continuous_semi;
                int start_semi = note.Absolute_semi_offset;
                List<Note> note_list = new List<Note>();

                for(int i = note_idx; i < music_sheet.Count && music_sheet[i].Absolute_semi_offset == note.Absolute_semi_offset; i++)
                {
                    note_list.Add(music_sheet[i]);
                    note_idx = i;
                    max_duration_time = Math.Max(music_sheet[i].Continuous_semi, max_duration_time);
                }
                note_list.Sort(
                    (x, y) =>
                    {
                        if (x.Key_idx > y.Key_idx)
                        {
                            return 1;
                        }
                        return -1;
                    });
                if (note_idx + 1 < music_sheet.Count)
                {
                    int next_start = music_sheet[note_idx + 1].Absolute_semi_offset;
                    max_duration_time = Math.Min(max_duration_time, next_start - current_semi);
                }
                if(current_semi < start_semi)
                {
/******************** 插入休止 ************************/
                    // TODO: 添加休止
                    duration = start_semi - current_semi;
                    
                    // 控制是否需要连接线
                    need_connect_sign = false;
                    while (duration > 0)
                    {
                        var start_rest_text_box = this.make_rest_note_text_block();
                        if (remain_vacancy_num == 0)
                        {
                            remain_vacancy_num = this.notes_per_row;
                            row_idx++;
                            if(page == 1 && row_idx > row_num_in_first_page)
                            {
                                save_first_page(folder_path, head, config_info);
                                row_idx = 0;
                                reset_grid();
                                page++;
                            }
                            else if( page > 1 && row_idx > row_num_in_ordinary_page)
                            {
                                save_img_with_page(folder_path, page);
                                row_idx = 0;
                                reset_grid();
                                page++;
                            }
                            main_stack_panel.Children.Add(this.make_row_grid(ref row_stack_panel_list));
                        }
                        switch (duration)
                        {
                            case 1:
                                if (need_connect_sign)
                                {
                                    this.add_vertical_line(ref start_rest_text_box, false);
                                }
                                add_bottom_line(ref start_rest_text_box, 2);
                                duration -= 1;
                                break;
                            case 2:
                                if (need_connect_sign)
                                {
                                    this.add_vertical_line(ref start_rest_text_box, false);
                                }
                                add_bottom_line(ref start_rest_text_box, 1);
                                duration -= 2;
                                break;
                            case 3:
                                if (need_connect_sign)
                                {
                                    this.add_vertical_line(ref start_rest_text_box, false);
                                }
                                add_bottom_line(ref start_rest_text_box, 2);
                                add_addtional_dot(ref start_rest_text_box);
                                duration -= 3;
                                break;
                            case 4:
                                duration -= 4;
                                break;
                            case 6:
                                if (need_connect_sign)
                                {
                                    this.add_vertical_line(ref start_rest_text_box, false);
                                }
                                add_addtional_dot(ref start_rest_text_box);
                                duration -= 6;
                                break;
                            default:
                                // 后续都用 - 表示
                                // 如果duration是4的倍数，直接自己内部循环退出
                                if(duration % 4 == 0)
                                {
                                    duration -= 4;
                                    // 直接循环输入 - 并跳出
                                    row_stack_panel_list[row_idx].Children.Add(start_rest_text_box);
                                    remain_vacancy_num -= 1;


                                    while (duration != 0)
                                    {
                                        if (remain_vacancy_num == 0)
                                        {
                                            remain_vacancy_num = this.notes_per_row;
                                            row_idx++;
                                            if (page == 1 && row_idx > row_num_in_first_page)
                                            {
                                                save_first_page(folder_path, head, config_info);
                                                row_idx = 0;
                                                reset_grid();
                                                page++;
                                            }
                                            else if (page > 1 && row_idx > row_num_in_ordinary_page)
                                            {
                                                save_img_with_page(folder_path, page);
                                                row_idx = 0;
                                                reset_grid();
                                                page++;
                                            }
                                            main_stack_panel.Children.Add(this.make_row_grid(ref row_stack_panel_list));
                                        }
                                        start_rest_text_box = this.make_note_text_block('—');
                                        row_stack_panel_list[row_idx].Children.Add(start_rest_text_box);
                                        remain_vacancy_num -= 1;

                                        duration -= 4;
                                    }
                                    continue;
                                }
                                else
                                {
                                    if (!need_connect_sign)
                                    {
                                        need_connect_sign = true;
                                        this.add_vertical_line(ref start_rest_text_box,true);
                                    }
                                    else
                                    {
                                        this.add_horizontal_line(ref start_rest_text_box);
                                    }
                                   
                                    duration -= 4;
                                }
                                break;
                        }
                        row_stack_panel_list[row_idx].Children.Add(start_rest_text_box);
                        remain_vacancy_num -= 1;

                    }
                }
                
                

/******************** 插入音符 ************************/
                // 控制是否需要连接线
                need_connect_sign = false;
                duration = max_duration_time;
                while (duration > 0)
                {
                    var text_block = this.make_note_text_block(note_list);
                    if (remain_vacancy_num == 0)
                    {
                        remain_vacancy_num = this.notes_per_row;
                        row_idx++;
                        if (page == 1 && row_idx > row_num_in_first_page)
                        {
                            save_first_page(folder_path, head, config_info);
                            row_idx = 0;
                            reset_grid();
                            page++;
                        }
                        else if (page > 1 && row_idx > row_num_in_ordinary_page)
                        {
                            save_img_with_page(folder_path, page);
                            row_idx = 0;
                            reset_grid();
                            page++;
                        }
                        main_stack_panel.Children.Add(this.make_row_grid(ref row_stack_panel_list));
                    }
                    switch (duration)
                    {
                        case 1:
                            if (need_connect_sign)
                            {
                                this.add_vertical_line(ref text_block, false);
                            }
                            add_bottom_line(ref text_block, 2);
                            duration -= 1;
                            break;
                        case 2:
                            if (need_connect_sign)
                            {
                                this.add_vertical_line(ref text_block, false);
                            }
                            add_bottom_line(ref text_block, 1);
                            duration -= 2;
                            break;
                        case 3:
                            if (need_connect_sign)
                            {
                                this.add_vertical_line(ref text_block, false);
                            }
                            add_bottom_line(ref text_block,  2);
                            add_addtional_dot(ref text_block);
                            duration -= 3;
                            break;
                        case 4:
                            duration -= 4;
                            break;
                        case 6:
                            if (need_connect_sign)
                            {
                                this.add_vertical_line(ref text_block, false);
                            }
                            add_addtional_dot(ref text_block);
                            duration -= 6;
                            break;
                        default:
                            // 后续都用 - 表示
                            // 如果duration是4的倍数，直接自己内部循环退出
                            if (duration % 4 == 0)
                            {
                                duration -= 4;
                                // 直接循环输入 - 并跳出
                                row_stack_panel_list[row_idx].Children.Add(text_block);
                                remain_vacancy_num -= 1;

                                while (duration != 0)
                                {
                                    if (remain_vacancy_num == 0)
                                    {
                                        remain_vacancy_num = this.notes_per_row;
                                        row_idx++;
                                        if (page == 1 && row_idx > row_num_in_first_page)
                                        {
                                            save_first_page(folder_path, head, config_info);
                                            row_idx = 0;
                                            reset_grid();
                                            page++;
                                        }
                                        else if (page > 1 && row_idx > row_num_in_ordinary_page)
                                        {
                                            save_img_with_page(folder_path, page);
                                            row_idx = 0;
                                            reset_grid();
                                            page++;
                                        }
                                        main_stack_panel.Children.Add(this.make_row_grid(ref row_stack_panel_list));
                                    }
                                    text_block = this.make_note_text_block('—');
                                    row_stack_panel_list[row_idx].Children.Add(text_block);
                                    remain_vacancy_num -= 1;
           
                                    duration -= 4;
                                }
                                continue;
                            }
                            else
                            {
                                if (!need_connect_sign)
                                {
                                    need_connect_sign = true;
                                    this.add_vertical_line(ref text_block, true);
                                }
                                else
                                {
                                    this.add_horizontal_line(ref text_block);
                                }
                                duration -= 4;
                            }
                            break;
                    }
                    row_stack_panel_list[row_idx].Children.Add(text_block);
                    remain_vacancy_num -= 1;

                }

                // 更新current_semi，以更新下一组休止
                current_semi = start_semi + max_duration_time;

            }
            if((row_idx == 0 && remain_vacancy_num == this.notes_per_row) != true)
            {
                if(page != 1)
                {
                    this.save_img_with_page(folder_path, page);
                }
                else
                {
                    this.save_first_page(folder_path, head, config_info);
                }
                
            }
           
        }
       
    }
}
