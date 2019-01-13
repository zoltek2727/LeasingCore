using LeasingCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeasingCore.Shopping
{
    public class ShoppingClass : IShoppingClass
    {
        public void Shopping(Product product, DateTime time, int quantity)
        {
            if(product.ProductId<0)
            {
                throw new ArgumentOutOfRangeException("ProductId cannot be less than 0");
            }

            if(time.Date.Date>DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("Purchase time cannot be greater than now");
            }

            if(quantity<1)
            {
                throw new ArgumentOutOfRangeException("You cannot purchase nothing");
            }

            if(product.ProductAvailability<1)
            {
                throw new ArgumentOutOfRangeException("You cannot purchase becuase product is unavailable");
            }

            if(product.ProductPrice<0)
            {
                throw new ArgumentOutOfRangeException("Product price cannot be smaller than one");
            }
        }
    }
}
