using LeasingCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Text;

namespace LeasingCore.Helpers
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(int id)
        {
            decimal total = 0;

            LeasingContext _context = new LeasingContext();

            var products = _context.Products.ToList();

            var leasing = _context.Leasings.Where(ld => ld.LeasingId == id)
                .Include(l => l.LeasingDetails)
                    .ThenInclude(l => l.Product)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Order no. {0} </h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Product Name</th>
                                        <th>Product Price</th>
                                        <th>Product Code</th>
                                        <th>Amount</th>
                                        <th>Total</th>
                                    </tr>",id);
            foreach (var ld in leasing)
            {
                foreach (var l in ld.LeasingDetails)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                  </tr>", l.Product.ProductName, l.Product.ProductPrice, l.Product.ProductCode, l.LeasingDetailAmount, l.LeasingDetailAmount * l.Product.ProductPrice);
                    total += l.LeasingDetailAmount * l.Product.ProductPrice;
                }
            }

            sb.AppendFormat(@"<tr><td colspan='4'></td><td></td>{0}</tr>
                                </table>
                            </body>
                        </html>",total);

            return sb.ToString();
        }
    }
}

