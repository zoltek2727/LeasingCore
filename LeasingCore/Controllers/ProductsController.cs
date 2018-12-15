using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeasingCore.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace LeasingCore.Controllers
{
    public class ProductsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "ProductName")
        {
            //var products = from p in dbContext.Products
            //               select p;

            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    products = products.Where(p=>p.ProductName.Contains(SearchString));
            //}

            //return View(await products.Include(p=>p.Category).ToListAsync());

            var qry = _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.ProductName.Contains(filter));
            }

            var model = await PagingList.CreateAsync(qry, 3, page, sortExpression, "ProductName");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);

            //return View(await dbContext.Products.Include(p => p.Category).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.ProductParams.Include(p => p.Param).Include(p => p.Product).Include(c=>c.Product.Category)
                .SingleOrDefaultAsync(p => p.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductAvailability,ProductCode,CategoryId")] Product products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductAvailability,ProductCode,CategoryId")] Product products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExist(products.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .SingleOrDefaultAsync(p => p.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExist(int id)
        {
            return _context.Products.Any(p => p.ProductId == id);
        }
    }
}