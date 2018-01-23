using MvcFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFirst.Controllers
{
    public class PeopleController : Controller
    {
        //
        // GET: /People/

        public ActionResult Index()
        {
            return View();
        }
        #region   弱类型的页面方式
        public ActionResult ShowPeople(int id) {
            People p = new People() { id = id, name = "huangjie", email = "123@163.com" };

            ViewData["people"] = p;
            
            return View();
        }
        #endregion

        #region  强类型的页面方式
        public ActionResult QiangShowPeople(int id) {
            People p = new People() { id = id, name = "huangqian", email = "520@163.com" };
            ViewData.Model = p;
            return View();
        }

        #endregion
    }
}
