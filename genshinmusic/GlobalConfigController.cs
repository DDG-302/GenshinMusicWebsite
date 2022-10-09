using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace genshinmusic
{
    class GlobalConfigController
    {
        BasicAttribution.THEME theme;
        MainWindow.WINDOW_SIZE window_size;

        public BasicAttribution.THEME Theme { get => theme; set => theme = value; }
        public MainWindow.WINDOW_SIZE Window_size { get => window_size; set => window_size = value; }
      

        string path = "genmuconfig.json";



        public GlobalConfigController(BasicAttribution.THEME theme, MainWindow.WINDOW_SIZE window_size)
        {
            this.theme = theme;
            this.window_size = window_size;
        }

        public GlobalConfigController()
        {

        }

        public void save_config()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var data = JsonSerializer.Serialize(this, options); // 导出json格式的序列化保存信息q
            var sw = new StreamWriter(path, false); // 写文件
            sw.WriteLine(data);
            sw.Flush();
            sw.Close();
        }

        public void load_config()
        {
            if (File.Exists(path))
            {
                string json_str = File.ReadAllText(path);
                try
                {
                    var config_obj = JsonSerializer.Deserialize<GlobalConfigController>(json_str);
                    this.theme = config_obj.theme;
                    this.window_size = config_obj.window_size;
                }
                catch (Exception e)
                {
                    LogMsg logMsg = new LogMsg(e);
                    ProgramLog.write_log(logMsg);
                    var config = new GlobalConfigController(BasicAttribution.THEME.NORMAL, MainWindow.WINDOW_SIZE.NORMAL);
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var data = JsonSerializer.Serialize(config, options); // 导出json格式的序列化保存信息q
                    var sw = new StreamWriter(path, false); // 写文件
                    sw.WriteLine(data);
                    sw.Flush();
                    sw.Close();
                }
               
            }
            else
            {
                var config = new GlobalConfigController(BasicAttribution.THEME.NORMAL, MainWindow.WINDOW_SIZE.NORMAL);
                var options = new JsonSerializerOptions { WriteIndented = true };
                var data = JsonSerializer.Serialize(config, options); // 导出json格式的序列化保存信息q
                var sw = new StreamWriter(path, false); // 写文件
                this.theme = BasicAttribution.THEME.NORMAL;
                this.window_size = MainWindow.WINDOW_SIZE.NORMAL;
                sw.WriteLine(data);
                sw.Flush();
                sw.Close();
            }
           
        }

    }
}
