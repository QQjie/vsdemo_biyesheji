using MySql.Data.MySqlClient;
using OA.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Common;
using OA.Model;
using System.Linq.Expressions;
using OA.IDAL;

namespace OA.DAL
{
    public class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {

        BaseDal<UserInfo> based = new BaseDal<UserInfo>();
        #region 根据ID获取用户
          //根据ID获取用户
        public   UserInfo GetUserInfoById(int id) {
            string sql = @"select UserId,UserName from UserInfo where UserId="+id;
            //MySqlParameter[] param = new MySqlParameter[]
            //    {
            //        new MySqlParameter("?id", MySqlDbType.Int32){ Value = id }
                   
            //    };
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    //实体转换
            //    return ds.Tables[0].ToEntity<Examrange>();
            //}
            ////返回考试范围数据列表
            //return new List<Examrange>();
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].ToEntity<UserInfo>()[0];
            }
            return new UserInfo();
        }
        //获取所有的用户
        //public static IList<UserInfo> GetAllUserInfo() 
        //{
        //    string sql = @"select * from UserInfo";
        //    DataSet ds=OA.Common.MySqlHelper.Query(sql);
        //    IList<UserInfo> ls= ds.Tables[0].ToEntity<UserInfo>();
        //    return ls;
        //}
        #endregion

        #region 获取所有的用户
        public  IList<UserInfo> GetAllUserInfo()
        {
            string sql = @"select * from UserInfo";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<UserInfo> ls = ds.Tables[0].ToEntity<UserInfo>();
            return ls;
        }
        #endregion

        //以前想随意按照条件过滤，就要动态拼接sql脚本
        #region 以前想随意按照条件过滤，就要动态拼接sql脚本
         public static IQueryable<UserInfo> GetUsers(Func<UserInfo,bool> whereLambda) 
        {
            return new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable();
        }
        #endregion

        //单元测试就是方法测试

        //新增一个用户
        #region 新增一个用户
          public  UserInfo AddUserInfo(UserInfo userinfo) 
        {
            string sql = @"insert into UserInfo values(0,?username)";
            MySqlParameter[] param = new MySqlParameter[] {new MySqlParameter("?username",MySqlDbType.VarChar,20){Value=userinfo.UserName} };
            OA.Common.MySqlHelper.ExecuteSql(sql,param);
            return userinfo;
        }
         #endregion

        //根据id删除一个用户
        #region 根据id删除一个用户
           public  UserInfo DeleteUserInfoById(UserInfo userinfo) 
        {
            string sql = @"delete from UserInfo where UserId=?id";
            MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?id", MySqlDbType.Int32) { Value = userinfo.UserId } };
            OA.Common.MySqlHelper.ExecuteSql(sql, param);
            return userinfo;
        }
          #endregion

        //根据用户姓名删除一个用户
        #region 根据用户姓名删除一个用户
            public  UserInfo DeleteUserInfo(UserInfo userinfo)
        {
            string sql = @"delete from UserInfo where UserId=?name";
            MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?name", MySqlDbType.VarChar) { Value = userinfo.UserName } };
            OA.Common.MySqlHelper.ExecuteSql(sql, param);
            return userinfo;
        }
           #endregion

        //修改一个用户
        #region 修改一个用户
        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="userinfo">输入一个用户对象</param>
        /// <returns>返回输入的用户对象</returns>
             public  UserInfo UpdateUserInfo(UserInfo userinfo)
        {
            string sql = @"update UserInfo set UserName=?username where UserId="+userinfo.UserId;
            MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?name", MySqlDbType.VarChar) { Value = userinfo.UserName } };
            OA.Common.MySqlHelper.ExecuteSql(sql, param);
            return userinfo;
        }
            #endregion

        //分页查询
        #region 分页查询
         public IQueryable<UserInfo> GetPageUsers<S>(int pageSize,int pageIndex,out int total,
                                                   Func<UserInfo,bool> whereLambda,
                                                   Expression<Func<UserInfo,S>> orderbyLambda,
                                                   bool isAsc
            
            )
        {
            total = new UserInfoDal().GetAllUserInfo().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else {
                var temp = new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }
           
        }
        #endregion

      
        
        
        
        
        //-------------------------------下面是NHibernate框架的crud-------------------------------------------------------------------------------------------------------------------
        
        
        
        //使用new是为了去除警告；
        //虽然MSDN中说派生类中虚函数重写时，可以选择使用override关键字而不是new，将基类实现替换为它自己的实现，
        //但是经过我测试，使用这两个关键字没有什么区别，使用new并不会产生编译问题，且运行结果也和override效果相同。
        //但是如果在基类中没有声明virtual但在继承类中使用override重写，会导致编译错误：继承成员未被标记为virtual,
         //abstract或override。看来，可以总结为：new可以替代override，反之不然。



         #region 根据输入的lambda参数返回一个IQueryable<UserInfo>的集合
         /// <summary>
         /// 根据输入的lambda参数返回一个IQueryable<UserInfo>的集合
         /// </summary>
         /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
         /// <returns>IQueryable<UserInfo></returns>
         public new IQueryable<UserInfo> GetEntities(Func<UserInfo, bool> whereLambda) {       
             return based.GetEntities(whereLambda);
         }
         #endregion

         //新增一个用户
         #region 新增一个用户
         /// <summary>
         /// 新增一个用户
         /// </summary>
         /// <param name="entity">输入一个准备新增用户的对象</param>
         /// <returns>返回该新增用户</returns>
         public new UserInfo Add(UserInfo entity) {
             return based.Add(entity);
         }

         #endregion

         //删除一个用户
         #region 删除一个用户
        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="entity">输入要删除的那个用户的对象</param>
        /// <returns>返回那个被删除的用户</returns>
         public new UserInfo DeleteEntity(UserInfo entity) {
              return based.DeleteEntity(entity);
         }

         #endregion

         //修改一个用户
         #region 修改一个用户
         /// <summary>
         /// 修改一个用户
         /// </summary>
         /// <param name="entity">输入的用户对象</param>
         /// <returns></returns>
         public new UserInfo UpdateEntity(UserInfo entity) {
             
             return based.UpdateEntity(entity);
         }

         #endregion

         //分页查询
         #region 分页查询
        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含用户的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有用户的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
         public new IQueryable<UserInfo> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                   Func<UserInfo, bool> whereLambda,
                                                   Expression<Func<UserInfo, S>> orderbyLambda,
                                                   bool isAsc

            ) {

                total = new UserInfoDal().GetAllUserInfo().Where(whereLambda).Count();
                if (isAsc)
                {
                    var temp = new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable()
                    .OrderBy(orderbyLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                    return temp;
                }
                else
                {
                    var temp = new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable()
                       .OrderByDescending(orderbyLambda)
                       .Skip(pageSize * (pageIndex - 1))
                       .Take(pageSize).AsQueryable();
                    return temp;
                }

            }
         #endregion


    }
}
