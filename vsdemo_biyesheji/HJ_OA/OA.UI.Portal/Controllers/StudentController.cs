using OA.DAL;
using OA.Model;
using OA.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using OA.DALFactory;

namespace OA.UI.Portal.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        
        JavaScriptSerializer js = new JavaScriptSerializer();
        public ActionResult Index(int pageIndex=1,int pageSize=1)
        {
           
            BaseDal<Student> based = new BaseDal<Student>();
            // BaseDal<UserInfo> based = StaticDalFactory<UserInfo>.GetBaseDal();
            //based.Add(new UserInfo() { UserId=20,UserName="zidong"});
            //  UserInfoDal id = new UserInfoDal();
            // id.AddUserInfo(new UserInfo() { UserId = 20, UserName = "zidong" });

            ViewData["pageIndex"] = pageIndex;
            ViewData["pageSize"] = pageSize;
           int total = based.GetEntities(u => u.Stu_Id > 0).Count();
           ViewData["total"] = total;
           ViewData.Model = based.GetPageEntities<string>(pageSize, pageIndex, out total, u => u.Stu_Id > 0, u =>u.Stu_Num, true);
          
            return View();
        }
        public ActionResult Login() 
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection c)
        {
            Student s = new Student();
            string name=Request["stuName"];
            string pwd=Request["stuPwd"];
            StudentDal stud = new StudentDal();
            IQueryable<Student> iq=stud.GetEntities(u=>u.Stu_Name==name&&u.Stu_Pwd==pwd);
            if(iq.Count<Student>()!=0){
                foreach (var item in iq)
                {
                    s = item;
                }
                Session["stu"] = s;
                return RedirectToAction("Detail");
            }

            return RedirectToAction("Error");
        }
        public ActionResult Detail() {
            Student s = new Student();
            s = Session["stu"] as Student;
            // StudentDal stud = new StudentDal();
            // IQueryable<Student> iq=stud.GetEntities(u=>u.Stu_Name==s.Stu_Name&&u.Stu_Pwd==s.Stu_Pwd);
            // foreach (var item in iq)
            // {
            //     s = item;
            // }
            ViewData.Model = s;
                return View();
        }

        public ActionResult Error() {
            return View();
        }
        public ActionResult Test() {
            return View();
        }

        [HttpPost]
        public ActionResult Test(int pageIndex , int pageSize )
        {
            BaseDal<Student> based = new BaseDal<Student>();
             pageIndex=Convert.ToInt32(Request["pageIndex"]);
             pageSize = Convert.ToInt32(Request["pageSize"]);
             int total = based.GetEntities(u => u.Stu_Id > 0).Count();
             ViewData["total"] = total;
             IQueryable<Student> iq = based.GetPageEntities<string>(pageSize, pageIndex, out total, u => u.Stu_Id > 0, u => u.Stu_Num, true);
             Student[] sts = new Student[iq.Count()];
             foreach (var item in iq)
             {
                 int i = 0;
                 sts[i] = item;
                 i++;
             }
             string[] starr = new string[]{"HELLO1","hello2" };
            JavaScriptSerializer js = new JavaScriptSerializer();
            string s = js.Serialize(sts);
            Response.Write(s);
            Response.End();
          //  Response.Write("helloworld");
            return View();
        }


        public ActionResult Login1() {
            Thread.Sleep(3000);
            string s=Request["role"];
            return Content(s+"      "+DateTime.Now.ToString());
        }

        /// <summary>
        /// 学生登录成功的主界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index1() 
        {
            return View();
        }
        #region 获取学生的详细信息
        /// <summary>
        /// 获取学生的个人详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStudentDetails()
        {
            Student s = (Student)Session["student"];
            string str = s.Stu_Num + "-" + s.Stu_Name + "-" + s.Stu_Sex + "-" + s.Profession.Pro_Name + "-" + s.Department.Dep_Name + "-" + s.Class.C_Name + "-" + s.Stu_Tel + "-" + s.Stu_Email + "-" + s.Stu_Photo;
            // string str = js.Serialize(s);
            Response.Write(str);
            Response.End();
            return View();
        }
        #endregion

        public ActionResult SavePhoto() 
        {
            var file = Request.Files["file"];
            string s = file.FileName;    //全路径的名字
            int index = s.LastIndexOf(".");   //获取最后面点的位置 
            string hzm = s.Substring(index); // 获取后缀名
            string path = "/Upload/" + Guid.NewGuid().ToString() + hzm;   //防止命名相同
            //string path = "/Upload/" + file.FileName;
            file.SaveAs(Request.MapPath(path));
            return Content(s + "    " + hzm + "    " + path);
        }

        #region 学生查询本专业的所有论文课题
        /// <summary>
        /// 学生查询本专业的所有论文课题
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页面显示的数据的条数</param>
        /// <returns></returns>
        public ActionResult GetAllTheme(int pageIndex = 1, int pageSize = 2) 
        {
            ThemeService themeservice = new ThemeService();
            BaseDal<Theme> based = new BaseDal<Theme>();
            Student s = (Student)Session["student"];   //session中的数据

            if (s==null)
            {
                return RedirectToAction("Login","Login");
            }

            int proid = (int)s.Profession.Pro_Id;
            IList<Theme> list = themeservice.GetThemesByPro(proid);
            int total = list.Count();
            if (Request["action"]=="gettotal")
            {
                ViewData["total"] = total;
                int[] arr = {pageIndex,pageSize,total};
                string str = js.Serialize(arr);
                Response.Write(str);
            }
            if (Request["action"]=="gettheme")
            {
                int pgindex=Convert.ToInt32(Request["pgindex"]);
                IQueryable<Theme> iq = based.GetPageEntities<int?>(pageSize, pgindex, out total, u => u.Profession.Pro_Id == proid, u => u.Theme_Id, true);
                
                string str = js.Serialize(iq);
                Response.Write(str);
            }
            Response.End();
            return View();
        }
        #endregion


        #region 学生查询自己所选的所有课题包含已经通过和没有通过的
        public ActionResult GetMyThemes() 
        {
          
            ChoseThemeDal d = new ChoseThemeDal();
            Student s = (Student)Session["student"];
            if (s == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ChoseThemeService chosethemeservice = new ChoseThemeService();
            IQueryable<ChoseTheme> c = d.GetEntities(u=>u.CT_Id>0);
          //  IList<Profession> pro= p.GetAllProfession();
            IQueryable<ChoseTheme> themes = d.GetEntities(u => u.Student.Stu_Num ==s.Stu_Num);
            string str = js.Serialize(themes);
            Response.Write(str);
            Response.End();
            return View();
        }
        #endregion

        #region 学生主动放弃论文课题
        public ActionResult FangQi() 
        {
            ChoseThemeDal cd = new ChoseThemeDal();
            Student s = (Student)Session["student"];
            int theme_id = Convert.ToInt32(Request["themeid"]);
            ChoseThemeService chosethemeservice = new ChoseThemeService();
          //  ChoseTheme ct = chosethemeservice.GetChoseThemeByStuId((int)s.Stu_Id);
         //   ChoseTheme chot=chosethemeservice.NoSelectTheme(ct);


           // ChoseTheme c=cd.UpdateEntity(ct);
            //string str = js.Serialize(chot);
            //Response.Write(str);
            //Response.End();
            return View();
        }
        #endregion

        #region 学生选择该论文

        public ActionResult ChoseThisTheme() {
            //ThemeService theser=new ThemeService();
            //Student s = (Student)Session["student"];
            //int theme_id = Convert.ToInt32(Request["themeid"]);
            //Theme t=theser.GetThemeByThemeId(theme_id);
            //ChoseThemeService chosethemeservice = new ChoseThemeService();
            //IQueryable<ChoseTheme> choselist = chosethemeservice.GetChosedThemeByStudent(s);
            //IQueryable<ChoseTheme> choselist1=chosethemeservice.GetChoseThemeByThemeId(theme_id);
            //Theme theme=theser.GetThemeByThemeId(theme_id);
            //int flag =0;
            ////if (choselist1.Count()>theme.CountStu)
            ////{
            ////    flag = 1;
            ////}

            //foreach (var item in choselist)
            //{
            //    if (item.Theme==t)
            //    {
            //        flag = 2;
            //    }
            //}
            //if (flag==0)
            //{
            //    chosethemeservice.AddChoseTheme(s,t,t.Teacher);
            //    string str = js.Serialize("选题成功");
            //    Response.Write(str);
            //    Response.End();
            //    return RedirectToAction("index1", "Student");
            //}if(flag==2){
           
            //    string str1 = js.Serialize("此题已选");
            //    Response.Write(str1);
            //    Response.End();
            //    return RedirectToAction("index1", "Student");
            //}
            //if (flag==1)
            //{
            //     string str1 = js.Serialize("选题人数已满");
            //    Response.Write(str1);
            //    Response.End();
            //    return RedirectToAction("index1", "Student");
            //}
            return null;
        }
        #endregion

        public ActionResult StyleChange() { 
             string s=  Request["upfilestyle"];
             Session["filestyle"] = s;
             return null;
        }

        #region 获取我的论文课题
        public ActionResult GetMyTheme() 
        {
            Session["filestyle"] = "开题报告";
           Student stu=(Student)Session["student"];
           ChoseThemeService choseser = new ChoseThemeService();
           ChoseTheme  ct = choseser.GetStuChoseTheme((int)stu.Stu_Id);
           if (ct==null)
           {
                Response.Write("");
                Response.End();
                return View();
           }
           string str = ct.Teacher.Tea_Name + "-" + ct.Teacher.Tea_Tel + "-" + ct.Teacher.Tea_Email+ "-" +ct.Theme.Theme_Name+ "-" +ct.Theme.TeaGroup.TG_Name+ "-" +ct.Theme.Introduce;
           Response.Write(str);
           Response.End();
           return View();
        }
        #endregion

        public ActionResult GetAllTea() {
            Student  stu=(Student)Session["student"];
            string s="";
            TeacherService teaser = new TeacherService();
            ChoseThemeService choser = new ChoseThemeService();
            IQueryable<Teacher> tealist= teaser.GetAllTeaByDep((int)stu.Department.Dep_Id);
            List<Teacher> tl= tealist.ToList<Teacher>();
            for (int i = 0; i < tl.Count(); i++)
            {
                 int status=1;

                 if (tl[i].Tea_ChoseCount>= tl[i].Tea_StuCount)
                {
                    status = 0;
                }
                s = s + tl[i].Tea_Name + "=" + status+"-";
            }
            //foreach (var item in tealist)
            //{
            //    int status=1;
            //    IQueryable<ChoseTheme> iq = choser.GetChosedThemeByTeacher((int)item.Tea_Id);
            //    if (iq.Count()>=item.Tea_StuCount)
            //    {
            //        status = 0;
            //    }
            //    s = s + item.Tea_Name + "=" + status+"-";
            //}
            Response.Write(s);
            Response.End();
            return View();
        }

        #region 获取老师的所有课题
        public ActionResult GetTeaThemes() {
            string teaname=Request["teaname"];  //获取老师的名字
            string kefou=Request["kefou"];
            ThemeService theser = new ThemeService(); //课题服务
            IQueryable<Theme> theiq = theser.GetAllPassedThemeByTeaName(teaname);//获取老师的课题
            IList<string> outstr=new List<string>();
            string str="";
            foreach (var item in theiq)
            {
                str = item.Theme_Name + "-" + item.Profession.Pro_Name + "-" + item.TeaGroup.TG_Name + "-" + kefou;
                outstr.Add(str);
            }
            string str1 = js.Serialize(outstr);
            Response.Write(str1);
            Response.End();
            return View();
        }
        #endregion

        #region 学生选择课题
        public ActionResult StuXuanzeTheme() { 
            Student stu=(Student)Session["student"];
            string themename=Request["ketiming"];
            ThemeService theser = new ThemeService();
            ChoseThemeService choseser=new ChoseThemeService();
            if (choseser.GetStuChoseTheme((int)stu.Stu_Id)!=null)//没有选过就为true
            {
                Response.Write("不能选择过个课题");
                Response.End();
                return View();
            };

            Theme theme= theser.GetThemeByThemeName(themename);
            ChoseTheme chosetheme = new ChoseTheme();
            chosetheme.Student = stu;
            chosetheme.Theme = theme;
            chosetheme.Teacher=theme.Teacher;
            chosetheme.SubTime=DateTime.Now;
            if (choseser.AddChoseTheme(chosetheme))
            {
                TeacherService ter = new TeacherService();
                ter.UpdateTeaAddCount(theme.Teacher);
                Response.Write("选题成功");
                Response.End();
                return View();
            };
            Response.Write("选题未成功");
            Response.End();
            return View();
        }
        #endregion

        #region 学生页面加载admin文件
        public ActionResult GetAdminFile() 
        {
            Session["t"] = 1;
            AdminFileService sdfser = new AdminFileService();
           IQueryable<AdminFile> iq= sdfser.GetPageAdminFile();
           IList<string> strl = new List<string>();
           foreach (var item in iq)
           {
               string str = item.SubTime + "=" + item.AdF_Name + "=" + item.Url;
               strl.Add(str);
           }
           string str1 = js.Serialize(strl);
           Session["strl"] = strl;
           Response.Write(str1);
           Response.End();
           return View();
        }
        #endregion

        #region 学生页面加载参考文献
        public ActionResult GetTeacherFile()
        {
            TeaFileService sdfser = new TeaFileService();
            IQueryable<TeaFile> iq = sdfser.GetPageTeaFile();
            IList<string> strl = new List<string>();
            foreach (var item in iq)
            {
                string str = item.SubTime + "=" + item.TeaF_Name + "=" + item.Url;
                strl.Add(str);
            }
            string str1 = js.Serialize(strl);
            Response.Write(str1);
            Response.End();
            return View();
        }
        #endregion

        #region 修改电话号码

        public ActionResult ModifyTel() 
        {
            string tel = Request["tgname"];
            if (!System.Text.RegularExpressions.Regex.IsMatch(tel, @"^[1]+[3,5]+\d{9}"))
            {
                Response.Write("请输入正确的电话号码");
                Response.End();
                return View();
            }
            Student stu=(Student)Session["student"];
            StudentService stuser = new StudentService();
            if (stuser.ModifyStudentTel(stu,tel))
            {
                Response.Write("修改成功");
                Response.End();
                return View();
            }
            Response.Write("修改失败");
            Response.End();
            return View();
        }
        #endregion

        #region 修改电话号码

        public ActionResult ModifyEmail()
        {
            string email = Request["tgname"];
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"\\w{1,}@\\w{1,}\\.\\w{1,}"))
            {
                Response.Write("请输入正确的邮箱");
                Response.End();
                return View();
            }
            Student stu = (Student)Session["student"];
            StudentService stuser = new StudentService();
            if (stuser.ModifyStudentTel(stu, email))
            {
                Response.Write("修改成功");
                Response.End();
                return View();
            }
            Response.Write("修改失败");
            Response.End();
            return View();
        }
        #endregion

    }
}
