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
    public  class TeaFileService
    {
        TeaFileDal tfdal = new TeaFileDal();

        #region 添加
        public bool AddTeaFile(TeaFile f) 
        {
            DBSession db = new DBSession();
            if (tfdal.Add(f) != null) {
                db.SaveChanges();
                return true;
            };
            return false;
        }
        #endregion
        #region 学生界面加载tea文件
        public IQueryable<TeaFile> GetPageTeaFile()
        {
            IQueryable<TeaFile> iq = tfdal.GetEntities(u => u.TeaF_Id > 0).OrderByDescending(u => u.SubTime).Take(4).AsQueryable();
            return iq;
        }
        #endregion
    }
}
