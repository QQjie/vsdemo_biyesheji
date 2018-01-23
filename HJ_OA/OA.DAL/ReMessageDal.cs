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
    public class ReMessageDal:BaseDal<ReMessage>
    {
        BaseDal<ReMessage> based = new BaseDal<ReMessage>();

        #region 获取所有的回复
        public IList<ReMessage> GetAllReMessage()
        {
            string sql = @"select * from ReMessage";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<ReMessage> ls = ds.Tables[0].ToEntity<ReMessage>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<ReMessage>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<ReMessage>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<ReMessage></returns>
        public new IQueryable<ReMessage> GetEntities(Func<ReMessage, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个回复
        #region 新增一个回复
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增回复的对象</param>
        /// <returns>返回该新增回复</returns>
        public new ReMessage Add(ReMessage entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个回复
        #region 删除一个回复
        /// <summary>
        /// 删除一个回复
        /// </summary>
        /// <param name="entity">输入要删除的那个回复的对象</param>
        /// <returns>返回那个被删除的回复</returns>
        public new ReMessage DeleteEntity(ReMessage entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个回复
        #region 修改一个回复
        /// <summary>
        /// 修改一个回复
        /// </summary>
        /// <param name="entity">输入的回复对象</param>
        /// <returns></returns>
        public new ReMessage UpdateEntity(ReMessage entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询回复
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含回复的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param> 
        /// <param name="whereLambda">查询出所有回复的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<ReMessage> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<ReMessage, bool> whereLambda,
                                                  Expression<Func<ReMessage, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new ReMessageDal().GetAllReMessage().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new ReMessageDal().GetAllReMessage().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new ReMessageDal().GetAllReMessage().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
