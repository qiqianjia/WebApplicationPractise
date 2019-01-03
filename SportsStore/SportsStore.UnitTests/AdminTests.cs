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
    public class AdminTests
    {
        [TestMethod]
        public void IndexContainsAllProducts()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1"},
                new Product{ ProductID=2,Name="P2"},
                new Product{ ProductID=3,Name="P3"},
            }.AsQueryable());
            //创建控制器
            AdminController target = new AdminController(mock.Object);
            //Action
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();
            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void CanEditProduct()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1" },
                new Product{ ProductID=2,Name="P2" },
                new Product{ ProductID=3,Name="P3" },
            }.AsQueryable());
            //创建控制器
            AdminController target = new AdminController(mock.Object);
            //Action
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;
            //Asssert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void CannotEditNonexistentProduct()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1" },
                new Product{ ProductID=2,Name="P2" },
                new Product{ ProductID=3,Name="P3" },
            }.AsQueryable());
            //创建控制器
            AdminController target = new AdminController(mock.Object);
            //Action
            Product result = (Product)target.Edit(4).ViewData.Model;
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CanSaveValidChanges()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //创建控制器
            AdminController target = new AdminController(mock.Object);
            //创建一个产品
            Product product = new Product { Name = "Test" };
            //Action
            //试着保存这个产品
            ActionResult result = target.Edit(product);
            //Assert
            //检查，调用了存储库
            mock.Verify(m => m.SaveProduct(product));
            //检查方法的结果类型
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CannotSaveInvalidChanges()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //创建控制器
            AdminController target = new AdminController(mock.Object);
            //创建一个产品
            Product product = new Product { Name = "Test" };
            //把一个错误添加到模型状态
            target.ModelState.AddModelError("error", "error");
            //Action
            //试图保存产品
            ActionResult result = target.Edit(product);
            //Assert
            //确认存储库未被调用
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            //确认方法的结果类型
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CanDeleteValidProducts()
        {
            //Arrange
            //创建一个产品
            Product productTemp = new Product { ProductID = 2, Name = "Test" };
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1"},
                productTemp,
                new Product{ ProductID=3,Name="P3"}
            }.AsQueryable());
            //创建控制器
            AdminController target = new AdminController(mock.Object);
            //Action
            //删除产品
            target.Delete(productTemp.ProductID);
            //Assert
            //确保存储库的删除方法是针对正确的产品被调用的
            mock.Verify(m => m.DeleteProduct(productTemp.ProductID));
        }

    }
}
