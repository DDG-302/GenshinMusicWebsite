using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace genshinmusic
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 判断shift按键是否按下
        /// </summary>
        bool shift_down = false;

        /// <summary>
        /// 判断ctrl是否按下
        /// </summary>
        bool ctrl_down = false;

        /// <summary>
        /// 小节号
        /// </summary>
        int bar_num = 0;

        /// <summary>
        /// 记录上一次鼠标悬浮的小节位置
        /// </summary>
        int pre_mouse_over_bar_idx = -1;

        /// <summary>
        /// 上一次鼠标悬浮的琴键位置
        /// </summary>
        int pre_mouse_over_keyrow_idx = -1;

        /// <summary>
        /// 基础信息
        /// </summary>
        BasicAttribution basic_attribution;

        /// <summary>
        /// 当鼠标悬浮时，哪个canvas上放置了提示块
        /// </summary>
        List<int> mouse_over_canvas_list;
        /// <summary>
        /// 当鼠标悬浮时，提示块在canvas子控件中的索引
        /// </summary>
        List<Rectangle> mouse_over_rec_list;


        /// <summary>
        /// 正在放置的音符块
        /// </summary>
        Rectangle putting_music_block;

        int putting_continuous_limit;

        /// <summary>
        /// 每个小节的节拍数（四分音符数）
        /// </summary>
        int beats_per_bar;

        /// <summary>
        /// 当前乐谱
        /// </summary>
        MusicSheet music_sheet;

        MIDIPlayer midiplayer;

        /// <summary>
        /// 记录当前工程是否已经保存
        /// </summary>
        bool is_proj_saved = true;

        string proj_save_path; // 工程保存路径

        List<Rectangle> chosen_block_list; // 被单击选中的block



        /// <summary>
        /// 末尾小节计数，计数音符块末尾所在小节，为delete bar提供数据
        /// </summary>
        List<int> end_bar_count_list;

        WINDOW_SIZE current_window_size; // 小中大三个尺寸
        BasicAttribution.THEME current_theme;


        const string Window_title_suffix = " - DDGGenshinMusicCreator";


        List<bool> is_play_key_down;// 判断演奏时是否已经按下按键

        ///// <summary>
        ///// UI变换委托
        ///// </summary>
        //public delegate void Change_music_block_UI(List<Rectangle> target_block_list);

        ///// <summary>
        ///// 调用主线程恢复播放时变换的UI
        ///// </summary>
        //public event Change_music_block_UI reset_play_ui;

        ///// <summary>
        ///// 调用主线程变更播放时的UI
        ///// </summary>
        //public event Change_music_block_UI set_play_ui;

        public MainWindow()
        {
            InitializeComponent();




            GlobalConfigController globalConfigController = new GlobalConfigController();
            globalConfigController.load_config();
            basic_attribution = new BasicAttribution(26, 16, 20);
            set_size(globalConfigController.Window_size);
            basic_attribution.set_theme(globalConfigController.Theme);
            current_theme = globalConfigController.Theme;
            current_window_size = globalConfigController.Window_size;

            app_data_init();
            midiplayer = new MIDIPlayer();
            midiplayer.music_reach_to_final += Music_reach_to_end_event_handler;
            midiplayer.reset_play_ui += reset_UI_on_playing;
            midiplayer.set_play_ui += set_UI_on_playing;


            switch (current_window_size)
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
            switch (current_theme)
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
            change_to_big_size_btn.Tag = WINDOW_SIZE.BIG;
            change_to_norm_size_btn.Tag = WINDOW_SIZE.NORMAL;
            change_to_small_size_btn.Tag = WINDOW_SIZE.SMALL;

            change_to_norm_theme_btn.Tag = BasicAttribution.THEME.NORMAL;
            change_to_tianyi_theme_btn.Tag = BasicAttribution.THEME.TIAN_YI;
            change_to_sakura_theme_btn.Tag = BasicAttribution.THEME.SAKURA;

            is_play_key_down = new List<bool>();
            for (int i = 0; i < 26; i++)
            {
                is_play_key_down.Add(false);
            }
        }

        /// <summary>
        /// 窗体大小枚举类型，用于设置大小时传入
        /// </summary>
        public enum WINDOW_SIZE { SMALL, NORMAL, BIG };

        /// <summary>
        /// 修改控件尺寸，必须先移除所有控件才可以设置
        /// </summary>
        /// <param name="w_size"></param>
        private void set_size(WINDOW_SIZE w_size)
        {

            switch (w_size)
            {
                case WINDOW_SIZE.SMALL:
                    basic_attribution.set_size(20, 10, 15, 10);
                    break;
                case WINDOW_SIZE.NORMAL:
                    basic_attribution.set_size(26, 16, 20, 12);
                    break;
                case WINDOW_SIZE.BIG:
                    basic_attribution.set_size(32, 22, 25, 14);
                    break;
            }

        }

        /// <summary>
        /// 程序数据初始化，程序运行时的内存中如小节号、乐谱列表等数据的初始化
        /// </summary>
        public void app_data_init()
        {
            this.Title = "新工程" + Window_title_suffix;
            bar_dock_panel.Children.Clear();
            bar_lbl_dockpanel.Children.Clear();
            bar_num = 0;
            pre_mouse_over_bar_idx = -1;
            pre_mouse_over_keyrow_idx = -1;
            beats_per_bar = 4;
            mouse_over_canvas_list = new List<int>();
            mouse_over_rec_list = new List<Rectangle>();
            putting_music_block = null;
            music_sheet = null;
            temple_type_combobox.SelectedItem = 0;
            temple_type_combobox.IsEnabled = true;
            putting_continuous_limit = 0;
            is_proj_saved = true;
            proj_save_path = null;
            chosen_block_list = new List<Rectangle>();
            end_bar_count_list = new List<int>();

            
        }

        /// <summary>
        /// 设置绑定
        /// </summary>
        /// <param name="source">数据源，是一个basicattribution对象</param>
        /// <param name="path">数据值，与可读变量同名的字符串</param>
        /// <param name="rectangle">绑定对象</param>
        /// <param name="dependencyProperty">控件属性，是一个静态值</param>
        private void set_rectangle_binding(ref BasicAttribution source, string path,ref Rectangle rectangle,
         DependencyProperty dependencyProperty)
        {
            var rect = rectangle;
            var src = source;
            // 异步方法
            Action actiondelegate = () =>
            {
                Binding binding = new Binding();
                binding.Source = src;
                binding.Path = new PropertyPath(path);
                BindingOperations.SetBinding(rect, dependencyProperty, binding);
            };
            Dispatcher.BeginInvoke(actiondelegate);
            

        }

        /// <summary>
        /// 由piano_keyboard_init调用，辅助一个按键的生成
        /// </summary>
        /// <param name="offset">相对顶部偏移（margin top）</param>
        /// <param name="black_or_white">0：黑色，1/2：白色，3：C</param>
        /// /// <param name="key_idx">从上至下的白键键盘索引</param>
        /// <param name="lbl_str">只有在black_or_white为2时生效，记录C值</param>
        private void add_one_key(int offset, int black_or_white, int key_idx, string lbl_str="")
        {
            Rectangle rectangle = new Rectangle();
           

            if(black_or_white == 0)
            {
                rectangle.Height = basic_attribution.Black_key_height;
                set_rectangle_binding(ref basic_attribution, "Black_key_style", ref rectangle, Rectangle.StyleProperty);
            }
            else if(black_or_white == 1 || black_or_white == 2)
            {
               
                rectangle.Height = basic_attribution.White_key_height;
                set_rectangle_binding(ref basic_attribution, "White_key_style", ref rectangle, Rectangle.StyleProperty);
                rectangle.Tag = key_idx;
                rectangle.MouseDown += new MouseButtonEventHandler(piano_mouse_down_handler);
            }
            else if(black_or_white == 3)
            {
                Grid grid = new Grid();
                grid.Height = basic_attribution.White_key_height;
                grid.Margin = new Thickness(0, offset, 0, 0);
                grid.Tag = key_idx;
                grid.MouseDown += new MouseButtonEventHandler(piano_mouse_down_handler);
                Binding binding = new Binding();
                binding.Source = basic_attribution;
                binding.Path = new PropertyPath("C_key_style");
                BindingOperations.SetBinding(grid, Grid.StyleProperty, binding);

                rectangle.Height = basic_attribution.White_key_height;
                rectangle.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));  
                grid.Children.Add(rectangle);

                Label lbl = new Label();
                lbl.Content = lbl_str;
                lbl.FontSize = basic_attribution.Keyboard_font_size;
                //lbl.Margin = new Thickness(0, (double)offset, 0, 0);
                lbl.VerticalAlignment = VerticalAlignment.Top;
                lbl.HorizontalAlignment = HorizontalAlignment.Right;
                lbl.Width = basic_attribution.White_key_height + 5;
                lbl.Height = basic_attribution.White_key_height;
                lbl.SetValue(Panel.ZIndexProperty, 2);
                grid.Children.Add(lbl);
                piano_grid.Children.Add(grid);
            }
           
            if(black_or_white != 3)
            {
                rectangle.SetValue(Rectangle.MarginProperty, new Thickness(0, offset, 0, 0));
                piano_grid.Children.Add(rectangle);
            }
            
        }

        
        private void piano_keyboard_init()
        {
            // http://t.zoukankan.com/dotnetHui-p-8398814.html
            int global_key_offset = 0;
            int global_key_offset_addition = 7 * basic_attribution.White_key_height;
            // 0:白转黑
            // 1:黑转白
            // 2:白转白
            //                     
            int[] next_key = new int[11]{ 0,// 7-6# 
                                1, // 6#-6
                                0, // 6-5# 
                                1, // 5#-5
                                0, // 5-4#
                                1, // 4#-4
                                2, // 4-3
                                0, // 3-2#
                                1, // 2#-2
                                0, // 2-1#
                                1  // 1#-1

            };
            int key_idx = -1;
            for(int i = 0; i < basic_attribution.Eight_degree_num; i++)
            {
                key_idx++;
                int offset = global_key_offset;
                add_one_key(offset, 1, key_idx);
                
                
                for(int j = 0; j < 11; j++)
                {
                    switch (next_key[j])
                    {
                        case 0:
                            offset += basic_attribution.White_key_height - basic_attribution.Black_key_height / 2;
                            
                            break;
                        case 1:
                            offset += basic_attribution.Black_key_height / 2;
                            key_idx++;
                            break;
                        case 2:
                            offset += basic_attribution.White_key_height;
                            key_idx++;
                            break;
                    }
                 
                    if (j == 10)
                    {
                        add_one_key(offset, 3, key_idx, "C" + (5 - i).ToString());
                    }
                    else
                    {
                        add_one_key(offset, next_key[j], key_idx);
                    }

                    
                }
                global_key_offset += global_key_offset_addition;
            }

        }

        private void add_bar()
        {
            bar_num++;
            Canvas canvas = new Canvas();
            canvas.Width = beats_per_bar * 4 * basic_attribution.Semiquaver_width;
            canvas.Height = basic_attribution.Piano_key_board_height;
            canvas.SetValue(Canvas.TagProperty, bar_num);
            canvas.SetValue(Panel.ZIndexProperty, 65536 - bar_num);
            double col_height = basic_attribution.Piano_key_board_height;
            int offset = 0;

            int notes_in_bar = 4 * beats_per_bar;
            for (int col_num = 0; col_num < notes_in_bar; col_num++)
            {
                Rectangle rectangle = new Rectangle();
                if (col_num % 2 == 0)
                    set_rectangle_binding(ref basic_attribution, "Dark_bar_style", ref rectangle, Rectangle.StyleProperty);
                else
                    set_rectangle_binding(ref basic_attribution, "Light_bar_style", ref rectangle, Rectangle.StyleProperty);
                rectangle.Height = col_height;
                rectangle.Tag = bar_num;
                rectangle.Width = basic_attribution.Semiquaver_width;
                rectangle.Margin = new Thickness(offset, 0, 0, 0);
                rectangle.MouseMove += new MouseEventHandler(mouse_over_handler);
                if (col_num % 4 == 3 && col_num != 0 && col_num != notes_in_bar - 1)
                {
                    // 画每一拍的分割
                    Line line = new Line();
                    line.X1 = offset + rectangle.Width - 2;
                    line.Y1 = 0;
                    line.X2 = line.X1;
                    line.Y2 = basic_attribution.Piano_key_board_height;
                    line.Stroke = new SolidColorBrush(Color.FromRgb(25, 25, 122));
                    line.StrokeThickness = 4;
                    line.SetValue(Panel.ZIndexProperty, 2);
                    canvas.Children.Add(line);
                }

                if (col_num == notes_in_bar - 1)
                {
                    // 画小节线
                    Line line = new Line();
                    // 右上角
                    line.X1 = offset + rectangle.Width - 2;
                    line.Y1 = 0;

                    // 左下角
                    line.X2 = line.X1;
                    line.Y2 = basic_attribution.Piano_key_board_height;
                    line.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    line.StrokeThickness = 4;
                    line.SetValue(Panel.ZIndexProperty, 2);
                    canvas.Children.Add(line);
                }

                canvas.Children.Add(rectangle);

                offset += basic_attribution.Semiquaver_width;
            }
            for (int i = 0; i < basic_attribution.Eight_degree_num * 7; i++)
            {
                // 画行分割线
                Line line = new Line();
                line.X1 = 0;
                line.Y1 = i * basic_attribution.White_key_height;

                line.X2 = basic_attribution.Semiquaver_width * beats_per_bar * 4;
                line.Y2 = i * basic_attribution.White_key_height;
                line.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                line.StrokeThickness = 1;
                line.SetValue(Panel.ZIndexProperty, 1);
                canvas.Children.Add(line);
            }


            bar_dock_panel.Children.Add(canvas);

            // 更新小节号
            Label lbl = new Label();
            lbl.Content = bar_num.ToString();
            lbl.Width = basic_attribution.Semiquaver_width * notes_in_bar;
            lbl.SetValue(Label.StyleProperty, Application.Current.Resources["bar_lbl"]);
            bar_lbl_dockpanel.Children.Add(lbl);
            end_bar_count_list.Add(0);
        }


    




        private void Music_reach_to_end_event_handler()
        {
            Dispatcher.Invoke(() =>
            {
                music_play_btn.Content = "Play";

            });
          
            
        }

        /// <summary>
        /// 重置在播放时使用UI提示
        /// </summary>
        /// <param name="pre_block_list">上一次播放时颜色改变的音符块</param>
        private void reset_UI_on_playing(List<Rectangle> pre_block_list)
        {
            for (int k = 0; k < pre_block_list.Count; k++)
            {
                Rectangle rectangle = pre_block_list[k];
                bool existed_flag = false;
                if (chosen_block_list.FindIndex(a => a == rectangle) != -1)
                {
                    existed_flag = true;
                }
                if (!existed_flag)
                {
                    set_rectangle_binding(ref basic_attribution, "Music_block_style", ref rectangle, Rectangle.StyleProperty);
                }
                else
                {
                    set_rectangle_binding(ref basic_attribution, "Chosen_music_block_style", ref rectangle, Rectangle.StyleProperty);
                }

            }

        }

        /// <summary>
        /// 变换在播放时使用UI提示
        /// </summary>
        /// <param name="target_block_list">要改变的音符块</param>
        private void set_UI_on_playing(List<Rectangle> target_block_list)
        {
            for (int k = 0; k < target_block_list.Count; k++)
            {
                Rectangle rectangle = target_block_list[k];
                set_rectangle_binding(ref basic_attribution, "Musci_block_on_play_style", ref rectangle, Rectangle.StyleProperty);
            }

        }


        private void test_change_size(object sender, RoutedEventArgs e)
        {
            set_size(WINDOW_SIZE.BIG);
        }


    }
}
