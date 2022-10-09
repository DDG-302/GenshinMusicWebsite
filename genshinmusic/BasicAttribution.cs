using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace genshinmusic
{
    /// <summary>
    /// 与app xaml中的一些属性对应
    /// </summary>
    public class BasicAttribution : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 一个白键的高度（px）
        /// </summary>
        int white_key_height;

        /// <summary>
        /// 一个黑键的高度（px）
        /// </summary>
        int black_key_height;

        /// <summary>
        /// 八度数量
        /// </summary>
        int eight_degree_num = 3;

        /// <summary>
        /// 整个键盘的高度
        /// </summary>
        int piano_key_board_height;

        int semiquaver_width; // 16分音符宽度

        int keyboard_font_size; // 钢琴键盘上字符大小

        public enum THEME { NORMAL, TIAN_YI, SAKURA};


        object white_key_style;
        object black_key_style;
        object dark_bar_style;
        object light_bar_style;
        object music_block_style;
        object chosen_music_block_style;
        object c_key_style;

        public THEME program; 

        /// <summary>
        /// 一个白键的高度（px）
        /// </summary>
        public int White_key_height {
            get { return white_key_height; }
            set
            {
                white_key_height = value;
            }
        }
        /// <summary>
        /// 一个黑键的高度（px）
        /// </summary>
        public int Black_key_height { get => black_key_height; 
            set {
                black_key_height = value;
            } 
        }

        /// <summary>
        /// 八度数量
        /// </summary>
        public int Eight_degree_num { get => eight_degree_num; }

        /// <summary>
        /// 整个键盘的高度
        /// </summary>
        public int Piano_key_board_height { get => piano_key_board_height; }

        /// <summary>
        /// 16分音符宽度(px)
        /// </summary>
        public int Semiquaver_width { get => semiquaver_width;
            set
            {
                semiquaver_width = value;
            }
        }

        public object White_key_style { get => white_key_style;  }
        public object Black_key_style { get => black_key_style; }
        public object Dark_bar_style { get => dark_bar_style; }
        public object Light_bar_style { get => light_bar_style; }
        public object Music_block_style { get => music_block_style; }
        public object Chosen_music_block_style { get => chosen_music_block_style; }
        public int Keyboard_font_size { get => keyboard_font_size;  }
        public object C_key_style { get => c_key_style; }





        /// <summary>
        /// 初始化黑键高度和白键高度（px）
        /// </summary>
        /// <param name="white_height"></param>
        /// <param name="black_height"></param>
        /// <param name="eight_degree_num"></param>
        public BasicAttribution(int white_height, int black_height, int semiquaver_width = 20, int eight_degree_num = 3)
        {
            white_key_height = white_height;
            black_key_height = black_height;
            this.eight_degree_num = eight_degree_num;
            piano_key_board_height = eight_degree_num * white_key_height * 7;
            Console.WriteLine(piano_key_board_height);
            this.semiquaver_width = semiquaver_width;
        }
     
        public void set_size(int white_key_height, int black_key_height, int semiquaver_width, int keyboard_font_size)
        {
            White_key_height = white_key_height;
            Black_key_height = black_key_height;
            Semiquaver_width = semiquaver_width;
            piano_key_board_height = eight_degree_num * white_key_height * 7;
            this.keyboard_font_size = keyboard_font_size;
        }


        public void set_theme(THEME theme)
        {
            
            switch (theme)
            {
                case THEME.NORMAL:
                    white_key_style = Application.Current.Resources["white_key_norm"];
                    black_key_style = Application.Current.Resources["black_key_norm"];
                    dark_bar_style = Application.Current.Resources["dark_bar_norm"];
                    light_bar_style = Application.Current.Resources["light_bar_norm"];
                    music_block_style = Application.Current.Resources["music_block_norm"];
                    chosen_music_block_style = Application.Current.Resources["chosen_music_block_norm"];
                    c_key_style = Application.Current.Resources["White_key_C_grid_norm"];
                    break;
                case THEME.TIAN_YI:
                    white_key_style = Application.Current.Resources["white_key_tianyi"];
                    black_key_style = Application.Current.Resources["black_key_tianyi"];
                    dark_bar_style = Application.Current.Resources["dark_bar_tianyi"];
                    light_bar_style = Application.Current.Resources["light_bar_tianyi"];
                    music_block_style = Application.Current.Resources["music_block_tianyi"];
                    chosen_music_block_style = Application.Current.Resources["chosen_music_block_tianyi"];
                    c_key_style = Application.Current.Resources["White_key_C_grid_tianyi"];
                    break;
                case THEME.SAKURA:
                    white_key_style = Application.Current.Resources["white_key_sakura"];
                    black_key_style = Application.Current.Resources["black_key_sakura"];
                    dark_bar_style = Application.Current.Resources["dark_bar_sakura"];
                    light_bar_style = Application.Current.Resources["light_bar_sakura"];
                    music_block_style = Application.Current.Resources["music_block_sakura"];
                    chosen_music_block_style = Application.Current.Resources["chosen_music_block_sakura"];
                    c_key_style = Application.Current.Resources["White_key_C_grid_sakura"];
                    break;
            }

            if(this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("White_key_style"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Black_key_style"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Dark_bar_style"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Light_bar_style"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Music_block_style"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Chosen_music_block_style"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("C_key_style"));
            }
        }
    }
}
