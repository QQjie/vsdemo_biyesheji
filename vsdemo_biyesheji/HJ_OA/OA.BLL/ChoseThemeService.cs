using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model;
using OA.DAL;
using OA.DALFactory;

namespace OA.BLL
{
    public class ChoseThemeService
    {
        //status：0代表未审批的 1代表审批不通过 2学生主动放弃 3审批通过
        ChoseThemeDal ctDal = new ChoseThemeDal();

        #region 获取所有
        public IQueryable<ChoseTheme> GetAllChoseTheme() {
            return ctDal.GetEntities(u=>u.CT_Id>0);
        }
        #endregion

        #region 学生查询自己已选的课题
        public ChoseTheme GetStuChoseTheme(int stuid) {
            ChoseTheme ct = new ChoseTheme();
           IQueryable<ChoseTheme> iq= ctDal.GetEntities(u=>u.Student.Stu_Id==stuid);
           if (iq.Count()==0)
           {
               return null;
           }
           foreach (var item in iq)
           {
               ct = item;
           }
           return ct;
        }
        #endregion

        #region 添加一个chosetheme
        public bool AddChoseTheme(ChoseTheme cho) 
        {
            DBSession db = new DBSession();
            if (ctDal.Add(cho)!=null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        //#region 查询已选论文课题状态
        ///// <summary>
        ///// 0:未审批
        ///// 1：审批未通过
        ///// 2：学生主动放弃
        ///// 3：审批通过
        ///// </summary>
        ///// <param name="ct"></param>
        ///// <returns></returns>
        //public int GetChoseThemeStatus(ChoseTheme ct)
        //{
        //    return ct.Status;
        //}
        //#endregion

        //#region 学生查询已选论文课题
        ///// <summary>
        ///// 学生查询已选论文课题
        ///// </summary>
        ///// <param name="stu"></param>
        ///// <returns></returns>
        //public IQueryable<ChoseTheme> GetChosedThemeByStudent(Student stu)
        //{
        //    IList<Theme> list = new List<Theme>();
        //    ChoseTheme cht = new ChoseTheme();
        //    IQueryable<ChoseTheme> ctiq = ctDal.GetEntities(ct => ct.Student.Stu_Num == stu.Stu_Num);
        //    return ctiq;
        //}
        //#endregion

        //#region 学生查询已选的并且通过的论文课题
        ///// <summary>
        ///// 学生查询已选的并且通过的论文课题
        ///// </summary>
        ///// <param name="stu"></param>
        ///// <returns></returns>
        //public IList<Theme> GetChosedAndPassedThemeByStudent(Student stu)
        //{

        //    IList<Theme> tlist = new List<Theme>();
        //    IQueryable<ChoseTheme> ctiq = ctDal.GetEntities(ct => ct.Student.Stu_Num == stu.Stu_Num && ct.Status == 3);
        //    if (ctiq.Count() == 0)
        //    {
        //        return null;
        //    }
        //    foreach (var item in ctiq)
        //    {
        //        tlist.Add(item.Theme);
        //    }
        //    return tlist;
        //}
        //#endregion

        //#region 学生查询已选的并且未通过的论文课题
        ///// <summary>
        ///// 学生查询已选的并且未通过的论文课题
        ///// </summary>
        ///// <param name="stu"></param>
        ///// <returns></returns>
        //public IList<Theme> GetChosedAndNotPassedThemeByStudent(Student stu)
        //{

        //    IList<Theme> tlist = new List<Theme>();
        //    IQueryable<ChoseTheme> ctiq = ctDal.GetEntities(ct => ct.Student.Stu_Num == stu.Stu_Num && ct.Status < 3);
        //    if (ctiq.Count() == 0)
        //    {
        //        return null;
        //    }
        //    foreach (var item in ctiq)
        //    {
        //        tlist.Add(item.Theme);
        //    }
        //    return tlist;
        //}
        //#endregion


        #region 老师查询已选论文课题
        /// <summary>
        /// 老师查询已选论文课题
        /// </summary>
        /// <param name="tea"></param>
        /// <returns></returns>
        public IQueryable<ChoseTheme> GetChosedThemeByTeacher(int  teaid)
        {
            // IList<ChoseTheme> list = new List<ChoseTheme>();
            ChoseTheme cht = new ChoseTheme();
            IQueryable<ChoseTheme> ctiq = ctDal.GetEntities(ct => ct.Teacher.Tea_Id == teaid);

            return ctiq;
        }
        #endregion

        //    #region 老师查询已选的并且通过的论文课题
        //    /// <summary>
        //    /// 老师查询已选的并且通过的论文课题
        //    /// </summary>
        //    /// <param name="tea"></param>
        //    /// <returns></returns>
        //    public IList<Theme> GetChosedAndPassedThemeByTeacher(Teacher tea)
        //    {

        //        IList<Theme> tlist = new List<Theme>();
        //        IQueryable<ChoseTheme> ctiq = ctDal.GetEntities(ct => ct.Teacher.Tea_Num == tea.Tea_Num && ct.Status == 3);
        //        if (ctiq.Count() == 0)
        //        {
        //            return null;
        //        }
        //        foreach (var item in ctiq)
        //        {
        //            tlist.Add(item.Theme);
        //        }
        //        return tlist;
        //    }
        //    #endregion

        //    #region 老师查询已选的并且未通过的论文课题
        //    /// <summary>
        //    /// 老师查询已选的并且未通过的论文课题
        //    /// </summary>
        //    /// <param name="tea"></param>
        //    /// <returns></returns>
        //    public IList<Theme> GetChosedAndNotPassedThemeByTeacher(Teacher tea)
        //    {

        //        IList<Theme> tlist = new List<Theme>();
        //        IQueryable<ChoseTheme> ctiq = ctDal.GetEntities(ct => ct.Teacher.Tea_Num == tea.Tea_Num && ct.Status <3);
        //        if (ctiq.Count() == 0)
        //        {
        //            return null;
        //        }
        //        foreach (var item in ctiq)
        //        {
        //            tlist.Add(item.Theme);
        //        }
        //        return tlist;
        //    }
        //    #endregion

        //    #region 学生选择一个课题学生即新增加一个ChoseTheme对象

        //    public void AddChoseTheme(Student stu,Theme theme,Teacher tea) 
        //    {
        //        DBSession db = new DBSession();
        //        DateTime dt=DateTime.Now;
        //        ChoseTheme cht = new ChoseTheme() {Student=stu,Theme=theme,Teacher=tea,Status=0,SubTime=dt };
        //        ctDal.Add(cht);
        //        db.SaveChanges();

        //    }
        //    #endregion

        //    #region 老师审批通过论文课题
        //    /// <summary>
        //    /// 老师审批通过论文课题
        //    /// </summary>
        //    /// <param name="ct"></param>
        //    public void ExmainePassByTeacher(ChoseTheme ct) 
        //    {
        //        ct.Status = 3;
        //        ctDal.UpdateEntity(ct);
        //       // db.SaveChanges();
        //    }
        //    #endregion

        //    #region 老师审批不通过的论文课题
        //    /// <summary>
        //    /// 老师审批不通过的论文课题
        //    /// </summary>
        //    /// <param name="ct"></param>
        //    public void ExmaineNotPassByTeacher(ChoseTheme ct)
        //    {
        //        ct.Status =1;
        //        ctDal.UpdateEntity(ct);
        //       // db.SaveChanges();
        //    }
        //    #endregion

        //    #region 判断学生是否已经有确定论文

        //    public bool IsSelectTheme(Student stu) 
        //    {
        //        IQueryable<ChoseTheme> iq = ctDal.GetEntities(cth=>cth.Student.Stu_Num==stu.Stu_Num&&cth.Status==3);
        //        if (iq.Count()>=1)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    #endregion

        //    #region 学生放弃
        //    /// <summary>
        //    /// 学生放弃已选课题
        //    /// </summary>
        //    /// <param name="ct"></param>
        //    public ChoseTheme NoSelectTheme(ChoseTheme ct) 
        //    {
        //        ct.Status = 2;

        //       DBSession db = new DBSession();
        //       ChoseTheme chot = ctDal.UpdateEntity(ct);
        //       db.SaveChanges();
        //      // MySession.TranCommit();
        //       return chot;
        //    }
        //    #endregion

        //    #region 根据id查询chosetheme对象
        //    public IQueryable<ChoseTheme> GetChoseThemeByThemeId(int theid) 
        //    {
        //        IQueryable<ChoseTheme> iq= ctDal.GetEntities(u=>u.CT_Id==theid);

        //        return iq;
        //    }
        //    #endregion

        //    public ChoseTheme GetChoseThemeByStuId(int id) 
        //    {
        //        ChoseTheme c = new ChoseTheme();
        //      IQueryable<ChoseTheme> iq = ctDal.GetEntities(u=>u.Student.Stu_Id==id);
        //      foreach (var item in iq)
        //      {
        //          c = item;
        //      }
        //      return c;
        //    }
        //}


    }
}
