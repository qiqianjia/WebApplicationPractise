﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControllersAndActions.Controllers
{
    public class BasicController : IController
    {
        public void Execute(RequestContext P_RequestContext)
        {
            string controller = (string)P_RequestContext.RouteData.Values["controller"];
            string action = (string)P_RequestContext.RouteData.Values["action"];

            P_RequestContext.HttpContext.Response.Write(
                string.Format("Controller:{0},Action:{1}", controller, action));
        }

    }
}