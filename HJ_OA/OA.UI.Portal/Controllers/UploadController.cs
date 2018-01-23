using OA.BLL;
using OA.DAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Portal.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        #region 管理员文件上传
          public ActionResult AdminFileUpLoad()
        {
            AdminFileService adfs = new AdminFileService();
            AdminFileDal adfd  = new AdminFileDal();
            Admin ad=(Admin)Session["admin"];
            var file = Request.Files["file"];
            if (file.ContentLength == 0)
            {
                return Content("请选择要上传的文件");
            }
            var filename = Request["filename"];
           
           IQueryable<AdminFile> iq= adfd.GetEntities(u=>u.AdF_Name==filename);
           if (iq.Count()>=1)
           {
               return Content("此文件或文件名已存在");
           }
            var intro = Request["intro"];
            AdminFile adf = new AdminFile();
            adf.AdF_Intro = intro;
            adf.AdF_Name = filename;
            adf.Admin= ad;
            adf.SubTime = DateTime.Now;
            string s = file.FileName;    //全路径的名字
            int index = s.LastIndexOf(".");   //获取最后面点的位置
            string hzm = s.Substring(index); // 获取后缀名
            // string path = "/Upload/" + Guid.NewGuid().ToString() + hzm;   //防止命名相同
            string path = "/Upload/" + Guid.NewGuid().ToString() + filename + hzm;
            adf.Url = path;
            adfs.AddAdminFile(adf);
            file.SaveAs(Request.MapPath(path));
            return Content("上传成功");
        }
        #endregion

        #region 老师下载学生论文初稿
          public ActionResult DownLoadStuChu() 
          {
                string name=Request["name"];
                StuFile file = new StuFile();
                StuFileDal stufile = new StuFileDal();
                IQueryable<StuFile>  stuf = stufile.GetEntities(u=>u.Student.Stu_Name==name&&u.Status==1);
                foreach (var item in stuf)
                {
                    file = item;
                }
                string filepath = file.Url;
                string filePath = Server.MapPath(filepath);
                FileInfo fileInfo = new FileInfo(filePath);
                try
                {
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Url);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.AddHeader("Content-Transfer-Encoding", "binary");
                    Response.ContentType = "application/octet-stream";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                    Response.WriteFile(fileInfo.FullName);
                    Response.Flush();
                    Response.End();

                }
                catch (Exception)
                {
                    return Content("此文件已被删除");
                }
                return null;
          }
          #endregion

          #region 老师上传学生论文初稿
          public ActionResult TeaUpStuChu() 
          {
              string p = Server.MapPath("");
              StuFileService adfs = new StuFileService();
              StuFileDal adfd = new StuFileDal();
              StuFile adf = new StuFile();
              string name=Request["name"];
              StudentDal stud = new StudentDal();

              Student stu   = new Student();
             IQueryable<Student> siq =stud.GetEntities(U=>U.Stu_Name==name);
             foreach (var item in siq)
             {
                 stu = item;
             }
             var file = Request.Files["file2"];
             if (file.ContentLength == 0)
             {
                 return Content("请选择要上传的文件");
             }
             var intro = Request["intro2"];
             var filename = Request["filename2"];
             IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Name == name && u.Status == 1);
             if (iq.Count() == 0)
             {
                 return Content("改还未提交过论文初稿");
             }
             foreach (var item in iq)
             {
                 adf = item;
             }
           
             string s = file.FileName;    //全路径的名字
             int index = s.LastIndexOf(".");   //获取最后面点的位置
             string hzm = s.Substring(index); // 获取后缀名
             try
             {
                 System.IO.File.Delete(p + "/" + stu.Department.Dep_Name + "/论文初稿/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文初稿" + hzm);
             }
             catch (Exception)
             {
             }
             string path = "/Upload/管理与信息工程学院/论文初稿/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文初稿" + hzm; ;   //防止命名相同

             adfs.UpdateStuFile(adf, filename, intro, path, 4);
             file.SaveAs(Request.MapPath(path));
             return Content("上传成功");

          }
          #endregion

        public ActionResult FileDownLoad()
        {
            string filepath=Request["filename"];
            string[] strs=filepath.Split(new Char[]{'/'});
            string fileName = strs[4];//获取文件的名字
            //string fileName = "cbef0316-53ae-451f-90f6-ed0a2754a37f.jpg";//客户端保存的文件名
            //string filePath = Server.MapPath("/Upload/cbef0316-53ae-451f-90f6-ed0a2754a37f.jpg");//路径
            string filePath = Server.MapPath(filepath);
            FileInfo fileInfo = new FileInfo(filePath);
            try
            {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();

            }
            catch (Exception)
            {
                
                return Content("此文件已被删除");
            }
          
            return null;
        }
        public ActionResult FileDownLoad1()
        {
            string filepath = Request["filename"];
            string[] strs = filepath.Split(new Char[] { '/' });
            string fileName = strs[2];//获取文件的名字
            //string fileName = "cbef0316-53ae-451f-90f6-ed0a2754a37f.jpg";//客户端保存的文件名
            //string filePath = Server.MapPath("/Upload/cbef0316-53ae-451f-90f6-ed0a2754a37f.jpg");//路径
            string filePath = Server.MapPath(filepath);
            FileInfo fileInfo = new FileInfo(filePath);
            try
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.AddHeader("Content-Transfer-Encoding", "binary");
                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.WriteFile(fileInfo.FullName);
                Response.Flush();
                Response.End();

            }
            catch (Exception)
            {

                return Content("此文件已被删除");
            }

            return null;
        }

        #region 学生论文上传
        public ActionResult  StuFileUpLoad()
        {
           
            var file = Request.Files["file"];
            if (file.ContentLength == 0)
            {
                return Content("请选择要上传的文件");
            }
            var intro = Request["intro"];
            var filename = Request["filename"];
            StuFileService adfs = new StuFileService();
            StuFileDal adfd = new StuFileDal();
            Student stu  = (Student)Session["student"];
           string filestyle=Request["style"];

            string dir = stu.Department.Dep_Name;
            if (Directory.Exists(Server.MapPath("/Upload/"+dir)) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Server.MapPath("/Upload/" + dir));
            }
            if (filestyle=="开题报告")
            {
                if (Directory.Exists(Server.MapPath("/Upload/"+dir+"/"+filestyle)) == false)//如果不存在就创建file文件夹
                {
                 Directory.CreateDirectory(Server.MapPath("/Upload/" + dir + "/" + filestyle));
                }
                IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id&&u.Status==0);
                if (iq.Count() >= 1)
                {
                    return Content("您已上传过开题报告，如果想要覆盖已上传的开题报告，请选择下面的覆盖上传！");
                }
                StuFile adf = new StuFile();
                // adf.Admin = ad;
                adf.StuF_Intro = intro;
                adf.StuF_Name = stu.Stu_Num+"-"+stu.Stu_Name+"-"+"开题报告";
                adf.Student = stu;
                adf.Status = 0;
                adf.SubTime = DateTime.Now;
                string s = file.FileName;    //全路径的名字
                int index = s.LastIndexOf(".");   //获取最后面点的位置
                string hzm = s.Substring(index); // 获取后缀名
                string path = "/Upload/" + dir + "/" + filestyle+"/" + stu.Stu_Id + "-" + stu.Stu_Name + "-" + "开题报告" + hzm;   //防止命名相同
                adf.Url = path;
                adfs.AddStuFile(adf);
                file.SaveAs(Request.MapPath(path));
                return Content("上传成功");
            }
            if (filestyle == "论文初稿")
            {
                if (Directory.Exists(Server.MapPath("/Upload/" + dir + "/" + filestyle)) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(Server.MapPath("/Upload/" + dir + "/" + filestyle));
                }
                IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id && u.Status == 1);
                if (iq.Count() >= 1)
                {
                    return Content("您已上传过论文初稿，如果想要覆盖已上传的论文初稿，请选择下面的覆盖上传！");
                }
                StuFile adf = new StuFile();
                // adf.Admin = ad;
                adf.StuF_Intro = intro;
                adf.StuF_Name = stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文初稿";
                adf.Student = stu;
                adf.Status = 1;
                adf.SubTime = DateTime.Now;
                string s = file.FileName;    //全路径的名字
                int index = s.LastIndexOf(".");   //获取最后面点的位置
                string hzm = s.Substring(index); // 获取后缀名
                string path = "/Upload/" + dir + "/" + filestyle +"/" +stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文初稿" + hzm;   //防止命名相同
                adf.Url = path;
                adfs.AddStuFile(adf);
                file.SaveAs(Request.MapPath(path));
                return Content("上传成功");
            }
            if (filestyle == "论文终稿")
            {
                if (Directory.Exists(Server.MapPath("/Upload/" + dir + "/" + filestyle)) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(Server.MapPath("/Upload/" + dir + "/" + filestyle));
                }
                IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id && u.Status == 2);
                if (iq.Count() >= 1)
                {
                    return Content("您已上传过论文，如果想要覆盖已上传的论文，请选择下面的覆盖上传！");
                }
                StuFile adf = new StuFile();
                // adf.Admin = ad;
                adf.StuF_Intro = intro;
                adf.StuF_Name = stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文终稿";
                adf.Student = stu;
                adf.Status = 2;
                adf.SubTime = DateTime.Now;
                string s = file.FileName;    //全路径的名字
                int index = s.LastIndexOf(".");   //获取最后面点的位置
                string hzm = s.Substring(index); // 获取后缀名
                string path = "/Upload/" + dir + "/" + filestyle+"/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文终稿" + hzm;   //防止命名相同
                // string path = "/Upload/"+ filename + hzm;
                adf.Url = path;
                adfs.AddStuFile(adf);
                //string path = "/Upload/" + file.FileName;

                //string activeDir = @"C:\myDir";
                //string newPath = System.IO.Path.Combine(activeDir, "mySubDirOne");
                //System.IO.Directory.CreateDirectory(newPath);

                file.SaveAs(Request.MapPath(path));
                //return Content(s + "    " + hzm + "    " + path);
                return Content("上传成功");
            }

            return Content("上传成功");
        }
        #endregion

        #region 学生第二次论文上传
        public ActionResult StuFileOverUpLoad()
        {
            string p = Server.MapPath("");
            //System.IO.File.Delete(p + "/管理与信息工程学院/论文终稿/201301020127-黄杰-论文终稿.jpg");
            //StuFileService adfs = new StuFileService();
            //StuFileDal adfd = new StuFileDal();
            //Student stu = (Student)Session["student"];
            //var file = Request.Files["file1"];
            //if (file.ContentLength==0)
            //{ 
            //     return Content("请选择要上传的文件");
            //}
            //var filename = Request["filename1"];
            //var intro = Request["intro1"];
            //IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id&&u.Status==1);
            //if (iq.Count()==0)
            //{
            //     return Content("你还未提交过论文");
            //}
            
            //StuFile adf = new StuFile();
            //foreach (var item in iq)
            //{
            //    adf = item;
            //}
            //string s = file.FileName;    //全路径的名字
            //int index = s.LastIndexOf(".");   //获取最后面点的位置
            //string hzm = s.Substring(index); // 获取后缀名
            //string path = "/Upload/" +filename+ Guid.NewGuid().ToString() + hzm;   //防止命名相同
            //adfs.UpdateStuFile(adf,filename,intro,path);
            //file.SaveAs(Request.MapPath(path));
            //return Content("上传成功");
             var file = Request.Files["file1"];
            if (file.ContentLength == 0)
            {
                return Content("请选择要上传的文件");
            }
            var intro = Request["intro1"];
            var filename = Request["filename1"];
           
            StuFileService adfs = new StuFileService();
            StuFileDal adfd = new StuFileDal();
            Student stu  = (Student)Session["student"];

            var filename1 = stu.Stu_Num + "-" + stu.Stu_Name + "-" + "开题报告";
           string filestyle=Request["style"];

            string dir = stu.Department.Dep_Name;
            StuFile adf = new StuFile();
            if (filestyle=="开题报告")
            {
                IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id && u.Status == 0);
                if (iq.Count() == 0)
                {
                    return Content("你还未提交过开题报告");
                }
                foreach (var item in iq)
                {
                    adf = item;
                }
                
                
                string s = file.FileName;    //全路径的名字
                int index = s.LastIndexOf(".");   //获取最后面点的位置
                string hzm = s.Substring(index); // 获取后缀名
                try
                {
                    System.IO.File.Delete(p + "/"+stu.Department.Dep_Name+"/开题报告/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "开题报告" + hzm);
                }
                catch (Exception)
                {
                }
                string path = "/Upload/" + dir + "/" + filestyle+"/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "开题报告" + hzm;   //防止命名相同
               
                adfs.UpdateStuFile(adf, filename1, intro, path,0);
                file.SaveAs(Request.MapPath(path));
                return Content("上传成功");
            }
            if (filestyle == "论文初稿")
            {
                IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id && u.Status == 1);
                if (iq.Count() == 0)
                {
                    return Content("你还未提交过论文初稿");
                }
                foreach (var item in iq)
                {
                    adf = item;
                }
                string s = file.FileName;    //全路径的名字
                int index = s.LastIndexOf(".");   //获取最后面点的位置
                string hzm = s.Substring(index); // 获取后缀名
                try
                {
                    System.IO.File.Delete(p + "/"+stu.Department.Dep_Name+"/论文初稿/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文初稿" + hzm);
                }
                catch (Exception)
                {
                }
                string path = "/Upload/" + dir + "/" + filestyle + "/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文初稿" + hzm;   //防止命名相同
                
                adfs.UpdateStuFile(adf, filename, intro, path,1);
                file.SaveAs(Request.MapPath(path));
                return Content("上传成功");
            }
            if (filestyle == "论文终稿")
            {
                IQueryable<StuFile> iq = adfd.GetEntities(u => u.Student.Stu_Id == stu.Stu_Id && u.Status == 2);
                if (iq.Count() == 0)
                {
                    return Content("你还未提交过论文终稿");
                }
                foreach (var item in iq)
                {
                    adf = item;
                }
                string s = file.FileName;    //全路径的名字
                int index = s.LastIndexOf(".");   //获取最后面点的位置
                string hzm = s.Substring(index); // 获取后缀名
                try
                {
                    System.IO.File.Delete(p + "/"+stu.Department.Dep_Name+"/论文终稿/" + stu.Stu_Num + "-" + stu.Stu_Name + "-" + "论文终稿" + hzm);
                }
                catch (Exception)
                {
                }
                string path = "/Upload/" + dir + "/" + filestyle + "/" + stu.Stu_Num   + "-" + stu.Stu_Name + "-" + "论文终稿" + hzm;   //防止命名相同
             
                adfs.UpdateStuFile(adf, filename, intro, path,2);
                file.SaveAs(Request.MapPath(path));
                return Content("上传成功");
            }
            return null;
        }
        #endregion

        #region 老师上传资料
        public ActionResult TeaFileUpLoad()
        {
            TeaFileService adfs = new TeaFileService();
            TeaFileDal adfd = new TeaFileDal();
            Teacher tea= (Teacher)Session["teacher"];
            var file = Request.Files["file"];
            if (file.ContentLength == 0)
            {
                return Content("请选择要上传的文件");
            }
            var filename = Request["filename"];
            IQueryable<TeaFile> iq = adfd.GetEntities(u => u.Teacher.Tea_Id == tea.Tea_Id);
            var intro = Request["intro"];
            TeaFile adf = new TeaFile();
            // adf.Admin = ad;
            adf.TeaF_Intro = intro;
            adf.TeaF_Name = filename;
            adf.Teacher = tea;
            adf.SubTime = DateTime.Now;
            string s = file.FileName;    //全路径的名字
            int index = s.LastIndexOf(".");   //获取最后面点的位置
            string hzm = s.Substring(index); // 获取后缀名
            string path = "/Upload/" + filename + Guid.NewGuid().ToString() + hzm;   //防止命名相同
            // string path = "/Upload/"+ filename + hzm;
            adf.Url = path;
            adfs.AddTeaFile(adf);
            //string path = "/Upload/" + file.FileName;

            //string activeDir = @"C:\myDir";
            //string newPath = System.IO.Path.Combine(activeDir, "mySubDirOne");
            //System.IO.Directory.CreateDirectory(newPath);

            file.SaveAs(Request.MapPath(path));
            //return Content(s + "    " + hzm + "    " + path);
            return Content("上传成功");
        }
        #endregion
    }
}
