using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Infrastructure
{
    public class CustomRedirectResult : ActionResult
    {
        public string Url { get; set; }

        public override void ExecuteResult(ControllerContext P_Context)
        {
            string fullUrl = UrlHelper.GenerateContentUrl(Url, P_Context.HttpContext);
            P_Context.HttpContext.Response.Redirect(fullUrl);
        }
    }
}