using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeasingCore.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace LeasingCore.Controllers
{
    public class ParamsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: Params
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "ParamName")
        {
            var qry = _context.Params.OrderBy(p => p.ParamName).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.ParamName.Contains(filter));
            }

            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "CategoryName");

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
                var names = _context.Params.Where(p => p.ParamName.Contains(term)).OrderBy(p => p.ParamName).Select(p => p.ParamName).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
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
        public async Task<IActionResult> Delete(int id)
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
