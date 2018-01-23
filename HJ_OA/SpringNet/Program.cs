using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNet
{
    class Program
    {
        static void Main(string[] args)
        {

            IApplicationContext ctx = ContextRegistry.GetContext();
          IUserInfoDal u= ctx.GetObject("UserInfoDal") as IUserInfoDal;
           // UserInfoService u = ctx.GetObject("UserInfoService") as UserInfoService;
            u.Show();
            Console.ReadKey();
        }
    }
}
