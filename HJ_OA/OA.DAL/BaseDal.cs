using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Common;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using OA.IDAL;

namespace OA.DAL
{   
    
   

    /// <summary>
    /// 职责：封装所有的Dal的公共的crud的方法
    /// </summary>
    ///
     public  class BaseDal<T> where T:class,new () 
     {
      
     
        //以前想随意按照条件过滤，就要动态拼接sql脚本
        #region 以前想随意按照条件过滤，就要动态拼接sql脚本
        public  IQueryable<T> GetEntities(Func<T, bool> whereLambda)
        {
            return MySession.GetSession().Query<T>().Where(whereLambda).AsQueryable();
        }
        #endregion

        //public T GetEntities(Func<T, bool> whereLambda)
        //{
        //    return (T)MySession.GetSession().Query<T>().Where(whereLambda).AsQueryable();
        //}

        //单元测试就是方法测试

        //新增一个T
        #region 新增一个T
          public  T Add(T entity) 
        {
            try
            {
                MySession.GetSession().Save(entity);
            }
            catch (Exception e)
            {
                MySession.TranRollBack();
                throw e;
            }
            return entity;
        }
         #endregion

        //删除一个T
        #region 删除一个T
          public T DeleteEntity(T entity) 
        {
            
            try
            {
                MySession.GetSession().Delete(entity);
            }
            catch (Exception e)
            {
                MySession.TranRollBack();
                throw e;
            }
         
            return entity;
        }
          #endregion

        //修改一个用户
        #region 修改一个用户
          public T UpdateEntity(T entity)
        {
          
            try
            {
                MySession.GetSession().Merge(entity);//.SaveOrUpdate(entity);
              //  MySession.TranCommit();   
            } 
            catch (Exception e)
            {
                MySession.TranRollBack();
                throw e;
            }
           
            return entity;
        }
            #endregion

        //分页查询
        #region 分页查询
         public  IQueryable<T> GetPageEntities<S>(int pageSize,int pageIndex,out int total,
                                                   Func<T,bool> whereLambda,
                                                   Expression<Func<T,S>> orderbyLambda,
                                                   bool isAsc
            )
        {

            total = MySession.GetSession().Query<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = MySession.GetSession().Query<T>().Where(whereLambda).AsQueryable()
                .OrderBy(orderbyLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else {
                var temp = MySession.GetSession().Query<T>().Where(whereLambda).AsQueryable()
                   .OrderByDescending(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1))
                   .Take(pageSize).AsQueryable();
                return temp;
            }
           
        }
        #endregion

    }
}
