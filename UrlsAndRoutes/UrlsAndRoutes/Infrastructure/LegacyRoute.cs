using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute:RouteBase
    {
        private string[] _urlArray;

        public LegacyRoute(params string[] P_TargetUrlArray)
        {
            _urlArray = P_TargetUrlArray;
        }

        public override RouteData GetRouteData(HttpContextBase P_HttpContext)
        {
            RouteData result = null;

            string requestedURL = P_HttpContext.Request.AppRelativeCurrentExecutionFilePath;

            if (_urlArray.Contains(requestedURL, StringComparer.OrdinalIgnoreCase))
            {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("P_LegacyURL", requestedURL);
            }

            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext P_RequestContext, RouteValueDictionary P_Values)
        {
            VirtualPathData result = null;

            if (P_Values.ContainsKey("P_LegacyURL")
                && _urlArray.Contains((string)P_Values["P_LegacyURL"],StringComparer.OrdinalIgnoreCase))
            {
                result = new VirtualPathData(this
                    , new UrlHelper(P_RequestContext).Content((string)P_Values["P_LegacyURL"]).Substring(1));
            }

            return result;
        }

    }
}