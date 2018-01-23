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
    public class ThemeDal:BaseDal<Theme>
    {


        BaseDal<Theme> based = new BaseDal<Theme>();

        #region 获取所有的论文课题
        public IList<Theme> GetAllTheme()
        {
            string sql = @"select * from Theme";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Theme> ls = ds.Tables[0].ToEntity<Theme>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Theme>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Theme>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Theme></returns>
        public new IQueryable<Theme> GetEntities(Func<Theme, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个论文课题
        #region 新增一个论文课题
        /// <summary>
        /// 新增一个论文课题
        /// </summary>
        /// <param name="entity">输入一个准备新增论文课题的对象</param>
        /// <returns>返回该新增论文课题</returns>
        public new Theme Add(Theme entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个论文课题
        #region 删除一个论文课题
        /// <summary>
        /// 删除一个论文课题
        /// </summary>
        /// <param name="entity">输入要删除的那个论文课题的对象</param>
        /// <returns>返回那个被删除的论文课题</returns>
        public new Theme DeleteEntity(Theme entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个论文课题
        #region 修改一个论文课题
        /// <summary>
        /// 修改一个论文课题
        /// </summary>
        /// <param name="entity">输入的论文课题对象</param>
        /// <returns></returns>
        public new Theme UpdateEntity(Theme entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion



        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询论文课题
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含论文课题的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有论文课题的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Theme> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Theme, bool> whereLambda,
                                                  Expression<Func<Theme, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new ThemeDal().GetAllTheme().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new ThemeDal().GetAllTheme().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new ThemeDal().GetAllTheme().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
