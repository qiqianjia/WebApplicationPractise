using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /*
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            */

            /*
            Route myRoute = new Route(
                "{controller}/{action}",
                new MvcRouteHandler()
                );
            routes.Add("MyRoute", myRoute);
            */

            /*
            routes.MapRoute("ShopSchema", "Shop/OldAction"
                , new { contoller = "Home", action = "Index" });

            routes.MapRoute("ShopSchema", "Shop/{action}"
                , new { controller = "Home" });

            routes.MapRoute("", "X{controller}/{action}");

            routes.MapRoute("MyRoute", "{controller}/{action}"
                ,new { controller="Home",action="Index"});

            routes.MapRoute("","Public/{controller}/{action}"
                ,new { controller="Home",action="Index"});
                
             */

            /*
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}"
                , new { controller = "Home", action = "Index", id = "DefaultId" });
             */

            /*
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}"
                ,new { controller="Home",action="Index",id=UrlParameter.Optional}); 
                
             */

            /*
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            */

            /*
            routes.MapRoute("AdditionalControllerRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new[] { "URLsAndRoutes.AdditionalControllers"}
                );

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new[] { "URLsAndRoutes.Controllers" }
                );
                */

            /*
            Route myRoute = routes.MapRoute("AdditionalControllerRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new[] { "URLsAndRoutes.AdditionalControllers" }
                );

            myRoute.DataTokens["UseNamespaceFallback"] = false;
            */

            /*
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new { controller = "^H.*" }
                , new[] { "URLsAndRoutes.Controllers" }
                );
            */

            /*
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new { controller = "^H.*",action="^Index$|^About$" }
                , new[] { "URLsAndRoutes.Controllers" }
                );
            */

            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("Content/{filename}.html");

            routes.MapRoute("DiskFile", "Content/StaticContent.html"
                , new { controller = "Customer", action = "List" }
                );

            routes.MapRoute("ChromeRoute", "{*catchall}"
                , new { controller = "Home", action = "Index" }
                , new { customConstraint = new UserAgentConstraints("Chrome") }
                , new[] { "UrlsAndRoutes.AdditionalControllers" }
                );

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new { controller = "^H.*", action = "Index|About" ,httpMethod=new HttpMethodConstraint("GET","POST")}
                , new[] { "URLsAndRoutes.Controllers" }
                );

        }
    }
}
