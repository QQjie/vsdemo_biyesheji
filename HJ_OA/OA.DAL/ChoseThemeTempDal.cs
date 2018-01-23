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
    public class ChoseThemeTempDal : BaseDal<ChoseThemeTemp>
    {
        BaseDal<ChoseThemeTemp> based = new BaseDal<ChoseThemeTemp>();

        #region 获取所有的临时选题
        public IList<ChoseThemeTemp> GetAllChoseThemeTemp()
        {
            string sql = @"select * from ChoseThemeTemp";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<ChoseThemeTemp> ls = ds.Tables[0].ToEntity<ChoseThemeTemp>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<ChoseThemeTemp>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<ChoseThemeTemp>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<ChoseThemeTemp></returns>
        public new IQueryable<ChoseThemeTemp> GetEntities(Func<ChoseThemeTemp, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个临时选题
        #region 新增一个临时选题
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增临时选题的对象</param>
        /// <returns>返回该新增临时选题</returns>
        public new ChoseThemeTemp Add(ChoseThemeTemp entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个临时选题
        #region 删除一个临时选题
        /// <summary>
        /// 删除一个临时选题
        /// </summary>
        /// <param name="entity">输入要删除的那个临时选题的对象</param>
        /// <returns>返回那个被删除的临时选题</returns>
        public new ChoseThemeTemp DeleteEntity(ChoseThemeTemp entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个临时选题
        #region 修改一个临时选题
        /// <summary>
        /// 修改一个临时选题
        /// </summary>
        /// <param name="entity">输入的临时选题对象</param>
        /// <returns></returns>
        public new ChoseThemeTemp UpdateEntity(ChoseThemeTemp entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询临时选题
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含临时选题的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有临时选题的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<ChoseThemeTemp> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<ChoseThemeTemp, bool> whereLambda,
                                                  Expression<Func<ChoseThemeTemp, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new ChoseThemeTempDal().GetAllChoseThemeTemp().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new ChoseThemeTempDal().GetAllChoseThemeTemp().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new ChoseThemeTempDal().GetAllChoseThemeTemp().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
       
    }
}
