using OA.DAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.IDAL;
using OA.BLL;
using OA.DALFactory;

namespace OA.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        public UserInfoService UserInfoService { set; get; }
       

        public ActionResult Index()
        {
            
            BaseDal<UserInfo> based=new BaseDal<UserInfo>();
           // BaseDal<UserInfo> based = StaticDalFactory<UserInfo>.GetBaseDal();
            //based.Add(new UserInfo() { UserId=20,UserName="zidong"});
          //  UserInfoDal id = new UserInfoDal();
           // id.AddUserInfo(new UserInfo() { UserId = 20, UserName = "zidong" });
            ViewData.Model = based.GetEntities(u=>u.UserId>0);
            //int t=13;
            //int pageSize = string.Equals( Request["pageSize"],null)?5 :Int32.Parse(Request["pageSize"]);
            //int pageIndex =string.Equals(Request["pageIndex"],null)?1 : Int32.Parse(Request["pageIndex"]);
            //ViewData.Model = based.GetPageEntities<int>(pageSize, pageIndex, out t, u => u.UserId < 40, u => u.UserId, true);
            return View();
        }
        public ActionResult Details(int id)
        {
            //UserInfoDal ud = new UserInfoDal();
            IUserInfoDal ud = StaticDalFactory.GetUserInfoDal();
            //BaseDal<UserInfo>.GetEntities(u => u.UserId ==id);
            ViewData.Model =ud.GetUserInfoById(id);
            return View();
        }
        public ActionResult Create() {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection c)
        {
            
            int userid = Int32.Parse(Request["UserId"]);
            string username=Request["UserName"];
           
            UserInfoService us = new UserInfoService();
            ViewData.Model = us.Add(new UserInfo() {UserId=userid,UserName=username });
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id) {
           
            UserInfoDal ud = new UserInfoDal();
            UserInfoService us = new UserInfoService();
            //BaseDal<UserInfo>.GetEntities(u => u.UserId ==id);
            // ViewData.Model = ud.GetUserInfoById(id);
            ViewData.Model = UserInfoService.Get(id);
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id,FormCollection c)
        {
            //string username = Request["UserName"];    //注意查看delete的页源码  他们字段是没有name属性的 所以用request对象拿不到属性的值
            int userid = id ;
            UserInfoService us = new UserInfoService();
            ViewData.Model = UserInfoService.Delete(new UserInfo() { UserId = userid });
            return View();
        }
        
        #region 验证码
        public ActionResult ShowVCode() {
          
            Common.ValidateCode validateCode = new Common.ValidateCode();
            
            string strcode=validateCode.CreateValidateCode(5);
            Session["validate"] = strcode;
            byte[] imagebyte = validateCode.CreateValidateGraphic(strcode);
            return File(imagebyte,@"image/jepg");
        }
        #endregion

        #region 处理登录的表单
        public ActionResult Action()
        {
            //第一步:处理验证码
            string vcode = Request["vCode"];
           
            string sessionCode = (string)Session["validate"];
            Session["validate"] = null;
            if (string.IsNullOrEmpty(sessionCode)||vcode!=sessionCode)
            {
                return Content("验证码错误！");
            }

          

            //第二步：处理登录用户名密码
            string username = Request["UserName"];
            string pwd=Request["PassWord"];
           // UserInfoService.

            //如果正确那么跳转到首页
            return View();
        }
        #endregion
        ///

        

    }
}
