using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HJDemo01.Dao;

namespace HJDemo01.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Index()
        {
            ViewData.Model = StudentDao.queryAll();
            return View();
        }

        public ActionResult Details(int id) {
            ViewData.Model = StudentDao.queryStudent(id);
            return View();
        }

        public ActionResult Edit(int id) {
            ViewData.Model = StudentDao.queryStudent(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id,FormCollection collection)
        {
             string email=Request["Email"];
             string name = Request["Name"];
             string sex=Request["Sex"];
             int age = Int32.Parse(Request["Age"]); ;
            ViewData.Model = StudentDao.EditStudent(id,name,sex,email,age);
            return View();
        }

    }
}
