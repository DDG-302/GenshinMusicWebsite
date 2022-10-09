using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
//using System.Windows.Forms;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography;
using System.Windows;

namespace genshinmusic
{

    class SaveLoadProj
    {
        string file_path = "";
        string proj_name = "";

        public string File_path { get => file_path;  }
        public string Proj_name { get => proj_name ;  }

        /// <summary>
        /// 保存一个新的文件
        /// </summary>
        /// <param name="music_sheet"></param>
        /// <param name="bpm"></param>
        /// <returns></returns>
        public bool save_to_file(MusicSheet music_sheet, int bpm)
        {
            if(music_sheet == null)
            {
                return false;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            
            string filename = "";
            dialog.Filter = "工程保存(*.genmujson)|*.genmujson";
            dialog.Title = "保存";
            if (dialog.ShowDialog() == true)
            {
                Console.WriteLine(dialog.FileName);
                filename = dialog.FileName;
            }
            else
            {
                return false;
            }

            StreamWriter sw = null;
            try
            {
                var save_file = new SaveFileTemplate(
                      beats_per_bar: music_sheet.Beat_per_bar,
                      bpm,
                      music_sheet: music_sheet.Music_sheet); // 根据文件的保存模板保存文件
                var options = new JsonSerializerOptions { WriteIndented = true };
                var data = JsonSerializer.Serialize(save_file, options); // 导出json格式的序列化保存信息q
                sw = new StreamWriter(filename, append: false, encoding: Encoding.UTF8); // 写文件
                sw.WriteLine(data);
                sw.Flush();
                sw.Close();
                MessageBox.Show("保存完成",
                            "提示",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                Console.WriteLine(data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("保存失败！",
                            "错误",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                LogMsg logmsg = new LogMsg(e);
                ProgramLog.write_log(logmsg);
            }
            finally
            {
                if(sw != null)
                    sw.Close();
            }
            this.file_path = filename;
            this.proj_name = Path.GetFileNameWithoutExtension(file_path);
            return true;

        }

        /// <summary>
        /// 保存到已有文件
        /// </summary>
        /// <param name="music_sheet"></param>
        /// <param name="bpm"></param>
        /// <returns></returns>
        public bool save_to_existed_file(MusicSheet music_sheet, int bpm, string file_path)
        {
            if (File.Exists(file_path))
            {
                StreamWriter sw = null;
                try
                {
                    string json_str = File.ReadAllText(file_path);
                    var save_file = new SaveFileTemplate(
                          beats_per_bar: music_sheet.Beat_per_bar,
                          bpm,
                          music_sheet: music_sheet.Music_sheet); // 根据文件的保存模板保存文件
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var data = JsonSerializer.Serialize(save_file, options); // 导出json格式的序列化保存信息q
                    sw = new StreamWriter(file_path, append: false, encoding: Encoding.UTF8); // 写文件
                    sw.WriteLine(data);
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("保存完成",
                                "提示",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("保存失败！",
                                "错误",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    LogMsg logmsg = new LogMsg(e);
                    ProgramLog.write_log(logmsg);
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                }

            }
            else
            {
                return save_to_file(music_sheet, bpm);
            }

            return true;
        }

        public SaveFileTemplate load_file()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "工程加载(*.genmujson)|*.genmujson";
            string filename = "";
            string json_str = "";
            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;
            }
            else
            {
                return null;
            }
            if (File.Exists(filename))
            {
                json_str = File.ReadAllText(filename);
            }
            else
            {
                MessageBox.Show("文件不存在",
                        "警告",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                return null;
            }
            SaveFileTemplate save_file = null;
            try
            {
                save_file = JsonSerializer.Deserialize<SaveFileTemplate>(json_str);
            }
            catch(Exception e)
            {
                LogMsg logMsg = new LogMsg(e);
                ProgramLog.write_log(logMsg);
                MessageBox.Show("工程文件内容错误，无法加载！",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                return null;
            }
            file_path = filename;
            this.proj_name = Path.GetFileNameWithoutExtension(filename);
            
            return save_file;
        }
    }
}
