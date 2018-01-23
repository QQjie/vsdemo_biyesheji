using MySql.Data.MySqlClient;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OA.Common;

namespace OA.DAL
{
    public class StudentDal:BaseDal<Student>
    {
        BaseDal<Student> based = new BaseDal<Student>();

        #region 根据ID获取用户
        //根据ID获取用户
        public UserInfo GetStudentById(int id)
        {
            string sql = @"select Stu_ID,UserName from UserInfo where UserId=" + id;
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
      
        #endregion

        #region 获取所有的学生
        public IList<Student> GetAllStudent()
        {
            string sql = @"select * from Student";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Student> ls = ds.Tables[0].ToEntity<Student>();
            return ls;
        }
        #endregion

        //以前想随意按照条件过滤，就要动态拼接sql脚本
        #region 以前想随意按照条件过滤，就要动态拼接sql脚本
        public static IQueryable<Student> GetStudents(Func<Student, bool> whereLambda)
        {
            return new StudentDal().GetAllStudent().Where(whereLambda).AsQueryable();
        }
        #endregion

        //单元测试就是方法测试

        //新增一个用户
        //#region 新增一个用户
        //public Student AddUserInfo(Student userinfo)
        //{
        //    string sql = @"insert into UserInfo values(0,?username)";
        //    MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?username", MySqlDbType.VarChar, 20) { Value = userinfo.UserName } };
        //    OA.Common.MySqlHelper.ExecuteSql(sql, param);
        //    return userinfo;
        //}
        //#endregion

        //根据id删除一个用户
        //#region 根据id删除一个用户
        //public UserInfo DeleteUserInfoById(UserInfo userinfo)
        //{
        //    string sql = @"delete from UserInfo where UserId=?id";
        //    MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?id", MySqlDbType.Int32) { Value = userinfo.UserId } };
        //    OA.Common.MySqlHelper.ExecuteSql(sql, param);
        //    return userinfo;
        //}
        //#endregion

        //根据用户姓名删除一个用户
        //#region 根据用户姓名删除一个用户
        //public UserInfo DeleteUserInfo(UserInfo userinfo)
        //{
        //    string sql = @"delete from UserInfo where UserId=?name";
        //    MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?name", MySqlDbType.VarChar) { Value = userinfo.UserName } };
        //    OA.Common.MySqlHelper.ExecuteSql(sql, param);
        //    return userinfo;
        //}
        //#endregion

        //修改一个用户
        //#region 修改一个用户
        ///// <summary>
        ///// 修改一个用户
        ///// </summary>
        ///// <param name="userinfo">输入一个用户对象</param>
        ///// <returns>返回输入的用户对象</returns>
        //public UserInfo UpdateUserInfo(UserInfo userinfo)
        //{
        //    string sql = @"update UserInfo set UserName=?username where UserId=" + userinfo.UserId;
        //    MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?name", MySqlDbType.VarChar) { Value = userinfo.UserName } };
        //    OA.Common.MySqlHelper.ExecuteSql(sql, param);
        //    return userinfo;
        //}
        //#endregion

        //分页查询
        //#region 分页查询
        //public IQueryable<UserInfo> GetPageUsers<S>(int pageSize, int pageIndex, out int total,
        //                                          Func<UserInfo, bool> whereLambda,
        //                                          Expression<Func<UserInfo, S>> orderbyLambda,
        //                                          bool isAsc

        //   )
        //{
        //    total = new UserInfoDal().GetAllUserInfo().Where(whereLambda).Count();
        //    if (isAsc)
        //    {
        //        var temp = new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable()
        //        .OrderBy(orderbyLambda)
        //        .Skip(pageSize * (pageIndex - 1))
        //        .Take(pageSize).AsQueryable();
        //        return temp;
        //    }
        //    else
        //    {
        //        var temp = new UserInfoDal().GetAllUserInfo().Where(whereLambda).AsQueryable()
        //           .OrderByDescending(orderbyLambda)
        //           .Skip(pageSize * (pageIndex - 1))
        //           .Take(pageSize).AsQueryable();
        //        return temp;
        //    }

        //}
        //#endregion

        #region 根据输入的lambda参数返回一个IQueryable<Student>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Student>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Student></returns>
        public new IQueryable<Student> GetEntities(Func<Student, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个学生
        #region 新增一个学生
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增学生的对象</param>
        /// <returns>返回该新增学生</returns>
        public new Student Add(Student entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个学生
        #region 删除一个学生
        /// <summary>
        /// 删除一个学生
        /// </summary>
        /// <param name="entity">输入要删除的那个学生的对象</param>
        /// <returns>返回那个被删除的学生</returns>
        public new Student DeleteEntity(Student entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个学生
        #region 修改一个学生
        /// <summary>
        /// 修改一个学生
        /// </summary>
        /// <param name="entity">输入的学生对象</param>
        /// <returns></returns>
        public new Student UpdateEntity(Student entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion


        //修改一个学生
        #region 修改一个学生的photo路径
        /// <summary>
        /// 修改一个学生photo路径
        /// </summary>
        /// <param name="student">输入的要修改学生的对象</param>
        /// <param name="photostr">输入的学生photo路径</param>
        /// <returns></returns>
        public void UpdateStudentPhoto(Student student, string photostr)
        {
            student.Stu_Photo = photostr;
            based.UpdateEntity(student);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询学生
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含学生的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有学生的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Student> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Student, bool> whereLambda,
                                                  Expression<Func<Student, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new StudentDal().GetAllStudent().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new StudentDal().GetAllStudent().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new StudentDal().GetAllStudent().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
