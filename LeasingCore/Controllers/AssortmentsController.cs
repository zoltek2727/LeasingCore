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
    [Authorize]
    public class AssortmentsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: Assortments
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "AssortmentName")
        {
            //var leasingContext = _context.Assortments.Include(a => a.Param);
            //return View(await leasingContext.ToListAsync());

            var qry = _context.Assortments.Include(p=>p.Param).OrderBy(a => a.AssortmentName).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(a => a.AssortmentName.Contains(filter));
            }

            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "AssortmentName");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _context.Assortments.Include(p=>p.Param).Where(a => a.AssortmentName.Contains(term)).OrderBy(a => a.AssortmentName).Select(a => a.AssortmentName).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Assortments/Create
        public IActionResult Create()
        {
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName");
            return View();
        }

        // POST: Assortments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssortmentId,AssortmentName,AssortmentBrand,AssortmentPrice,ParamId")] Assortment assortment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assortment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", assortment.ParamId);
            return View(assortment);
        }

        // GET: Assortments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assortment = await _context.Assortments.FindAsync(id);
            if (assortment == null)
            {
                return NotFound();
            }
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", assortment.ParamId);
            return View(assortment);
        }

        // POST: Assortments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssortmentId,AssortmentName,AssortmentBrand,AssortmentPrice,ParamId")] Assortment assortment)
        {
            if (id != assortment.AssortmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assortment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssortmentExists(assortment.AssortmentId))
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
            ViewData["ParamId"] = new SelectList(_context.Params, "ParamId", "ParamName", assortment.ParamId);
            return View(assortment);
        }

        // GET: Assortments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var assortment = await _context.Assortments.FindAsync(id);
            _context.Assortments.Remove(assortment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssortmentExists(int id)
        {
            return _context.Assortments.Any(e => e.AssortmentId == id);
        }
    }
}
