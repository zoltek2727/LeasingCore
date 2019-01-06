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
    public class ProductParamsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: ProductParams
        public async Task<IActionResult> Index()
        {
            var leasingContext = _context.ProductParams.Include(p => p.Param).Include(p => p.Product);
            return View(await leasingContext.ToListAsync());
        }

        // GET: ProductParams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productParam = await _context.ProductParams
                .Include(p => p.Param)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductParamId == id);
            if (productParam == null)
            {
                return NotFound();
            }

            return View(productParam);
        }

        // GET: ProductParams/Create
        public IActionResult Create()
        {
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode");
            return View();
        }

        // POST: ProductParams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductParamId,ProductId,ParamId")] ProductParam productParam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productParam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", productParam.ParamId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode", productParam.ProductId);
            return View(productParam);
        }

        // GET: ProductParams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productParam = await _context.ProductParams.FindAsync(id);
            if (productParam == null)
            {
                return NotFound();
            }
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", productParam.ParamId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode", productParam.ProductId);
            return View(productParam);
        }

        // POST: ProductParams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductParamId,ProductId,ParamId")] ProductParam productParam)
        {
            if (id != productParam.ProductParamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productParam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductParamExists(productParam.ProductParamId))
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
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", productParam.ParamId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode", productParam.ProductId);
            return View(productParam);
        }

        // GET: ProductParams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productParam = await _context.ProductParams
                .Include(p => p.Param)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductParamId == id);
            if (productParam == null)
            {
                return NotFound();
            }

            return View(productParam);
        }

        // POST: ProductParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productParam = await _context.ProductParams.FindAsync(id);
            _context.ProductParams.Remove(productParam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductParamExists(int id)
        {
            return _context.ProductParams.Any(e => e.ProductParamId == id);
        }
    }
}
