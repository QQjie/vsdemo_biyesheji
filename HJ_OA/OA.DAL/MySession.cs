using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
    /// <summary>
    /// 获取与关闭session的类
    /// </summary>

    public class MySession
    {

        public static ISessionFactory sessionFactory;

        public static ISession session;

        public static ITransaction tran;

        private static void BindContext()
        {
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                CurrentSessionContext.Bind(sessionFactory.OpenSession());
            }
        }

        private static void UnBindContext()
        {
            if (CurrentSessionContext.HasBind(sessionFactory))
            {
                CurrentSessionContext.Unbind(sessionFactory);
            }
        }

        public static ISession GetCurrentSession()
        {
            BindContext();
         
            ISession session = CallContext.GetData("ISession") as ISession;   //一次请求公用一个session实例
            if (session==null)
            {
                session = sessionFactory.GetCurrentSession();
                CallContext.SetData("ISession", session);
            }
            return session;
        }
        //获取所有的T类的IQueryable  相当于ef上下文的db.Set(T);
        public static ISession GetSession()
        {

            //通过配置文件创建Nh  Session的工厂
            sessionFactory =
              new Configuration().Configure().BuildSessionFactory();

            //HttpContext context = HttpContext.Current;
            //if (context.Items["nhsession"] == null)
            //{
            //    context.Items["nhsession"] = sessionFactory.OpenSession();
            //}
            //return context.Items["nhsession"] as ISession;




            //通过工厂创建一个Session对象
            //  var session = sessionFactory.OpenSession();//相当于EF的上下文
            //session = sessionFactory.OpenSession();
            //       session = sessionFactory.GetCurrentSession();
            //       tran = session.BeginTransaction();           //这一句会conn.setAutoCommit(false);  重点此处将连接设置为不自动提交
            //一定要导入NHibernate.Linq;
            


            ISession session = MySession.GetCurrentSession();
            tran = session.BeginTransaction();
            return session;
        }

        public static ITransaction GetTran()
        {
          //  tran = session.BeginTransaction();        //注意这里的tran的前面不能加上ITransaction 否则 tran会是null
            return tran;
        }

        public static void TranRollBack()
        {
            tran.Rollback();
        }


        //session.save()的时候并没有执行sql语句 而是把它缓存了起来
        //commit之后才提交sql语句
        //因此这样可以在commit之前执行多条sql语句 从而减少数据库的访问
        public static void TranCommit()
        {
            tran.Commit();
        }
        //关闭session
        public static void CloseSession()
        {
            if (session != null)
            {
                session.Close();
            }
        }
    }
}
