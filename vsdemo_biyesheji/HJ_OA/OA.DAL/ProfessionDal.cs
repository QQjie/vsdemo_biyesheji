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
    public class ProfessionDal:BaseDal<Profession>
    {
        BaseDal<Profession> based = new BaseDal<Profession>();

        #region 获取所有的专业
        public IList<Profession> GetAllProfession()
        {
            string sql = @"select * from Profession";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<Profession> ls = ds.Tables[0].ToEntity<Profession>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<Profession>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<Profession>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<Profession></returns>
        public new IQueryable<Profession> GetEntities(Func<Profession, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个专业
        #region 新增一个专业
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增专业的对象</param>
        /// <returns>返回该新增专业</returns>
        public new Profession Add(Profession entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个专业
        #region 删除一个专业
        /// <summary>
        /// 删除一个专业
        /// </summary>
        /// <param name="entity">输入要删除的那个专业的对象</param>
        /// <returns>返回那个被删除的专业</returns>
        public new Profession DeleteEntity(Profession entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个专业
        #region 修改一个专业
        /// <summary>
        /// 修改一个专业
        /// </summary>
        /// <param name="entity">输入的专业对象</param>
        /// <returns></returns>
        public new Profession UpdateEntity(Profession entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询专业
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含专业的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有专业的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<Profession> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<Profession, bool> whereLambda,
                                                  Expression<Func<Profession, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new ProfessionDal().GetAllProfession().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new ProfessionDal().GetAllProfession().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new ProfessionDal().GetAllProfession().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
