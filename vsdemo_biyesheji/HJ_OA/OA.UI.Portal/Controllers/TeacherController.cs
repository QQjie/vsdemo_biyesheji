using OA.BLL;
using OA.DAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OA.UI.Portal.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        JavaScriptSerializer js = new JavaScriptSerializer();
        public ActionResult Index()
        {
            return View();
        }

        #region 获取老师的详细信息
           public ActionResult GetTeacherDetails() 
        {
            Teacher s = (Teacher)Session["teacher"];
            string str="";
            if (s.TeaGroup==null)
            {
                 str = s.Tea_Num + "-" + s.Tea_Name + "-" + s.Tea_Sex + "-" + s.Department.Dep_Name + "-" +" "+ "-" + s.Tea_Tel + "-" + s.Tea_Email+"-"+s.Tea_Photo;
            }
            if (s.TeaGroup != null)
            {
                str = s.Tea_Num + "-" + s.Tea_Name + "-" + s.Tea_Sex + "-" + s.Department.Dep_Name + "-" + s.TeaGroup.TG_Name + "-" + s.Tea_Tel + "-" + s.Tea_Email + "-" + s.Tea_Photo;
            }
            Response.Write(str);
            Response.End();
            return View();
        }
        #endregion

        #region 获取选了我的课题的学生
            public ActionResult GetMyThemes() 
        {
           
            Teacher tea = (Teacher)Session["teacher"];
            if (tea == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ThemeService themeservice = new ThemeService();
            IQueryable<Theme> iq= themeservice.GetAllThemeByTeaId((int)tea.Tea_Id);
            IList<string> ls = new List<string>();
            foreach (var item in iq)
            {
                string str = item.Theme_Id + "-" + item.Theme_Name + "-" + item.SubTime + "-" + item.Examine;
                ls.Add(str);
            }
            string jstr = js.Serialize(ls);
            Response.Write(jstr);
            Response.End();
            return View();
        }
        #endregion

        #region 获取本源专业
         public ActionResult GetPro() 
        {
            Teacher tea = (Teacher)Session["teacher"];
            if (tea == null)
            {
                return RedirectToAction("Login", "Login");
            }
            DepartmentService deps = new DepartmentService();
           
            ProfessionDal prod = new ProfessionDal();
           IQueryable<Profession> iq= prod.GetEntities(u=>u.Department.Dep_Id==tea.Department.Dep_Id);
           string str="";
            foreach (var item in iq)
           {
               str = str+item.Pro_Name + "-";
           }
           Response.Write(str);
           Response.End();
           return View();
        }
        #endregion
        public ActionResult AddTheme(FormCollection c)
        {
            return View();
        }
        #region 添加课题
         [HttpPost]
        public ActionResult AddTheme() {
             Teacher tea = (Teacher)Session["teacher"];
            if (tea == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string ketiming=Request["ketiming"];
          string xianxuanzhuanye=Request["xianxuanzhuanye"];
           // int xianxuanrenshu = Convert.ToInt32(Request["xianxuanrenshu"]);
            string ketijieshao =Request["ketijieshao"];
            Theme theme = new Theme();
            theme.Theme_Name = ketiming;
           // theme.CountStu = xianxuanrenshu;
            theme.Department = tea.Department;
            theme.Introduce = ketijieshao;
            ProfessionDal prod = new ProfessionDal();
            Profession p = new Profession();
            IQueryable<Profession> iq= prod.GetEntities(u=>u.Pro_Name==xianxuanzhuanye);
            foreach (var item in iq)
            {
                p = item;
            }
            theme.Profession =p;
            theme.SubTime = DateTime.Now;
            theme.Teacher = tea;
            theme.TeaGroup = tea.TeaGroup;
            theme.Examine=0;
            //theme.ExamineStatus=1;
            theme.ExamineTime=DateTime.Now;
            ThemeService themeservice = new ThemeService();
            themeservice.AddTheme(theme);

            return RedirectToAction("Index", "Teacher");
        }
        #endregion

        public ActionResult GetAllMyStudent() {
            //Teacher tea = (Teacher)Session["teacher"];
            //ChoseThemeService choseservice = new ChoseThemeService();
            //IQueryable<ChoseTheme> list= choseservice.GetChosedThemeByTeacher(tea);
            //string str = js.Serialize(list);
            //Response.Write(str);
            //Response.End();
            return View();
        }

        public ActionResult GetMyStu() 
        {
            Teacher tea = (Teacher)Session["teacher"];
            ChoseThemeService choseservice = new ChoseThemeService();   
            IQueryable<ChoseTheme> list = choseservice.GetChosedThemeByTeacher((int)tea.Tea_Id);
            IList<string> liststr = new List<string>();
            string str = "";
            foreach (var item in list)
            {
                str = item.Student.Stu_Name + "-" + item.Theme.Theme_Name + "-" + item.SubTime;
                liststr.Add(str);
            }
            string str1 = js.Serialize(liststr);
            Response.Write(str1);
            Response.End();
            return View();
        }

        #region 查询我负责的论文
          public ActionResult GetAllMyFuZeLunWen() 
        {
            Teacher s = (Teacher)Session["teacher"];
            TeaGroup tg = s.TeaGroup;
            if (tg==null)
            {
                Response.Write("");
                Response.End();
                return View();
            }
            ChoseThemeService chosetser = new ChoseThemeService();
            IQueryable<ChoseTheme> choiq = chosetser.GetAllChoseTheme();
            StuFileService sfser = new StuFileService();
            IList<int> stuidlist=new List<int>();
            IList<string> strlist = new List<string>();
            string str = "";
            int m = 0;
            strlist.Add(tg.TG_Name);
            foreach (var item in choiq)
            {
                if (item.Theme.TeaGroup.TG_Id==tg.TG_Id)
                {
                    //stuidlist.Add((int)item.Student.Stu_Id);
                    StuFile sf = sfser.GetStuFileByStu((int)item.Student.Stu_Id);
                    if (sf == null)
                    {
                        m++;
                    }
                    if (sf != null)
                    {
                        str = sf.Student.Stu_Name + "=" + sf.StuF_Name + "=" + sf.SubTime + "=" + sf.Url;
                        strlist.Add(str);
                    }
                }
            }
            //string str = "";
           
            //for (int i = 0; i < stuidlist.Count(); i++)
            //{
            //   StuFile sf = sfser.GetStuFileByStu(stuidlist[i]);
            //   if (sf == null)
            //   {
            //       m++;
            //   }
            //   if (sf!=null)
            //   {
            //        str = sf.Student.Stu_Name + "=" + sf.StuF_Name + "=" + sf.SubTime+"="+sf.Url;
            //        strlist.Add(str);
            //   }
            //}
            string outstr = js.Serialize(strlist);
            Response.Write(outstr);
            Response.End();
            return null;
        }
        #endregion


    }
}
