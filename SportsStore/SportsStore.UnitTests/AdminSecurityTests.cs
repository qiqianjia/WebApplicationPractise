using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void CanLoginWithValidCredentials()
        {
            //Arrange
            //创建模仿认证提供器
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            //创建视图模型
            LoginViewModel model = new LoginViewModel { UserName = "admin", Password = "secret" };
            //创建控制器
            AccountController target = new AccountController(mock.Object);
            //Action
            //使用合法凭据进行认证
            ActionResult result = target.Login(model, "/MyURL");
            //Assert
            //断言
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void CannotLoginWithInvalidCredentials()
        {
            //Arrange
            //创建模仿认证提供器
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(true);

            //创建视图模型
            LoginViewModel model = new LoginViewModel { UserName = "badUser", Password = "badPass" };
            //创建控制器
            AccountController target = new AccountController(mock.Object);
            //Action
            //使用非法凭据进行认证
            ActionResult result = target.Login(model, "/MyURL");
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }


    }
}
