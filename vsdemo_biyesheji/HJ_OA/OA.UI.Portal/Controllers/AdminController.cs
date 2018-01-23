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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        JavaScriptSerializer js = new JavaScriptSerializer();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStuGradeByPro(int pageIndex = 1, int pageSize = 3)
        {
            BaseDal<Grade> based = new BaseDal<Grade>();
            GradeService gradeser = new GradeService();
            ChoseThemeDal c = new ChoseThemeDal();
            GradeDal g=new GradeDal();
            StudentDal studal = new StudentDal();
            int total;
            if (Request["action"] == "gettotal")
            {
                string pro = Request["pro"];
                IQueryable<Grade> iq = gradeser.GetAllGradeByPro(pro, true);
                IQueryable<Student> stuiq = studal.GetEntities(u=>u.Profession.Pro_Name==pro);
                total = iq.Count();
               int prototal = stuiq.Count();
                int[] arr = { pageIndex, pageSize, total,prototal };
                string str = js.Serialize(arr);
                Response.Write(str);
            }
            if (Request["action"] == "getgrade")
            {
                string pro = Request["pro"];
                IQueryable<Grade> iq = gradeser.GetAllGradeByPro(pro, true);
                total = iq.Count();
                int pgindex = Convert.ToInt32(Request["pgindex"]);
                iq = based.GetPageEntities<int?>(pageSize, pgindex, out total, u => u.Student.Profession.Pro_Name == pro, u => u.Score, true);
                IList<string> strl=new List<string>();
                foreach (var item in iq)
                {
                    string s = item.Student.Stu_Name + "-" + item.Theme.Theme_Name + "-" + item.Teacher.Tea_Name + "-" + item.Score;
                    strl.Add(s);
                }
                string str = js.Serialize(strl);
                Response.Write(str);
            }
            Response.End();

            return View();
        }

        #region 查询所有学生
        public ActionResult GetAllStu() {
           // string dep=Request["dep"];
            string pro = Request["pro"];
            StudentService stuser = new StudentService();
            IQueryable<Student> stulist= stuser.GetAllStuByPro(pro);
            if (stulist.Count()==0)
            {
                Response.Write("");
                Response.End();
                 return View();
            }
            IList<string> sl=new List<string>();
            foreach (var item in stulist)
            {
                string str = item.Stu_Num + "-" + item.Stu_Name + "-" + item.Stu_Sex + "-" + item.Stu_Tel;
                sl.Add(str);
            }
            string str1 = js.Serialize(sl);
            Response.Write(str1);
            Response.End();
            return View();
        }
        #endregion

        public ActionResult GetAllDep() 
        {
           
            DepartmentDal depd = new DepartmentDal();
            IQueryable<Department> iq = depd.GetEntities(u=>u.Dep_Id>0);
            string deplist = "";
            foreach (var item in iq)
            {
                deplist = deplist+ item.Dep_Name+"-";
            }
           
            Response.Write(deplist);
            Response.End();
            return View();
        }

        public ActionResult GetAllProByDep() 
        {
            ProfessionDal prod = new ProfessionDal();
           string dep = Request["dep"];
           IQueryable<Profession> iq = prod.GetEntities(u=>u.Department.Dep_Name==dep);
           string str = "";
           foreach (var item in iq)
           {
               str = str + item.Pro_Name + "-";
           }
             //string str = js.Serialize(iq);
            Response.Write(str);
            Response.End();
            return View();
        }

        public ActionResult GetAllClassByPro() {
            ClassDal clad = new ClassDal();
            string pro= Request["pro"];
            IQueryable<Class> iq = clad.GetEntities(u=>u.Profession.Pro_Name==pro);
            string str = "";
            foreach (var item in iq)
            {
                str = str + item.C_Name + "-";
            }
           // string str1 = js.Serialize(iq);
            Response.Write(str);
            Response.End();
            return View();
        }

        #region 添加评分小组
        public ActionResult AddTeaGroup() {

            TeaGroupDal tgdal = new TeaGroupDal();
            string depname = Request["dep"];
            string tgname=Request["tgname"];
            if (string.IsNullOrEmpty(tgname))
            {
               Response.Write("小组名不能为空");
               Response.End();
               return null;
            }
           IQueryable<TeaGroup>  iq = tgdal.GetEntities(u=>u.TG_Name==tgname);
           if (iq.Count()>0)
           {
               Response.Write("小组已存在不能重复添加");
               Response.End();
               return null;
           }
           DepartmentService depser=new DepartmentService();
           Department dep = depser.GetDepartmentByName(depname);
           TeaGroup tg = new TeaGroup();
           tg.TG_Name = tgname;
           tg.Department=dep;
           TeaGroupService tgser = new TeaGroupService();
           tgser.Add(tg);
           Response.Write("添加成功");
           Response.End();
           return null;

        }
        #endregion

        public ActionResult AddOneTea() 
        {
            DepartmentService depser = new DepartmentService();
            TeaGroupDal tegd = new TeaGroupDal();
            TeaGroup   t = new TeaGroup();    
            IQueryable<TeaGroup> iq = tegd.GetEntities(u=>u.TG_Id==1);
            foreach (var item in iq)
            {
                t = item;
            }
            //DepartmentDal depdal = new DepartmentDal();
            TeacherService teaser = new TeacherService();
           string teanum= Request["teanum"];
           string teaname = Request["teaname"];
           string teasex = Request["teasex"];
           string teadep  =  Request["teadep"];
           Department dep= depser.GetDepartmentByName(teadep);
           Teacher tea = new Teacher();
           tea.Tea_Num = teanum;
           if (teanum==""||teaname=="")
           {
                Response.Write("工号姓名不能为空！");
               Response.End();
               return null;
           }

           Teacher tea2 = teaser.GetTeacherByNum(teanum);
           if (tea2!=null)
           {
               Response.Write("此老师已存在,不能重复添加");
               Response.End();
               return null;
           }
           tea.Tea_Name = teaname;
           tea.Tea_Pwd = "123";
           tea.Tea_Sex = teasex;
           tea.Department = dep;
          // tea.Tea_Email = "";
           tea.Tea_Photo = "";
           tea.Tea_Tel = "";
          // tea.TeaGroup = t;
           teaser.AddOneTea(tea);

           Response.Write("添加成功");
           Response.End();
           return null;
        }

        public ActionResult GetAllTeaByDep(int pageIndex = 1, int pageSize = 3)
        {
            Session["nice"] = "123";
            DepartmentService depser=new DepartmentService();
            TeacherService teaser = new TeacherService();
            BaseDal<Teacher> based = new BaseDal<Teacher>();
            //teaser.ge
          
            
            if (Request["action"] == "gettotal")
            {
                string dep = Request["dep"];
                Department department = depser.GetDepartmentByName(dep);
                IQueryable<Teacher> iq = teaser.GetAllTeaByDep((int)department.Dep_Id);
                int total = iq.Count();
                int[] arr = { pageIndex, pageSize, total };
                string str = js.Serialize(arr);
                Response.Write(str);
            }
            if (Request["action"] == "gettea")
            {
                string dep = Request["dep"];
                Department department = depser.GetDepartmentByName(dep);
                IQueryable<Teacher> iq = teaser.GetAllTeaByDep((int)department.Dep_Id);
                int total = iq.Count();
               
                int pgindex = Convert.ToInt32(Request["pgindex"]);
                iq = based.GetPageEntities<int?>(pageSize, pgindex, out total, u => u.Department==department, u =>u.Tea_Id, true);
               // string str = js.Serialize(iq);
                IList<string> strlist =new  List<string>();
                foreach (var item in iq)
                {
                    string str = "";
                    if (item.TeaGroup!=null)
                    {
                        str = item.Tea_Num + "-" + item.Tea_Name + "-" + item.Tea_Sex + "-" + item.TeaGroup.TG_Name + "-" + item.Tea_Id;
                    }
                    if (item.TeaGroup == null)
                    {
                        str = item.Tea_Num + "-" + item.Tea_Name + "-" + item.Tea_Sex + "-" + " " + "-" + item.Tea_Id;
                    }
                    
                    strlist.Add(str);
                   
                }
                string str1 = js.Serialize(strlist);
                Response.Write(str1);
            }
            Response.End();
           
            return View();
        }

        public ActionResult GetTeaGroupByDep() 
        {
            string dep = Request["dep"];
            TeaGroupDal tgdal = new TeaGroupDal();
            TeacherDal teadal = new TeacherDal();
            IQueryable<TeaGroup> iq = tgdal.GetEntities(u=>u.Department.Dep_Name==dep);
            IList<string> ls = new List<string>();
            foreach (var item in iq)
            {
                string lstr="";
                IList<string> l = new List<string>();
                l.Add(item.TG_Id+"-"+item.TG_Name+"-");
                TeaGroup t = item;
                IQueryable<Teacher>  teaiq=  teadal.GetEntities(u=>u.TeaGroup==t);
                foreach (var  item1  in teaiq)
                {
                    l.Add(item1.Tea_Name+"  ");
                }
                for (int i = 0; i < l.Count(); i++)
                {
                     lstr =lstr+l[i];
                }
                ls.Add(lstr);
            }
            string str = js.Serialize(ls);
           // Session["tgmanage"] = ls;
            Response.Write(str);
            string s = string.Empty ;
            Response.End();
            return View();
        }
        #region 修改评分小组的组员
        public ActionResult GetTeaListByTgId() {
            
            TeaGroupService tgser = new TeaGroupService();
            TeacherDal teadal = new TeacherDal();
            int  tgid = Convert.ToInt32(Request["tgid"]);
            TeaGroup tg = tgser.GetTeaGroupById(tgid);
            string str = tg.TG_Name+"-";
            IQueryable<Teacher> teaiq = teadal.GetEntities(u => u.TeaGroup == tg);
            foreach (var item in teaiq)
            {
               str=str+item.Tea_Name+"-";
            }
            Response.Write(str);
            Response.End();
            return null;
        }
        #endregion

        #region 修改评分小组 删除小组成员
        public ActionResult DeleteTeaInTg() 
        {
          string teaname=  Request["teaname"];
          string tgname = Request["tgname"];
          TeacherService teaser = new TeacherService();
          Teacher tea= teaser.GetTeacherByName(teaname);
          //tea.TeaGroup.TG_Id = 8;
          if (teaser.UpdateTeaTg(tea, 7))
          {
              Response.Write("删除成功");
              Response.End();
              return null;
          };
          Response.Write("删除失败");
          Response.End();
          return null;
        }

        #endregion

        #region 添加小组成员
        public ActionResult AddTeaToTg() {
           string teaname= Request["teaname"];
           string tgname = Request["tgname"];
            string dep=Request["dep"];
           TeacherService teaser = new TeacherService();
           Teacher tea= teaser.GetTeacherByName(teaname);
           if (tea==null)
           {
               Response.Write("老师不存在");
               Response.End();
               return null;
           }
           if (tea.TeaGroup!=null)
           {
               Response.Write("该老师已有分组");
               Response.End();
               return null;
           }
           if (tea.Department.Dep_Name!=dep)
           {
               Response.Write("请选择本院的老师");
               Response.End();
               return null;
           }
           if (teaser.AddTeaToTG(teaname,tgname))
           {
               Response.Write("添加成功");
               Response.End();
               return null;
           };
           Response.Write("添加失败");
           Response.End();
           return null;
        }
        #endregion

        #region 删除老师
        public ActionResult ShanChuTea() {
            int teaid = Convert.ToInt32(Request["teaid"]);
             TeacherService teaser = new TeacherService();
             teaser.DeleteTea(teaid);
             Response.Write("删除成功");
             Response.End();
             return null;
        }
        #endregion

        #region 删除小组
        public ActionResult DeleteTg() {
            TeaGroupService tgser = new TeaGroupService();
            int tgid = Convert.ToInt32(Request["tgid"]);
            tgser.DeleteTg(tgid);
            Response.Write("删除成功");
            Response.End();
            return null;
        }
        #endregion

        #region 审批通过课题
        public ActionResult PassTheme() {
            int theid = Convert.ToInt32(Request["theid"]);
            ThemeService theser = new ThemeService();
            if (theser.PassThe(theid))
            {
                Response.Write("已通过");
                Response.End();
                return null;
            };
            Response.Write("通过失败");
            Response.End();
            return null;
        
        }
        #endregion

        public ActionResult GetTeasByTg() {
            Session["tgid"] = null;
            int tgid = Convert.ToInt32(Request["tgid"]);
            Session["tgid"] = tgid;
            TeacherDal teadal1 = new TeacherDal();
            IQueryable<Teacher> iq = teadal1.GetEntities(u =>u.TeaGroup.TG_Id == tgid );
            string str = js.Serialize(iq);
           
            Response.Write(str);

            Response.End();
            return View();
        }
        public ActionResult GetSessionTgId() {
            int tgid = Convert.ToInt32(Session["tgid"]);
            string str = js.Serialize(tgid);
            Response.Write(str);
            Response.End();
            return View();

        }

        public ActionResult GetTeaBySou() { 
          string key=  Request["key"];
          if (string.IsNullOrEmpty(key))
          {
              string str1 = js.Serialize("请输入正确的Key值");
              Response.Write(str1);
              Response.End();
              return View();
          }
          string dep=Request["dep"];
          TeacherDal teadal1 = new TeacherDal();
          IQueryable<Teacher> iq = teadal1.GetEntities(u => (u.Tea_Name.Contains(key) || (u.TeaGroup == null ? false : u.TeaGroup.TG_Name.Contains(key))) && u.Department.Dep_Name == dep);
          IList<string> strlist = new List<string>();
            string str = "";
            foreach (var item in iq)
          {
             
              if (item.TeaGroup != null)
              {
                  str = item.Tea_Id + "-" + item.Tea_Name + "-" + item.Tea_Sex + "-" + item.TeaGroup.TG_Name;
              }
              if (item.TeaGroup == null)
              {
                  str = item.Tea_Id + "-" + item.Tea_Name + "-" + item.Tea_Sex + "-" + " ";
              }
              strlist.Add(str);

          }
            string s = js.Serialize(strlist);
          Response.Write(s);
          Response.End();
          return View();
        }


        public ActionResult GetStuSou(){
            string key = Request["key"];
           GradeDal studal = new GradeDal();
            IQueryable<Grade> iq = studal.GetEntities(u => u.Student.Stu_Name.Contains(key) || u.Theme.Theme_Name.Contains(key) || u.Teacher.Tea_Name.Contains(key));
            IList<string> strl = new List<string>();
            foreach (var item in iq)
            {
                string s = item.Student.Stu_Name + "-" + item.Theme.Theme_Name + "-" + item.Teacher.Tea_Name + "-" + item.Score;
                strl.Add(s);
            }
            string str = js.Serialize(strl);
            Response.Write(str);
            Response.End();
            return View();
        }

        public ActionResult ResPage() {
            Response.Write("/Views/Login/index.cshtml");
            Response.End();
            return null;
            
        }

        #region 管理员获取本院系所有待审批的课题
        public ActionResult GetAllNoPassTheme()
        {
            ThemeService theser = new ThemeService();
            string dep = Request["dep"];

            IQueryable<Theme> iq = theser.GetAllNotPassedTheme(dep);
            IList<string> themelist = new List<string>();
            foreach (var item in iq)
            {
                string str = "";
                str = item.Theme_Id + "-" + item.Theme_Name + "-" + item.Teacher.Tea_Name + "-" + item.Introduce + "-" + item.SubTime;
                themelist.Add(str);
            }
            string str1 = js.Serialize(themelist);
            Response.Write(str1);
            Response.End();
            return null;
        }
        #endregion

        #region 获取所有的论文
        public ActionResult GetAllStuFile() { 
               string dep=Request["dep"];
               StuFileService stufser = new StuFileService();
               IQueryable<StuFile>  stufile  =stufser.GetStuFileByDep(dep);
               if (stufile==null)
               {
                   Response.Write("");
                   Response.End();
                   return null;
               }
               IList<string> strl = new List<string>();
               foreach (var item in stufile)
               {
                   string str = item.Student.Stu_Name + "=" + item.StuF_Name + "=" + item.SubTime + "=" + item.Url;
                   strl.Add(str);
               }
               Session["lunwen"] = strl;
               string str1 = js.Serialize(strl);
               Response.Write(str1);
               Response.End();
               return null;

        }
        #endregion
        #region 获取所有的论文
        public ActionResult GetAllStuFileZhong()
        {
            string dep = Request["dep"];
            StuFileService stufser = new StuFileService();
            IQueryable<StuFile> stufile = stufser.GetStuFileZhongByDep(dep);
            if (stufile == null)
            {
                Response.Write("");
                Response.End();
                return null;
            }
            IList<string> strl = new List<string>();
            foreach (var item in stufile)
            {
                string str = item.Student.Stu_Name + "=" + item.StuF_Name + "=" + item.SubTime + "=" + item.Url;
                strl.Add(str);
            }
            Session["lunwen"] = strl;
            string str1 = js.Serialize(strl);
            Response.Write(str1);
            Response.End();
            return null;

        }
        #endregion
        #region 获取所有的论文
        public ActionResult GetAllStuFileKai()
        {
            string dep = Request["dep"];
            StuFileService stufser = new StuFileService();
            IQueryable<StuFile> stufile = stufser.GetStuFileKaiByDep(dep);
            if (stufile == null)
            {
                Response.Write("");
                Response.End();
                return null;
            }
            IList<string> strl = new List<string>();
            foreach (var item in stufile)
            {
                string str = item.Student.Stu_Name + "=" + item.StuF_Name + "=" + item.SubTime + "=" + item.Url;
                strl.Add(str);
            }
            Session["lunwen"] = strl;
            string str1 = js.Serialize(strl);
            Response.Write(str1);
            Response.End();
            return null;

        }
        #endregion

        public ActionResult LunWenGuanLi() {
            string dep = Request["dep"];
            StuFileService stufser = new StuFileService();
            IQueryable<StuFile> stufile = stufser.GetStuFileByDep(dep);
            if (stufile == null)
            {
                Response.Write("");
                Response.End();
                return null;
            }
            IList<string> strl = new List<string>();
            foreach (var item in stufile)
            {
                string str = item.Student.Stu_Name + "=" + item.StuF_Name + "=" + item.SubTime + "=" + item.Url;
                strl.Add(str);
            }
            Session["lunwen"] = strl;
            return View();
        }

    }
}
