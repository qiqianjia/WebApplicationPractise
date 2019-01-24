using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult OutgoingUrlRedirect()
        {
            ViewBag.Controller = "Admin";
            ViewBag.Action = "OutgoingUrlRedirect";
            return View("ActionName");
        }

    }
}