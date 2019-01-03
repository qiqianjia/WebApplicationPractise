using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider _authProvider;

        public AccountController(IAuthProvider P_AuthProvider)
        {
            _authProvider = P_AuthProvider;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel P_Model, string P_ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(P_Model.UserName, P_Model.Password))
                {
                    return Redirect(P_ReturnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}