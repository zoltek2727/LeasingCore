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
    public class ParamAssortmentsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: ParamAssortments
        public async Task<IActionResult> Index()
        {
            var leasingContext = _context.ParamAssortments.Include(p => p.Param);
            return View(await leasingContext.ToListAsync());
        }

        // GET: ParamAssortments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paramAssortment = await _context.ParamAssortments
                .Include(p => p.Param)
                .FirstOrDefaultAsync(m => m.ParamAssortmentId == id);
            if (paramAssortment == null)
            {
                return NotFound();
            }

            return View(paramAssortment);
        }

        // GET: ParamAssortments/Create
        public IActionResult Create()
        {
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName");
            return View();
        }

        // POST: ParamAssortments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParamAssortmentId,ParamAssortmentName,ParamAssortmentBrand,ParamId")] ParamAssortment paramAssortment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paramAssortment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", paramAssortment.ParamId);
            return View(paramAssortment);
        }

        // GET: ParamAssortments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paramAssortment = await _context.ParamAssortments.FindAsync(id);
            if (paramAssortment == null)
            {
                return NotFound();
            }
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", paramAssortment.ParamId);
            return View(paramAssortment);
        }

        // POST: ParamAssortments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParamAssortmentId,ParamAssortmentName,ParamAssortmentBrand,ParamId")] ParamAssortment paramAssortment)
        {
            if (id != paramAssortment.ParamAssortmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paramAssortment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParamAssortmentExists(paramAssortment.ParamAssortmentId))
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
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", paramAssortment.ParamId);
            return View(paramAssortment);
        }

        // GET: ParamAssortments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paramAssortment = await _context.ParamAssortments
                .Include(p => p.Param)
                .FirstOrDefaultAsync(m => m.ParamAssortmentId == id);
            if (paramAssortment == null)
            {
                return NotFound();
            }

            return View(paramAssortment);
        }

        // POST: ParamAssortments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paramAssortment = await _context.ParamAssortments.FindAsync(id);
            _context.ParamAssortments.Remove(paramAssortment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParamAssortmentExists(int id)
        {
            return _context.ParamAssortments.Any(e => e.ParamAssortmentId == id);
        }
    }
}
