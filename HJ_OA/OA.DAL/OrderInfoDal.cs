using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using System.Linq.Expressions;

namespace OA.DAL
{
    public class OrderInfoDal:BaseDal<OrderInfo>,IOrderInfoDal
    {


        BaseDal<OrderInfo> based = new BaseDal<OrderInfo>();
        #region 根据输入的lambda参数返回一个IQueryable<OrderInfo>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<OrderInfo>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<OrderInfo></returns>
        public new IQueryable<OrderInfo> GetEntities(Func<OrderInfo, bool> whereLambda)
        {
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
        public new OrderInfo Add(OrderInfo entity)
        {
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
        public new OrderInfo DeleteEntity(OrderInfo entity)
        {
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
        public new OrderInfo UpdateEntity(OrderInfo entity)
        {

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
        public new IQueryable<OrderInfo> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<OrderInfo, bool> whereLambda,
                                                  Expression<Func<OrderInfo, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new OrderInfoDal().GetEntities(u=>u.OrderId>0).Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new OrderInfoDal().GetEntities(u => u.OrderId > 0).Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new OrderInfoDal().GetEntities(u => u.OrderId > 0).Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion


   
    }
}
