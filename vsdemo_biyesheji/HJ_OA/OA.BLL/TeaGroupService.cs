using OA.DAL;
using OA.DALFactory;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    
     public class TeaGroupService
    {
         TeaGroupDal tgdal = new TeaGroupDal();

        #region 添加
         public bool Add(TeaGroup tg) {

             DBSession db = new DBSession();
             if (tgdal.Add(tg) != null)
             {
                 db.SaveChanges();
                 return true;
             };
             return false;
         }
        #endregion

        #region 根据tgid查询TeaGroup对象
         public TeaGroup GetTeaGroupById(int tgid) 
         {
             TeaGroup tg = new TeaGroup();
             IQueryable<TeaGroup> iq= tgdal.GetEntities(u => u.TG_Id == tgid);
             if (iq.Count()==0)
             {
                 return null;
             }
             foreach (var item in iq)
             {
                 tg = item;
             }
             return tg;
         }

        #endregion

        #region 删除小组
         public void DeleteTg(int  tgid) {
             TeacherDal teadal=new TeacherDal();
             DBSession db = new DBSession();
             ThemeDal thdal = new ThemeDal();
             IQueryable<TeaGroup> iq= tgdal.GetEntities(u=>u.TG_Id==tgid);
             TeaGroup tg = new TeaGroup();
             foreach (var item in iq)
             {
                 tg = item;
             }
             if (tgdal.DeleteEntity(tg)!=null)
             {
                 IQueryable<Teacher> tiq = teadal.GetEntities(u=>u.TeaGroup==tg);
                 foreach (var item in tiq)
                 {
                     item.TeaGroup = null;
                 }
                 IQueryable<Theme> theiq = thdal.GetEntities(u=>u.TeaGroup==tg);
                 foreach (var item in theiq)
                 {
                     item.TeaGroup = null;
                 }
              };

             db.SaveChanges();
         
         }
        #endregion
    }
}
