using OA.DAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Portal.Controllers
{
    public class MyController : Controller
    {
        //
        // GET: /My/

        public ActionResult Index()
        {
            BaseDal<My> based = new BaseDal<My>();
            ViewData.Model = based.GetEntities(u => u.id < 40);
            return View();
        }

    }
}
