using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationForWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new localhost.WebService1();
            var r = ws.Add(2,5);
            Console.WriteLine(r);
            Console.ReadLine();
        }
    }
}
