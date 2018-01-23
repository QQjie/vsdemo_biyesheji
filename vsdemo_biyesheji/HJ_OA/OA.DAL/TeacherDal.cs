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
    public class TeacherDal:BaseDal<Teacher>
    {
        BaseDal<Teacher> based = new BaseDal<Teacher>();
        
        #region 获取所有的老师
        public IList<Teacher> GetAllTeacher()
        {
            string sql = @"select * from Teacher";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Teacher> ls = ds.Tables[0].ToEntity<Teacher>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Teacher>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Teacher>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Teacher></returns>
        public new IQueryable<Teacher> GetEntities(Func<Teacher, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个老师
        #region 新增一个老师
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增老师的对象</param>
        /// <returns>返回该新增老师</returns>
        public new Teacher Add(Teacher entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个老师
        #region 删除一个老师
        /// <summary>
        /// 删除一个老师
        /// </summary>
        /// <param name="entity">输入要删除的那个老师的对象</param>
        /// <returns>返回那个被删除的老师</returns>
        public new Teacher DeleteEntity(Teacher entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个老师
        #region 修改一个老师
        /// <summary>
        /// 修改一个老师
        /// </summary>
        /// <param name="entity">输入的老师对象</param>
        /// <returns></returns>
        public new Teacher UpdateEntity(Teacher entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion


        //修改一个老师
        #region 修改一个老师的photo路径
        /// <summary>
        /// 修改一个老师photo路径
        /// </summary>
        /// <param name="Teacher">输入的要修改老师的对象</param>
        /// <param name="photostr">输入的老师photo路径</param>
        /// <returns></returns>
        public void UpdateTeacherPhoto(Teacher Teacher, string photostr)
        {
            Teacher.Tea_Photo = photostr;
            based.UpdateEntity(Teacher);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询老师
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含老师的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有老师的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Teacher> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Teacher, bool> whereLambda,
                                                  Expression<Func<Teacher, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new TeacherDal().GetAllTeacher().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new TeacherDal().GetAllTeacher().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new TeacherDal().GetAllTeacher().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
