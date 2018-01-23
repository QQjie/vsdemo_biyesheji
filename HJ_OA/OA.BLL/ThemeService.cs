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
    public class ThemeService
    {
        ThemeDal themedal = new ThemeDal();

        #region 返回审批通过的所有论文课题
        /// <summary>
        /// 返回审批通过的所有论文课题
        /// </summary>
        /// <returns></returns>
        public IQueryable<Theme> GetAllPassedTheme()
        {
            return themedal.GetEntities(u =>u.Examine == 1);
        }
        #endregion

        #region 返回未审批的所有论文课题
        /// <summary>
        /// 返回未审批的所有论文课题
        /// </summary>
        /// <returns></returns>
        public IQueryable<Theme> GetAllNotPassedTheme(string depname)
        {
            return themedal.GetEntities(u =>u.Examine == 0&&u.Department.Dep_Name==depname);
        }
        #endregion

        #region 根据老师的Id返回所有论文课题
        /// <summary>
        /// 根据老师的Id返回所有论文课题
        /// </summary>
        /// <returns></returns>
        public IQueryable<Theme> GetAllThemeByTeaId(int id)
        {
            return themedal.GetEntities(u => u.Teacher.Tea_Id == id);
        }
        #endregion

        #region 根据老师的名字返回所有论文课题
        /// <summary>
        /// 根据老师的名字返回所有论文课题
        /// </summary>
        /// <returns></returns>
        public IQueryable<Theme> GetAllThemeByTeaName(string teaName)
        {
            return themedal.GetEntities(u => u.Teacher.Tea_Name == teaName);
        }
        #endregion

        #region 根据老师的名字返回所有通过的论文课题
        /// <summary>
        /// 根据老师的名字返回所有论文课题
        /// </summary>
        /// <returns></returns>
        public IQueryable<Theme> GetAllPassedThemeByTeaName(string teaName)
        {
            return themedal.GetEntities(u => u.Teacher.Tea_Name == teaName&&u.Examine==1);
        }
        #endregion

        #region 根据论文课题Id查询论文课题
        /// <summary>
        /// 根据论文课题Id查询论文课题
        /// </summary>
        /// <returns></returns>
        public Theme GetThemeByThemeId(int id)
        {
            Theme t = new Theme();
            IQueryable<Theme> iq = themedal.GetEntities(u => u.Theme_Id == id);
            foreach (var item in iq)
            {
                t = item;
            }
            return t;
        }
        #endregion

        #region 根据专业ID查询出所有的论文课题
        /// <summary>
        /// 根据专业ID查询出所有的论文课题
        /// </summary>
        /// <param name="proid"></param>
        /// <returns></returns>
        public IList<Theme> GetThemesByPro(int proid)
        {
            IList<Theme> t = new List<Theme>();
            IQueryable<Theme> iq = themedal.GetEntities(u => u.Profession.Pro_Id == proid);
            foreach (var item in iq)
            {
                t.Add(item);
            }
            return t;
        }
        #endregion

        #region 添加一个论文课题
        /// <summary>
        /// 添加一个论文课题
        /// </summary>
        /// <returns></returns>
        public bool AddTheme(Theme theme)
        {
            DBSession db = new DBSession();
            if (themedal.Add(theme)!=null) {
                db.SaveChanges();
                return true;
            };
           
            return false;
        }
        #endregion

        #region 添加论文课题集合
        /// <summary>
        /// 添加一个论文课题集合
        /// </summary>
        /// <returns></returns>
        public void AddThemes(IQueryable<Theme> themes)
        {
            DBSession db = new DBSession();
            foreach (var item in themes)
            {
                themedal.Add(item);
            }
            db.SaveChanges();
        }
        #endregion

        #region 修改一个论文课题
        /// <summary>
        /// 修改一个论文课题
        /// </summary>
        /// <returns></returns>
        public void UpdateTheme(Theme theme)
        {
            themedal.UpdateEntity(theme);
        }
        #endregion

        #region 删除一个论文课题
        /// <summary>
        /// 修改一个论文课题
        /// </summary>
        /// <returns></returns>
        public void DeleteTheme(Theme theme)
        {
            
            themedal.UpdateEntity(theme);
        }
        #endregion

        #region 管理员审批通过课题
        public bool PassThe(int theid) {
            DBSession db = new DBSession();
          IQueryable<Theme> iq = themedal.GetEntities(u=>u.Theme_Id==theid);
          Theme t = new Theme();
          foreach (var item in iq)
          {
              t=item;
          }
          t.Examine = 1;
          t.ExamineTime = DateTime.Now;
          if (themedal.UpdateEntity(t)!=null)
          {
              db.SaveChanges();
              return true;
          };
          return false;
        }
        #endregion

        #region 根据课题名查询课题
        public Theme GetThemeByThemeName(string themename) {
            Theme the = new Theme();
            IQueryable<Theme> iq = themedal.GetEntities(u=>u.Theme_Name==themename);
            if (iq.Count()==0)
            {
                return null;
            }
            foreach (var item in iq)
            {
                the = item;
            }
            return the;
        }
        #endregion

    }
}
