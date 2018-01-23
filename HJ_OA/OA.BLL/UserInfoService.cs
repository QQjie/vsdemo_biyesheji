using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model;
using OA.DAL;
using OA.IDAL;
using OA.DALFactory;
using log4net;
namespace OA.BLL
{
    //模块内高内聚  模块间低耦合
    //变化点 1.有不同的数据库
    //       2.有不同的数据库驱动层  有可能是efuserinfodal也有可能是nhuserinfodal
    //       IUserInfoDal dal=new EFUserInfoDal() 或new NHUserInfoDal();


    public class UserInfoService : BaseService<UserInfo>
    {
        ILog logwriter = log4net.LogManager.GetLogger("DemoWriter");
        //依赖接口编程 
        IUserInfoDal userdal = StaticDalFactory.GetUserInfoDal();
        //如果现在有需求 原来是IUserInfoDal dal=new EFUserInfoDal()现在要求把EFUserInfoDal换成NHUserInfoDal
        //因为有可能很多地方都用到了EFUserInfoDal所以这样改起来将会很麻烦
        //自然想到简单工厂设计
        public  UserInfo Add(UserInfo userinfo)
        {    
            
          //如果使用new关键字实际   BaseService<UserInfo> s = new UserInfoService();那么这样调用的就是baseservice的add方法这里默认是使用new
        
          // UserInfo u=userdal.Add(userinfo);
          // BaseDal<UserInfo> based = new BaseDal<UserInfo>();
          // UserInfo u=based.Add(userinfo);
          DBSession db = new DBSession();
          UserInfo u2 = db.UserInfoDal.Add(userinfo);
        
          db.SaveChanges();    //数据提交的权利从数据库访问层提到了业务逻辑层

           //如果没有提到业务逻辑层 那么每次在dal层调用一次crud方法都会执行一次commit方法
           //如歌一个业务有多个crud方法 ，那么每次调用提交 调用提交 这样肯定会降低效率，
           //所以把提交的方法提到业务逻辑层 可以实现多次操作一次 提交从而增加效率.
           return u2;
        }

        public UserInfo Delete(UserInfo userinfo)
        {
            // BaseDal<UserInfo> based = new BaseDal<UserInfo>();
            //UserInfo u = based.DeleteEntity(userinfo);
            DBSession db = new DBSession();
            UserInfo u = db.UserInfoDal.DeleteEntity(userinfo);
            db.SaveChanges();
            return u;
        }

        public UserInfo Get(int id)
        {
            UserInfo user = new UserInfo();
            BaseDal<UserInfo> based = new BaseDal<UserInfo>();
            IQueryable<UserInfo> iq= based.GetEntities(u=>u.UserId==id);
            foreach (var item in iq)
            {
                user = item;
            }
            return user;
        }

        public IQueryable<UserInfo> GetAllUsers(Func<UserInfo, bool> wherelambda)
        {
            BaseDal<UserInfo> based = new BaseDal<UserInfo>();
            IQueryable<UserInfo> iq = based.GetEntities(wherelambda);
            //IList<UserInfo> ls=new List<UserInfo>();
            //foreach (var item in iq)
            //{
            //    ls.Add(item);  
            //}
            return iq;
        }


        public override void SetCurrentDal()
        {
            CurrenDal = StaticDalFactory.GetUserInfoDal();
        }
    }
}
