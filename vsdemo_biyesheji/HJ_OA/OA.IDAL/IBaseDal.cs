using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
    public interface IBaseDal<T> where T:class ,new()
    {
         //以前想随意按照条件过滤，就要动态拼接sql脚本
         IQueryable<T> GetEntities(Func<T, bool> whereLambda);

         //新增一个T
         #region 新增一个T

         T Add(T entity);
        
         #endregion

         //删除一个T
         #region 删除一个T
          T DeleteEntity(T entity);
        
         #endregion



         //修改一个用户
         #region 修改一个用户
          T UpdateEntity(T entity);
        
         #endregion

         //分页查询
         #region 分页查询
          IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                    Func<T, bool> whereLambda,
                                                    Expression<Func<T, S>> orderbyLambda,
                                                    bool isAsc

             );
        
         #endregion
    }
}
