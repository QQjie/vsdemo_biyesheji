using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Common;
using OA.Model;
using System.Linq.Expressions;
using System.Data;

namespace OA.DAL
{
    public class AdminDal:BaseDal<Admin>
    {
        BaseDal<Admin> based = new BaseDal<Admin>();

        #region 获取所有的管理员
        public IList<Admin> GetAllAdmin()
        {
            string sql = @"select * from Admin";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Admin> ls = ds.Tables[0].ToEntity<Admin>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Admin>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Admin>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Admin></returns>
        public new IQueryable<Admin> GetEntities(Func<Admin, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个管理员
        #region 新增一个管理员
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增管理员的对象</param>
        /// <returns>返回该新增管理员</returns>
        public new Admin Add(Admin entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个管理员
        #region 删除一个管理员
        /// <summary>
        /// 删除一个管理员
        /// </summary>
        /// <param name="entity">输入要删除的那个管理员的对象</param>
        /// <returns>返回那个被删除的管理员</returns>
        public new Admin DeleteEntity(Admin entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个管理员
        #region 修改一个管理员
        /// <summary>
        /// 修改一个管理员
        /// </summary>
        /// <param name="entity">输入的管理员对象</param>
        /// <returns></returns>
        public new Admin UpdateEntity(Admin entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询管理员
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含管理员的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有管理员的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Admin> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Admin, bool> whereLambda,
                                                  Expression<Func<Admin, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new AdminDal().GetAllAdmin().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new AdminDal().GetAllAdmin().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new AdminDal().GetAllAdmin().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
