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
     public class AdminService
    {
         AdminDal admdal = new AdminDal();
         #region 管理员登录验证
         /// <summary>
         /// 管理员登录验证
         /// </summary>
         /// <param name="num">管理员工号</param>
         /// <param name="pwd">管理员密码</param>
         /// <returns>登录成功的管理员</returns>
         public Admin AdminLoginByNumAndPwd(string num, string pwd)
         {
             Admin adm = new Admin();
             IQueryable<Admin> iq = admdal.GetEntities(u => u.Ad_Num == num && u.Ad_Pwd == pwd);
             if (iq.Count() == 0)
             {
                 return null;
             }
             foreach (var item in iq)
             {
                 adm = item;
             }
             return adm;
         }
         #endregion

         public void Delete(Admin ad) {
             DBSession db = new DBSession();
             admdal.DeleteEntity(ad);
             db.SaveChanges();
         }
        
    }
}
