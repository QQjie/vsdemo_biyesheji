using OA.DAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace OA.UI.Portal.Controllers
{
    public class OrderInfoController : Controller
    {
        //
        // GET: /OrderInfo/

        public ActionResult Index()
        {
            BaseDal<OrderInfo> based = new BaseDal<OrderInfo>();
            ViewData.Model =based.GetEntities(u => u.OrderId < 40);
            return View();
        }

    }
}
