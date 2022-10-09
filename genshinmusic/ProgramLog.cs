using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace genshinmusic
{
    class LogMsg
    {
        DateTime datetime;
        string msg;
        string source;
        string stack_trace;

        public DateTime Datetime { get => datetime; set => datetime = value; }
        public string Msg { get => msg; set => msg = value; }
        public string Source { get => source; set => source = value; }
      
        public string Stack_trace { get => stack_trace; set => stack_trace = value; }

        /// <summary>
        /// 打log时的格式化信息
        /// </summary>
        /// <param name="e">报错信息（exception）</param>
        public LogMsg(Exception e)
        {
            this.datetime = DateTime.Now;
            this.msg = e.Message;
            this.source = e.Source;
            this.stack_trace = e.StackTrace;
        }
    }
    static class ProgramLog
    {
        static string log_path = "log.txt";
        static public void write_log(LogMsg logmsg)
        {
            StreamWriter sw = new StreamWriter(log_path, true);
            sw.WriteLine("错误时间：" + logmsg.Datetime.ToString());
            sw.WriteLine("错误信息：" + logmsg.Msg);
            sw.WriteLine("错误源：" + logmsg.Source);
            sw.WriteLine("错误栈：" + logmsg.Stack_trace);
            sw.WriteLine("===========================================");
            sw.WriteLine("===========================================");
            sw.Flush();
            sw.Close();
        }
    }
}
