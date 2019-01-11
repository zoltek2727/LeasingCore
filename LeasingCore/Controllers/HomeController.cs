using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeasingCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LeasingCore.Controllers
{
    public class HomeController : Controller
    {
        LeasingContext _context = new LeasingContext();

        public async Task<IActionResult> Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p=>p.PhotoProducts)
                    .ThenInclude(p=>p.Photo)
                .Include(p => p.ProductAssortments)
                    .ThenInclude(a => a.Assortment)
                        .ThenInclude(p=>p.Param)
                .OrderByDescending(p => p.ProductAdded).Take(3);
            return View(await products.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
