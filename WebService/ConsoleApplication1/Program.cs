using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new ServiceReference1.WebService1SoapClient();
            var result=ws.Add(new ServiceReference1.AddRequest(3,3));
            var s=ws.HelloWorld(new ServiceReference1.HelloWorldRequest());
            Console.WriteLine(result.AddResult);
            Console.WriteLine(s.Body.HelloWorldResult);
            Console.ReadLine();
        } 
    }
}
