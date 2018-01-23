 using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4Net
{
    class Program
    {
        static void Main(string[] args)
        {
            //从配置文件中读取log4net的配置，然后进行工作
            log4net.Config.XmlConfigurator.Configure();

            //怎么写日志
            //ILog logwriter = log4net.LogManager.GetLogger("DemoWriter");
            //logwriter.Debug("DEBUG");
            //logwriter.Error("ERROR");
            //logwriter.Warn("WARN");
            string s = "nihaoa";
            string s1=s.Replace("o","a");
            Console.WriteLine(s1);
            Console.WriteLine("ss");
            Console.ReadLine();
        }
    }
}
