using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model;
using OA.Common;
using System.Linq.Expressions;
using System.Data;

namespace OA.DAL
{
    public class TeaGroupDal:BaseDal<TeaGroup>
    {
        BaseDal<TeaGroup> based = new BaseDal<TeaGroup>();

        #region 获取所有的评分小组
        public IList<TeaGroup> GetAllTeaGroup()
        {
            string sql = @"select * from TeaGroup";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<TeaGroup> ls = ds.Tables[0].ToEntity<TeaGroup>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<TeaGroup>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<TeaGroup>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<TeaGroup></returns>
        public new IQueryable<TeaGroup> GetEntities(Func<TeaGroup, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个评分小组
        #region 新增一个评分小组
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增评分小组的对象</param>
        /// <returns>返回该新增评分小组</returns>
        public new TeaGroup Add(TeaGroup entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个评分小组
        #region 删除一个评分小组
        /// <summary>
        /// 删除一个评分小组
        /// </summary>
        /// <param name="entity">输入要删除的那个评分小组的对象</param>
        /// <returns>返回那个被删除的评分小组</returns>
        public new TeaGroup DeleteEntity(TeaGroup entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个评分小组
        #region 修改一个评分小组
        /// <summary>
        /// 修改一个评分小组
        /// </summary>
        /// <param name="entity">输入的评分小组对象</param>
        /// <returns></returns>
        public new TeaGroup UpdateEntity(TeaGroup entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询评分小组
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含评分小组的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有评分小组的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<TeaGroup> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<TeaGroup, bool> whereLambda,
                                                  Expression<Func<TeaGroup, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new TeaGroupDal().GetAllTeaGroup().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new TeaGroupDal().GetAllTeaGroup().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new TeaGroupDal().GetAllTeaGroup().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
