using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Common;
using System.Linq.Expressions;
using System.Data;

namespace OA.DAL
{
    public class DepartmentDal:BaseDal<Department>
    {
        BaseDal<Department> based = new BaseDal<Department>();

        #region 获取所有的院系
        public IList<Department> GetAllDepartment()
        {
            string sql = @"select * from Department";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Department> ls = ds.Tables[0].ToEntity<Department>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Department>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Department>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Department></returns>
        public new IQueryable<Department> GetEntities(Func<Department, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个院系
        #region 新增一个院系
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增院系的对象</param>
        /// <returns>返回该新增院系</returns>
        public new Department Add(Department entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个院系
        #region 删除一个院系
        /// <summary>
        /// 删除一个院系
        /// </summary>
        /// <param name="entity">输入要删除的那个院系的对象</param>
        /// <returns>返回那个被删除的院系</returns>
        public new Department DeleteEntity(Department entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个院系
        #region 修改一个院系
        /// <summary>
        /// 修改一个院系
        /// </summary>
        /// <param name="entity">输入的院系对象</param>
        /// <returns></returns>
        public new Department UpdateEntity(Department entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询院系
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含院系的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有院系的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Department> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Department, bool> whereLambda,
                                                  Expression<Func<Department, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new DepartmentDal().GetAllDepartment().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new DepartmentDal().GetAllDepartment().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new DepartmentDal().GetAllDepartment().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
