using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class CustomAuthAttribute:AuthorizeAttribute
    {
        private bool localAllowed;

        public CustomAuthAttribute(bool P_AllowedParam)
        {
            localAllowed = P_AllowedParam;
        }

        protected override bool AuthorizeCore(HttpContextBase P_HttpContext)
        {
            if (P_HttpContext.Request.IsLocal)
            {
                return localAllowed;
            }
            else
            {
                return true;
            }
        }

    }
}