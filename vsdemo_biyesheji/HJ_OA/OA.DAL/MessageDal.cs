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
    public class MessageDal:BaseDal<Message>
    {
        BaseDal<Message> based = new BaseDal<Message>();

        #region 获取所有的留言
        public IList<Message> GetAllMessage()
        {
            string sql = @"select * from Message";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Message> ls = ds.Tables[0].ToEntity<Message>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Message>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Message>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Message></returns>
        public new IQueryable<Message> GetEntities(Func<Message, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个留言
        #region 新增一个留言
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增留言的对象</param>
        /// <returns>返回该新增留言</returns>
        public new Message Add(Message entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个留言
        #region 删除一个留言
        /// <summary>
        /// 删除一个留言
        /// </summary>
        /// <param name="entity">输入要删除的那个留言的对象</param>
        /// <returns>返回那个被删除的留言</returns>
        public new Message DeleteEntity(Message entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个留言
        #region 修改一个留言
        /// <summary>
        /// 修改一个留言
        /// </summary>
        /// <param name="entity">输入的留言对象</param>
        /// <returns></returns>
        public new Message UpdateEntity(Message entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询留言
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含留言的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有留言的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Message> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Message, bool> whereLambda,
                                                  Expression<Func<Message, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new MessageDal().GetAllMessage().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new MessageDal().GetAllMessage().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new MessageDal().GetAllMessage().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
