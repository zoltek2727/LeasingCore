using LeasingCore.Models;
using System;

namespace LeasingCore.Shopping
{
    interface IShoppingClass
    {
        void Shopping(Product product, DateTime time, int quantity);
    }
}
