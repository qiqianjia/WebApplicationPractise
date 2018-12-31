using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools2.Models;
using Moq;
using System.Linq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Product[] _productArray = {
            new Product{ Name="Kayak",Category="Watersports",Price=275M},
            new Product{ Name="Lifejacket",Category="Watersports",Price=48.95M},
            new Product{ Name="Soccer ball",Category="Soccer",Price=19.50M},
            new Product{ Name="Corner flag",Category="Soccer",Price=34.95M}
        };

        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void DiscountAbove100()
        {
            //准备
            IDiscountHelper target = getTestObject();
            decimal total = 200;
            //动作
            var discountedTotal = target.ApplyDiscount(total);
            //断言
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }

        [TestMethod]
        public void DiscountBetween10And100()
        {
            //Arrange
            IDiscountHelper target = getTestObject();
            //Action
            decimal TenDollarDiscount = target.ApplyDiscount(10);
            decimal HundredDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);
            //Assert
            Assert.AreEqual(5, TenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        public void DiscountLessThen10()
        {
            //Arrange
            IDiscountHelper target = getTestObject();
            //Action
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);
            //Assert
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DiscountNegativeTotal()
        {
            //Arrange
            IDiscountHelper target = getTestObject();
            //Action
            target.ApplyDiscount(-1);
        }

        [TestMethod]
        public void SumProductsCorrectly()
        {
            //Arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);
            //Action
            var result = target.ValueProducts(_productArray);
            //Assert
            Assert.AreEqual(_productArray.Sum(e => e.Price), result);
        }

        private Product[] CreateProduct(decimal P_Value)
        {
            return new[] { new Product { Price = P_Value} };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void PassThroughVariableDiscounts()
        {
            //Arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
                .Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                .Returns<decimal>(total => total * 0.9M);
            var target = new LinqValueCalculator(mock.Object);

            //Action
            decimal FiveDollarDiscount = target.ValueProducts(CreateProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(CreateProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(CreateProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(CreateProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(CreateProduct(500));

            //Assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 fail");
            Assert.AreEqual(10, TenDollarDiscount, "$10 fail");
            Assert.AreEqual(50, FiftyDollarDiscount, "$50 fail");
            Assert.AreEqual(100, HundredDollarDiscount, "$100 fail");
            Assert.AreEqual(500, FiveHundredDollarDiscount, "$500 fail");
        }

    }
}
