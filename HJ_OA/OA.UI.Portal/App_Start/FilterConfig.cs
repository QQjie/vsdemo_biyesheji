﻿using OA.UI.Portal.Models;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionFilterAttribute());
        }
    }
}