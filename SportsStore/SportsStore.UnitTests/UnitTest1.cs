using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ ProductID=1,Name="P1"},
                new Product{ ProductID=2,Name="P2"},
                new Product{ ProductID=3,Name="P3"},
                new Product{ ProductID=4,Name="P4"},
                new Product{ ProductID=5,Name="P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Action
            /*
            IEnumerable<Product> result =
                (IEnumerable<Product>)controller.List(2).Model;
                */
            //ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;
            //Assert
            //Product[] productArray = result.ToArray();
            Product[] productArray = result.Products.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "P4");
            Assert.AreEqual(productArray[1].Name, "P5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            //Arrange
            //定义一个HTML辅助器，为了运用扩展方法，需要这样
            HtmlHelper helper = null;
            //创建PagingInfo数据
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            //用lambda表达式建立委托
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            //Action
            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);
            //Assert
            Assert.AreEqual(result.ToString(),
                @"<a href=""Page1"">1</a>"
+ @"<a class=""selected"" href=""Page2"">2</a>"
+ @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1"},
                new Product{ ProductID=2,Name="P2"},
                new Product{ ProductID=3,Name="P3"},
                new Product{ ProductID=4,Name="P4"},
                new Product{ ProductID=5,Name="P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Action
            //ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterProducts()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[]{
                new Product{ ProductID=1,Name="P1",Category="Cat1"},
                new Product{ ProductID=2,Name="P2",Category="Cat2"},
                new Product{ ProductID=3,Name="P3",Category="Cat1"},
                new Product{ ProductID=4,Name="P4",Category="Cat2"},
                new Product{ ProductID=5,Name="P5",Category="Cat3"}
            }.AsQueryable());

            //创建控制器，并使页面大小为3个物品
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");

        }

        [TestMethod]
        public void CanCreateCategories()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1",Category="Apples"},
                new Product{ ProductID=2,Name="P2",Category="Apples"},
                new Product{ ProductID=3,Name="P3",Category="Plums"},
                new Product{ ProductID=4,Name="P4",Category="Oranges"},
                
            }.AsQueryable());
            //创建控制器
            NavController target = new NavController(mock.Object);
            //Action
            //获取分类集合
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");

        }

        [TestMethod]
        public void IndicatesSelectedCategory()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1",Category="Apples"},
                new Product{ ProductID=2,Name="P2",Category="Oranges"},
            }.AsQueryable());

            //创建控制器
            NavController target = new NavController(mock.Object);

            //定义已选分类
            string categoryToSelect = "Apples";

            //Action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);

        }

        [TestMethod]
        public void CategorySpecificProductCount()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductID=1,Name="P1",Category="Cat1"},
                new Product{ ProductID=2,Name="P2",Category="Cat2"},
                new Product{ ProductID=3,Name="P3",Category="Cat1"},
                new Product{ ProductID=4,Name="P4",Category="Cat2"},
                new Product{ ProductID=5,Name="P5",Category="Cat3"},
            }.AsQueryable());

            //创建控制器，并使页面能够容纳3个物品
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            //Action
            int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;
            
            //Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);

        }

        [TestMethod]
        public void CanAddNewLines()
        {
            //Arrange
            //创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            //创建一个新的购物车
            Cart target = new Cart();
            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();
            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);

        }

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            //Arrange
            //创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            //创建一个新的购物车
            Cart target = new Cart();
            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p1, 5);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            //创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3= new Product { ProductID = 3, Name = "P3" };
            //创建一个新的购物车
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Action
            target.RemoveLine(p2);

            //Assert
            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);

        }

        [TestMethod]
        public void CalculateCartTotal()
        {
            //Arrange
            //创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" ,Price=100M};
            Product p2 = new Product { ProductID = 2, Name = "P2" ,Price=50M};
            //创建一个新的购物车
            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void CanClearContents()
        {
            //Arrange
            //创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            //创建一个新的购物车
            Cart target = new Cart();

            //Action
            //添加一些物品
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            //重置购物车
            target.Clear();

            //Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void CanAddToCart()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]{ new Product { ProductID=1,Name="P1",Category="Apples"}
                }.AsQueryable()
                );
            //创建Cart
            Cart cart = new Cart();
            //创建控制器
            CartController target = new CartController(mock.Object);

            //Action
            //对cart添加一个产品
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void AddingProductToCartGoesToCartScreen()
        {
            //Arrange
            //创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]{ new Product { ProductID=1,Name="P1",Category="Apples"}
                }.AsQueryable()
                );
            //创建Cart
            Cart cart = new Cart();
            //创建控制器
            CartController target = new CartController(mock.Object);

            //Action
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["Action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void CanViewCartContents()
        {
            //Arrange
            //创建Cart
            Cart cart = new Cart();
            //创建控制器
            CartController target = new CartController(null);
            //Action
            //调用Index动作方法
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            //Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");

        }

    }
}
