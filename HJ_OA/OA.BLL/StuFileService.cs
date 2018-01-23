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
    public class StuFileService
    {
        StuFileDal stufdal = new StuFileDal();

        #region 添加
        public bool  AddStuFile(StuFile f)
        {
            DBSession db = new DBSession();
            if (stufdal.Add(f) != null)
            {
                db.SaveChanges();
                return true;
            };
            return false;
        }
        #endregion

        #region 根据学生获得论文文件
        public StuFile GetStuFileByStu(int  stuid) {
            StuFile sf = new StuFile();
           IQueryable<StuFile> iq= stufdal.GetEntities(u=>u.Student.Stu_Id==stuid&&u.Status==2);
           if (iq.Count()==0)
           {
               return null;
           }
           foreach (var item in iq)
           {
               sf = item; 
           }
           return sf;
        }
        #endregion

        #region 根据院系获得论文文件
        public IQueryable<StuFile> GetStuFileByDep(string dep)
        {
            StuFile sf = new StuFile();
            IQueryable<StuFile> iq = stufdal.GetEntities(u => u.Student.Department.Dep_Name==dep);
            if (iq.Count() == 0)
            {
                return null;
            }
           
            return iq;
        }
        #endregion
        #region 根据院系获得论文文件
        public IQueryable<StuFile> GetStuFileZhongByDep(string dep)
        {
            StuFile sf = new StuFile();
            IQueryable<StuFile> iq = stufdal.GetEntities(u => u.Student.Department.Dep_Name == dep&&u.Status==2);
            if (iq.Count() == 0)
            {
                return null;
            }

            return iq;
        }
        #endregion

        #region 根据院系获得论文文件
        public IQueryable<StuFile> GetStuFileKaiByDep(string dep)
        {
            StuFile sf = new StuFile();
            IQueryable<StuFile> iq = stufdal.GetEntities(u => u.Student.Department.Dep_Name == dep && u.Status == 0);
            if (iq.Count() == 0)
            {
                return null;
            }

            return iq;
        }
        #endregion
        #region 根据院系获得论文文件
        public IQueryable<StuFile> GetStuFileChuByDep(string dep)
        {
            StuFile sf = new StuFile();
            IQueryable<StuFile> iq = stufdal.GetEntities(u => u.Student.Department.Dep_Name == dep && u.Status == 1);
            if (iq.Count() == 0)
            {
                return null;
            }

            return iq;
        }
        #endregion

        public void UpdateStuFile(StuFile file,string name,string intro,string url,int status) 

        {
            DBSession db = new DBSession();
            file.StuF_Intro = intro;
            file.StuF_Name = name;
            file.Url = url;
            file.Status = status;
            file.SubTime = DateTime.Now;
            stufdal.UpdateEntity(file);
            db.SaveChanges();
        }

    }
}
