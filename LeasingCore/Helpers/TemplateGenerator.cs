using LeasingCore.Models;
using System.Linq;
using System.Text;

namespace LeasingCore.Helpers
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString()
        {
            LeasingContext _context = new LeasingContext();

            var products = _context.Products.ToList();

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>ProductId</th>
                                        <th>ProductName</th>
                                        <th>ProductDescription</th>
                                        <th>ProductPrice</th>
                                        <th>ProductAvailability</th>
                                        <th>ProductCode</th>
                                    </tr>");

            foreach (var emp in products)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                  </tr>", emp.ProductId, emp.ProductName, emp.ProductDescription, emp.ProductPrice, emp.ProductAvailability, emp.ProductCode);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}

