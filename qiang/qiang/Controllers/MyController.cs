using qiang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

namespace qiang.Controllers
{
    public class MyController : Controller
    {

        List<Test> ls = new List<Test>();
        Test t1 = new Test() { id = 01, name = "wo1" };
        Test t2 = new Test() { id = 02, name = "wo2" };
        Test t3 = new Test() { id = 03, name = "wo3" };
        Test t4 = new Test() { id = 04, name = "wo4" };
        Test t5 = new Test() { id = 05, name = "wo5" };
        
        //
        // GET: /My/

           
         

        public ActionResult Index()
        {
            Test t = new Test() { name="hj"};
            ViewData.Model = t;

            return View();
        }
        public ActionResult Show() {
           
            return View();
        }

        public ActionResult Edit() {
            Test t = new Test() { name = "hhhj" };
            ViewData.Model = t;
            return View();
        }

        [HttpPost]   //post请求先给他  他优先级高
        public ActionResult Edit(int id,Test test) {

            return RedirectToAction("Index"); 
        }
        public ActionResult List() {

            ls.Add(t1); ls.Add(t2); ls.Add(t3); ls.Add(t4); ls.Add(t5);

            ViewData.Model = ls;

            return View();
        }

        public ActionResult Details(int id) {

            return View();
        }

        public ActionResult Delete(int id) {


            return View();
        }

    }

    public class MyTest {

       
       

        
    }
}
