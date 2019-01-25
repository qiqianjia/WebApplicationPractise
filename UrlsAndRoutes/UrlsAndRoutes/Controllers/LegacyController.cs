using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class LegacyController : Controller
    {
        // GET: Legacy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLegacyURL(string P_LegacyURL)
        {
            return View((object)P_LegacyURL);
        }

    }
}