using OA.BLL;
using OA.DAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Portal.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() 
        {

            //加载admin文件
            AdminFileService sdfser = new AdminFileService();
            IQueryable<AdminFile> iq = sdfser.GetPageAdminFile();
            IList<string> strl = new List<string>();
            foreach (var item in iq)
            {
                string str = item.SubTime + "=" + item.AdF_Name + "=" + item.Url;
                strl.Add(str);
            }

            Session["strlist"] = strl;

            //加载老师文件
            TeaFileService sdfser2 = new TeaFileService();
            IQueryable<TeaFile> iq2 = sdfser2.GetPageTeaFile();
            IList<string> strl2 = new List<string>();
            foreach (var item in iq2)
            {
                string str = item.SubTime + "=" + item.TeaF_Name + "=" + item.Url;
                strl2.Add(str);
            }
            Session["strlist2"] = strl2; 

            string role=Request["role"];
            string num=Request["Num"];
            string pwd=Request["Pwd"];
            string vcode = Request["vCode"].Trim();
            string sessionCode = (string)Session["validate"];
            Session["validate"] = null;
            if (string.IsNullOrEmpty(sessionCode) || !String.Equals(vcode, sessionCode, StringComparison.CurrentCultureIgnoreCase))
            {
                return Content("验证码错误！");
            }

            if (role == "student") {
                StudentService stus = new StudentService();
                Student s=stus.StudentLoginByNumAndPwd(num,pwd);
                if (s!=null)
                {
                    
                    Session["student"] = s;
                    return RedirectToAction("Index1", "Student");
                }
                else
                {
                    return Content("学生用户名密码不正确1");
                }
            }
            else if (role == "teacher")
            {
                TeacherService teas = new TeacherService();
                Teacher t = teas.TeacherLoginByNumAndPwd(num, pwd);
                if (t != null)
                {
                    Session["teacher"] = t;
                    return RedirectToAction("Index","Teacher");
                }
                else
                {
                    return Content("教师用户名密码不正确2");
                }
            }
            else if (role == "admin")
            {
                AdminService adm= new AdminService();
                Admin admin=new Admin();
              
                Admin admin1 = adm.AdminLoginByNumAndPwd(num, pwd);
                if (admin1 != null)
                {
                    Session["admin"] = admin1;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return Content("管理员用户名密码不正确");
                }
            }

            return Content("用户名密码不正确3");
        }

        public ActionResult lg() {
            return View();
        }
        public ActionResult n() {
            //return RedirectToAction("Index", "Student");
            return View();
        }
        public ActionResult tupian() {
            return View();
        }

        #region 验证码
        public ActionResult ShowVCode()
        {

            Common.ValidateCode validateCode = new Common.ValidateCode();

            string strcode = validateCode.CreateValidateCode(5);
            Session["validate"] = strcode;
            byte[] imagebyte = validateCode.CreateValidateGraphic(strcode);
            return File(imagebyte, @"image/jepg");
        }
        #endregion
    }
}
