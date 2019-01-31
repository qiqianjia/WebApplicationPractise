using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllersAndActions.Controllers;
using System.Web.Mvc;

namespace ControllersAndActions.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void ViewSelectionTest()
        {
            //Arrange
            //创建控制器
            ExampleController target = new ExampleController();
            //Action
            //调用动作方法
            ViewResult result = target.Index();
            //Assert
            //检查结果
            Assert.AreEqual("Homepage", result.ViewName);
        }
    }
}
