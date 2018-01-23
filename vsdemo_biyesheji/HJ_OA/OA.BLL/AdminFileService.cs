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
    public class AdminFileService
    {
        AdminFileDal adfdal = new AdminFileDal();
        BaseDal<AdminFile> based=new BaseDal<AdminFile>();
        #region 添加
        public bool AddAdminFile(AdminFile f)
        {
            DBSession db = new DBSession();
            if (adfdal.Add(f) != null)
            {
                db.SaveChanges();
                return true;
            };
            return false;
        }
        #endregion

        #region 学生界面加载admin文件
        public IQueryable<AdminFile> GetPageAdminFile() 
        { 
           IQueryable<AdminFile> iq =adfdal.GetEntities(u => u.AdF_Id > 0).OrderByDescending(u => u.SubTime).Take(4).AsQueryable();
           return iq;
        }
        #endregion
    }
}
