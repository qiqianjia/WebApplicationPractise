﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebuggingDemo2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int firstVal = 10;
            int secondVal = 5;
            int result = firstVal / secondVal;
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View(result);
        }
    }
}