using NHibernate;
using NHibernate.Cfg;
using OA.DAL;
using OA.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using OA.Model;
using OA.BLL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
           //int  i= (int)MyEnum.post;
           //Console.WriteLine(i);
           //Console.ReadLine();

            //UserInfo u=UserInfoDal.GetUserInfoById(1);
           
            //Console.WriteLine(u.UserName);

            //遍历查询所有的用户
            //IList<UserInfo> ls = UserInfoDal.GetAllUserInfo();

       //     IEnumerable<UserInfo> ls = (IEnumerable<UserInfo>)new UserInfoDal().GetAllUserInfo().Where(u => u.UserId < 5);
            //for (int i = 0; i < ls.Count; i++)
            //{
            //     Console.WriteLine(ls[i].UserName);
            //}
      //      IQueryable<UserInfo> ql=UserInfoDal.GetUsers(u=>u.UserId<40);
      //      foreach (var item in ql)
      //      {
      //          Console.WriteLine(item.UserName);
      //      }

           // ISessionFactory sessionFactory =
            //  new Configuration().Configure().BuildSessionFactory();

            ////通过工厂创建一个Session对象
           // var session = sessionFactory.OpenSession();//相当于EF的上下文
           // IQueryable<Student> od1=session.Query<Student>();   //using NHibernate.Linq;该方法位于NHibernate.Linq中一定要先using
          //IQueryable<OrderInfo> od = session.Query<OrderInfo>();
         //IQueryable<OrderInfo> od = BaseDal<OrderInfo>.GetEntities(u => u.OrderId < 40);
          // BaseDal<UserInfo>.Add(new UserInfo(){UserId=8,UserName="sssss"} );

     //       IQueryable<My> od = new BaseDal<My>().GetEntities(u => u.id< 40);
     //    Console.WriteLine("---------------");
     //       IQueryable<Student> od2 =new  BaseDal<Student>().GetEntities(u => u.Id< 40);
     //       foreach (var item in od)
     //       {
    //            Console.WriteLine(item.name);
     //       }
     //       Console.WriteLine("---------------");
     //       foreach (var item in od2)
    //        {
     //           Console.WriteLine(item.Name);
     //       }
     //      Console.WriteLine("---------------");
            //foreach (var item in od1)
            //{
            //    Console.WriteLine(item.Name);
            //}
            BaseDal<UserInfo> bs = new BaseDal<UserInfo>();
            IQueryable<UserInfo> od = bs.GetEntities(u => u.UserId < 40);
            foreach (var item in od)
            {
                Console.WriteLine( item.UserName);
            }
            UserInfoService us = new UserInfoService();
            us.Delete(new UserInfo() { UserId=40,UserName="hhhhh"});
            Console.WriteLine("---------");

           Console.ReadLine();
        }
    }
}
