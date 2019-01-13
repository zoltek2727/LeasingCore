using LeasingCore.Shopping;
using System;
using Xunit;

namespace LeasingCore.UnitTests
{
    public class ProductShoppingUnitTests
    {
        [Fact]
        public void ValidProductId()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product(), DateTime.Now, 1));
        }

        [Fact]
        public void InValidProductId()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product() {ProductId=-1 }, DateTime.Now, 1));
        }

        [Fact]
        public void VaildTime()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product(), DateTime.Now, 1));
        }

        [Fact]
        public void InVaildTime()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product(), DateTime.Now.AddHours(1), 1));
        }

        [Fact]
        public void ValidQuantity()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product(), DateTime.Now, 1));
        }

        [Fact]
        public void InValidQuantity()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product(), DateTime.Now, -1));
        }

        [Fact]
        public void ValidProductAvailabilty()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product() { ProductAvailability = 1 }, DateTime.Now, 1));
        }

        [Fact]
        public void InValidProductAvailabilty()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product() { ProductAvailability = 0 }, DateTime.Now, 1));
        }

        [Fact]
        public void ValidProductPrice()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product() { ProductPrice = 5 }, DateTime.Now, 1));
        }

        [Fact]
        public void InValidProductPrice()
        {
            var shoppingclass = new ShoppingClass();

            Assert.Throws<ArgumentOutOfRangeException>(() => shoppingclass.Shopping(new Models.Product() { ProductPrice = 0 }, DateTime.Now, 1));
        }
    }
}
