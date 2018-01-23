using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Common;
using OA.Model;
using System.Data;
using System.Linq.Expressions;

namespace OA.DAL
{
    public class ClassDal:BaseDal<Class>
    {
        BaseDal<Class> based = new BaseDal<Class>();

        #region 获取所有的班级
        public IList<Class> GetAllClass()
        {
            string sql = @"select * from Class";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Class> ls = ds.Tables[0].ToEntity<Class>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Class>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Class>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Class></returns>
        public new IQueryable<Class> GetEntities(Func<Class, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个班级
        #region 新增一个班级
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增班级的对象</param>
        /// <returns>返回该新增班级</returns>
        public new Class Add(Class entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个班级
        #region 删除一个班级
        /// <summary>
        /// 删除一个班级
        /// </summary>
        /// <param name="entity">输入要删除的那个班级的对象</param>
        /// <returns>返回那个被删除的班级</returns>
        public new Class DeleteEntity(Class entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个班级
        #region 修改一个班级
        /// <summary>
        /// 修改一个班级
        /// </summary>
        /// <param name="entity">输入的班级对象</param>
        /// <returns></returns>
        public new Class UpdateEntity(Class entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询班级
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含班级的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有班级的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Class> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Class, bool> whereLambda,
                                                  Expression<Func<Class, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new ClassDal().GetAllClass().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new ClassDal().GetAllClass().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new ClassDal().GetAllClass().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
