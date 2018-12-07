using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeasingCore.Models;

namespace LeasingCore.Controllers
{
    public class LeasingController : Controller
    {
        LeasingContext dbContext = new LeasingContext();

        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Products.Include(p => p.Category).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await dbContext.Products
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
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductAvailability,ProductCode,CompanyId")] Product products)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(products);
                await dbContext.SaveChangesAsync();
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

            var products = await dbContext.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductAvailability,ProductCode,CompanyId")] Product products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(products);
                    await dbContext.SaveChangesAsync();
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

            var products = await dbContext.Products
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
            var products = await dbContext.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            dbContext.Products.Remove(products);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExist(int id)
        {
            return dbContext.Products.Any(p => p.ProductId == id);
        }
    }
}