using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OA.Common
{
    //1 public delegate void WriteLogDel(string str);
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        //1public static WriteLogDel WriteLogDelFunc;

        public static List<ILogWriter> lwlist = new List<ILogWriter>();
       
        
        static LogHelper() 
        {
            lwlist.Add(new WriteLogToTextFile());
            lwlist.Add(new WriteLogToDB());
            //1WriteLogDelFunc = new WriteLogDel(WriteLogToFile);
            //1WriteLogDelFunc += WriteLogToDB;
            //把从队列中获取的错误消息写到日志文件中去
            //ThreadPool.QueueUserWorkItem(o => {
            //    lock (ExceptionStringQueue)
            //    {
            //        string s = ExceptionStringQueue.Dequeue();
            //        //把一场信息写到日志文件中去
            //        //变化点：有可能写到日志文件，也有可能写到数据库里面去，也可以两个地方都写

            //        //执行委托方法，把异常信息写到委托里面去
            //      //1      WriteLogDelFunc(s);

            //        //ILogWriter wl = new WriteLogToTextFile();
            //        //wl.WriteLog(s);

            //        foreach (var item in lwlist)
            //        {
            //            item.WriteLog(s);
            //        }
            //    }

            //});

        }

       //1 public static void WriteLogToFile(string str) { 


        //1 }
      //1  public static void WriteLogToDB(string str)
       //1 {


        //1 }

        public static void writeLog(string exception) {

            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(exception);
            }
        }
    }
}
