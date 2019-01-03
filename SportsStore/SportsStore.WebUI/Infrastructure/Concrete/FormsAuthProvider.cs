using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string P_UserName, string P_Password)
        {
            bool result = FormsAuthentication.Authenticate(P_UserName, P_Password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(P_UserName, false);
            }
            return result;
        }
    }
}