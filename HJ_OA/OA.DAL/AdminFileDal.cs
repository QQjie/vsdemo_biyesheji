﻿using OA.Model;
using OA.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
    public class AdminFileDal:BaseDal<AdminFile>
    {
        BaseDal<AdminFile> based = new BaseDal<AdminFile>();

        #region 获取所有的选题
        public IList<AdminFile> GetAllAdminFile()
        {
            string sql = @"select * from adminfile";
            DataSet ds = OA.Common.MySqlHelper.Query(sql);
            IList<AdminFile> ls = ds.Tables[0].ToEntity<AdminFile>();
            return ls;
        }
        #endregion

        #region 根据输入的lambda参数返回一个IQueryable<AdminFile>的集合
        /// <summary>
        /// 根据输入的lambda参数返回一个IQueryable<AdminFile>的集合
        /// </summary>
        /// <param name="whereLambda">输入一个lambda表达式筛选要查询的集合</param>
        /// <returns>IQueryable<AdminFile></returns>
        public new IQueryable<AdminFile> GetEntities(Func<AdminFile, bool> whereLambda)
        {
            return based.GetEntities(whereLambda);
        }
        #endregion

        //新增一个选题
        #region 新增一个选题
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="entity">输入一个准备新增选题的对象</param>
        /// <returns>返回该新增选题</returns>
        public new AdminFile Add(AdminFile entity)
        {
            return based.Add(entity);
        }

        #endregion

        //删除一个选题
        #region 删除一个选题
        /// <summary>
        /// 删除一个选题
        /// </summary>
        /// <param name="entity">输入要删除的那个选题的对象</param>
        /// <returns>返回那个被删除的选题</returns>
        public new AdminFile DeleteEntity(AdminFile entity)
        {
            return based.DeleteEntity(entity);
        }

        #endregion

        //修改一个选题
        #region 修改一个选题
        /// <summary>
        /// 修改一个选题
        /// </summary>
        /// <param name="entity">输入的选题对象</param>
        /// <returns></returns>
        public new AdminFile UpdateEntity(AdminFile entity)
        {

            return based.UpdateEntity(entity);
        }

        #endregion

        //分页查询
        #region 分页查询
        /// <summary>
        /// 分页查询选题
        /// </summary>
        /// <typeparam name="S">orderby的参数类型</typeparam>
        /// <param name="pageSize">页面大小，一个页面包含选题的个数</param>
        /// <param name="pageIndex">当前的页面</param>
        /// <param name="total">输出的用户总数</param>
        /// <param name="whereLambda">查询出所有选题的条件</param>
        /// <param name="orderbyLambda">orderby的lambda表达式</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <returns></returns>
        public new IQueryable<AdminFile> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<AdminFile, bool> whereLambda,
                                                  Expression<Func<AdminFile, S>> orderbyLambda,
                                                  bool isAsc

           )
        {

            total = new AdminFileDal().GetAllAdminFile().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = new AdminFileDal().GetAllAdminFile().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = new AdminFileDal().GetAllAdminFile().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion
    }
}
