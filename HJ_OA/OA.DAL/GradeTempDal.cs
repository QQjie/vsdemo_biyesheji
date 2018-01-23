using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OA.Common;
using OA.Model;

namespace OA.DAL
{
    public class GradeTempDal:BaseDal<GradeTemp>
    {
        BaseDal<GradeTemp> based = new BaseDal<GradeTemp>();
        #region 获取所有的临时成绩
        public IList<GradeTemp> GetAllGradeTemp()
        {
            string sql = @"select * from GradeTemp";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<GradeTemp> ls = ds.Tables[0].ToEntity<GradeTemp>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<GradeTemp>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<GradeTemp>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<GradeTemp></returns>
        public new IQueryable<GradeTemp> GetEntities(Func<GradeTemp, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个临时成绩
        #region 新增一个临时成绩
        /// <summary>
        /// 新增一个临时成绩
        /// </summary>
        /// <param name="entity">输入一个准备新增临时成绩的对象</param>
        /// <returns>返回该新增成绩</returns>
        public new GradeTemp Add(GradeTemp entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个临时成绩
        #region 删除一个临时成绩
        /// <summary>
        /// 删除一个临时成绩
        /// </summary>
        /// <param name="entity">输入要删除的那个临时成绩的对象</param>
        /// <returns>返回那个被删除的临时成绩</returns>
        public new GradeTemp DeleteEntity(GradeTemp entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个临时成绩
        #region 修改一个临时成绩
        /// <summary>
        /// 修改一个临时成绩
        /// </summary>
        /// <param name="entity">输入的临时成绩对象</param>
        /// <returns></returns>
        public new GradeTemp UpdateEntity(GradeTemp entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询临时成绩
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含成绩的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有成绩的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<GradeTemp> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<GradeTemp, bool> whereLambda,
                                                  Expression<Func<GradeTemp, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new GradeTempDal().GetAllGradeTemp().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new GradeTempDal().GetAllGradeTemp().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new GradeTempDal().GetAllGradeTemp().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
