using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EssentialTools2.Infrastructure;
using System.Web.Http;

namespace EssentialTools2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.Register(GlobalFilters.Filters);
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
