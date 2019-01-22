using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class UserAgentConstraints:IRouteConstraint
    {
        private string _requiredUserAgent;

        public UserAgentConstraints(string P_RequiredUserAgent)
        {
            _requiredUserAgent = P_RequiredUserAgent;
        }

        public bool Match(HttpContextBase P_HttpContext, Route P_Route, string P_ParamName
            , RouteValueDictionary P_RouteValues, RouteDirection P_RouteDirection)
        {
            return P_HttpContext.Request.UserAgent != null
                && P_HttpContext.Request.UserAgent.Contains(_requiredUserAgent);
        }

    }
}