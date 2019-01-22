using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Reflection;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string P_TargetUrl = null,string P_HttpMethod="GET")
        {
            //创建模仿请求
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(P_TargetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(P_HttpMethod);
            //创建模仿响应
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            //创建使用上述请求和响应的模仿上下文
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //返回模仿的上下文
            return mockContext.Object;
        }

        private void TestRouteMatch(string P_Url, string P_Controller, string P_Action,
            object P_RouteProperties = null, string P_HttpMethod = "GET")
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Action
            //处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(P_Url, P_HttpMethod));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result,P_Controller,P_Action,P_RouteProperties));
        }

        private bool TestIncomingRouteResult(RouteData P_RouteResult, string P_Controller, 
            string P_Action, object P_PropertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };
            bool result = valCompare(P_RouteResult.Values["controller"], P_Controller)
                && valCompare(P_RouteResult.Values["action"], P_Action);

            if (P_PropertySet != null)
            {
                PropertyInfo[] propInfoArray = P_PropertySet.GetType().GetProperties();
                foreach (PropertyInfo propInfo in propInfoArray)
                {
                    if (!(P_RouteResult.Values.ContainsKey(propInfo.Name)
                        &&valCompare(P_RouteResult.Values[propInfo.Name],propInfo.GetValue(P_PropertySet,null)) 
                        ))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private void TestRouteFail(string P_Url)
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Action
            //处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(P_Url));
            //Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            //对用户希望接收的URL进行检查
            TestRouteMatch("~/Admin/Index", "Admin", "Index");
            //检查通过片段获取的值
            TestRouteMatch("~/One/Two", "One", "Two");

            //确保太多或太少的片段数不会匹配
            TestRouteFail("~/Admin/Index/Segment");
            TestRouteFail("~/Admin");

            /*
            TestRouteMatch("~/", "Home", "Index",new { id="DefaultId"});
            TestRouteMatch("~/Customer", "Customer", "Index",new { id="DefaultId"});
            TestRouteMatch("~/Shop/Index", "Home", "Index", new { id = "DefaultId" });
            TestRouteMatch("~/Customer/List", "Customer", "List", new { id = "DefaultId" });
            TestRouteFail("~/Customer/List/All");
             */

            /*
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            TestRouteFail("~/Customer/List/All/Delete");
            */

            /*
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List", 
                new { id = "All" ,catchall="Delete"});
            TestRouteMatch("~/Customer/List/All/Delete/Perm", "Customer", "List",
                new { id = "All", catchall = "Delete/Perm" });
                */

            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Home", "Home", "Index");
            TestRouteMatch("~/Home/Index", "Home", "Index");
            TestRouteMatch("~/Home/About", "Home", "About");
            TestRouteMatch("~/Home/About/MyID", "Home", "About", new { id = "MyID" });
            TestRouteMatch("~/Home/About/MyID/More/Segments", "Home", "About",
                new { id = "MyID", catchall = "More/Segments" });

            TestRouteFail("~/Home/OtherAction");
            TestRouteFail("~/Account/Index");
            TestRouteFail("~/Account/About");

        }
    }
}
