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
    public class ParamsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: Params
        public async Task<IActionResult> Index()
        {
            return View(await _context.Params.ToListAsync());
        }

        // GET: Params/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @param = await _context.Params
                .FirstOrDefaultAsync(m => m.ParamId == id);
            if (@param == null)
            {
                return NotFound();
            }

            return View(@param);
        }

        // GET: Params/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Params/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParamId,ParamName")] Param @param)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@param);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@param);
        }

        // GET: Params/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @param = await _context.Params.FindAsync(id);
            if (@param == null)
            {
                return NotFound();
            }
            return View(@param);
        }

        // POST: Params/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParamId,ParamName")] Param @param)
        {
            if (id != @param.ParamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@param);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParamExists(@param.ParamId))
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
            return View(@param);
        }

        // GET: Params/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @param = await _context.Params
                .FirstOrDefaultAsync(m => m.ParamId == id);
            if (@param == null)
            {
                return NotFound();
            }

            return View(@param);
        }

        // POST: Params/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @param = await _context.Params.FindAsync(id);
            _context.Params.Remove(@param);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParamExists(int id)
        {
            return _context.Params.Any(e => e.ParamId == id);
        }
    }
}
