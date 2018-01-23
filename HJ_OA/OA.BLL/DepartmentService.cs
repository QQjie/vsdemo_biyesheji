
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.Model;

namespace OA.BLL
{
    public class DepartmentService
    {
        DepartmentDal depDal=new DepartmentDal();

        #region 根据传入的id查询院系对象
        /// <summary>
        ///根据传入的id查询院系对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department GetDepartmentBuId(int id)
        {
            Department dep = new Department();
            IQueryable<Department> depiq = depDal.GetEntities(dp => dp.Dep_Id == id);
            if (depiq.Count() == 0)
            {
                return null;
            }
            foreach (var item in depiq)
            {
                dep = item;
            }
            return dep;
        }
        #endregion

        #region 根据名字查询院系
        /// <summary>
        /// 根据名字查询院系
        /// </summary>
        /// <param name="depname"></param>
        /// <returns></returns>
        public Department GetDepartmentByName(string  depname) 
        {
            Department dep = new Department();
           IQueryable<Department> depiq=depDal.GetEntities(dp=>dp.Dep_Name==depname);
           if (depiq.Count()==0)
           {
               return null;
           }
           foreach (var item in depiq)
           {
               dep = item;   
           }
           return dep;
        }
        #endregion


    }
}
