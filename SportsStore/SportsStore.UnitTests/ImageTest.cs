using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void CanRetrieveImageData()
        {
            //Arrange
            //创建一个带有图像数据的Product
            Product product = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1"},
                product,
                new Product{ ProductID=3,Name="P3"}
            }.AsQueryable());
            //创建控制器
            ProductController target = new ProductController(mock.Object);

            //Action
            //调用Get Image动作方法
            ActionResult result = target.GetImage(2);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(product.ImageMimeType, ((FileResult)result).ContentType);

        }

        [TestMethod]
        public void CannotRetrieveImageDataForInvalidID()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1"},
                new Product{ ProductID=2,Name="P2"}
            }.AsQueryable());
            //创建控制器
            ProductController target = new ProductController(mock.Object);
            //Action
            ActionResult result = target.GetImage(100);
            //Assert
            Assert.IsNull(result);

        }

    }
}
