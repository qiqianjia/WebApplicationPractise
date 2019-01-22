﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}"
                ,new { controller="Home",action="Index",id=UrlParameter.Optional}); 

        }
    }
}