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
    public class LeasingsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: Leasings
        public async Task<IActionResult> Index()
        {
            var leasingContext = _context.Leasings.Include(l => l.Product).Include(l => l.User);
            return View(await leasingContext.ToListAsync());
        }

        // GET: Leasings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leasing = await _context.Leasings
                .Include(l => l.Product)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LeasingId == id);
            if (leasing == null)
            {
                return NotFound();
            }

            return View(leasing);
        }

        // GET: Leasings/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Leasings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeasingId,LeasingStart,LeasingEnd,LeasingExtend,UserId,ProductId")] Leasing leasing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leasing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode", leasing.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", leasing.UserId);
            return View(leasing);
        }

        // GET: Leasings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leasing = await _context.Leasings.FindAsync(id);
            if (leasing == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode", leasing.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", leasing.UserId);
            return View(leasing);
        }

        // POST: Leasings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeasingId,LeasingStart,LeasingEnd,LeasingExtend,UserId,ProductId")] Leasing leasing)
        {
            if (id != leasing.LeasingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leasing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeasingExists(leasing.LeasingId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductCode", leasing.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", leasing.UserId);
            return View(leasing);
        }

        // GET: Leasings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leasing = await _context.Leasings
                .Include(l => l.Product)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LeasingId == id);
            if (leasing == null)
            {
                return NotFound();
            }

            return View(leasing);
        }

        // POST: Leasings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leasing = await _context.Leasings.FindAsync(id);
            _context.Leasings.Remove(leasing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeasingExists(int id)
        {
            return _context.Leasings.Any(e => e.LeasingId == id);
        }
    }
}
