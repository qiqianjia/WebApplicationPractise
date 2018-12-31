using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /*
            routes.MapRoute(
                name: null,
                url: "Page{P_Page}",
                defaults: new { Controller = "Product", action = "List" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
            */

            routes.MapRoute(null, 
                "", 
                new{controller = "Product",action = "List",P_Category = (string)null,P_Page = 1}
            );

            routes.MapRoute(null, 
                "Page{P_Page}", 
                new { controller = "Product", action = "List", P_Category = (string)null }, 
                new { P_Page = @"\d+" }
                );

            routes.MapRoute(null,
                "{P_Category}",
                new { controller = "Product", action = "List", P_Page = 1 }
                );

            routes.MapRoute(null,
                "{ P_Category}/Page{P_Page}",
                new { controller = "Product", action = "List" },
                new { P_Page = @"\d+" }
                );
            
            //routes.MapRoute(null,"{controller}/{action}/{id}");
            routes.MapRoute(null,"{controller}/{action}");
        }
    }
}
