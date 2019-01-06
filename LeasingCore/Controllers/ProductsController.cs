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

using Microsoft.AspNetCore.Authorization;


namespace LeasingCore.Controllers
{
    public class ProductsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        public async Task<IActionResult> Index(string filter, string categoryFilter, int page = 1, string sortExpression = "ProductName")
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
                if (string.IsNullOrWhiteSpace(categoryFilter))
                {
                    qry = qry.Where(p => p.ProductName.Contains(filter));
                }
                else
                {
                    qry = qry.Where(p => p.ProductName.Contains(filter)).Where(p => p.Category.CategoryId == Convert.ToInt32(categoryFilter));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(categoryFilter))
                {
                    qry = qry.Where(p => p.Category.CategoryId == Convert.ToInt32(categoryFilter));
                }     
            }

            var model = await PagingList.CreateAsync(qry, 3, page, sortExpression, "ProductName");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "categoryFilter", categoryFilter },
                { "sortExpression", sortExpression }
            };

            ViewBag.ListOfCategories = _context.Categories.OrderBy(c=>c.CategoryName).ToList();

            return View(model);

            //return View(await dbContext.Products.Include(p => p.Category).ToListAsync());
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _context.Products.Where(p => p.ProductName.Contains(term)).Select(p => p.ProductName).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(c=>c.Category)
                .Include(p => p.ProductParams)
                    .ThenInclude(p => p.Param)
                        .ThenInclude(p => p.ParamAssortments)
                .Include(p => p.PhotoProducts)
                    .ThenInclude(p => p.Photo)
                .SingleOrDefaultAsync(p => p.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", products.CategoryId);
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