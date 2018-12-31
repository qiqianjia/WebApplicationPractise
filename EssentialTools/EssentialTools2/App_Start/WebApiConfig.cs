using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EssentialTools2
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration P_Config)
        {
            //Do something
            P_Config.MapHttpAttributeRoutes();
            P_Config.Routes.MapHttpRoute(
                name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}