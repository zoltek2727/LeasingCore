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
    public class ReportsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        // GET: Reports
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "-ReportAdded")
        {
            //var leasingContext = _context.Reports
            //    .Include(r => r.LeasingDetail)
            //    .Include(r => r.LeasingDetail.Leasing)
            //    .Include(u => u.LeasingDetail.Leasing.User)
            //    .Include(p => p.LeasingDetail.Product)
            //    .Include(r => r.Status)
            //    .OrderBy(s => s.Status.StatusId)
            //    .OrderByDescending(r => r.ReportAdded);
            //return View(await leasingContext.ToListAsync());

            var qry = _context.Reports
                .Include(r => r.LeasingDetail)
                .Include(r => r.LeasingDetail.Leasing)
                .Include(u => u.LeasingDetail.Leasing.User)
                .Include(p => p.LeasingDetail.Product)
                .Include(r => r.Status)
                .OrderBy(s => s.Status.StatusId)
                .OrderByDescending(r=>r.ReportAdded).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(r => r.LeasingDetail.Product.ProductName.Contains(filter));
            }

            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "-ReportAdded");

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
                var names = _context.Reports.Where(r => r.LeasingDetail.Product.ProductName.Contains(term)).OrderBy(r => r.LeasingDetail.Product.ProductName).Select(r => r.LeasingDetail.Product.ProductName).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.LeasingDetail)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create(int id)
        {
            ViewData["LeasingDetailId"] = new SelectList(_context.LeasingDetails.Where(ld=>ld.LeasingDetailId==id), "LeasingDetailId", "LeasingDetailId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,ReportDescription,ReportAdded,LeasingDetailId,StatusId")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeasingDetailId"] = new SelectList(_context.LeasingDetails, "LeasingDetailId", "LeasingDetailId", report.LeasingDetailId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName", report.StatusId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["LeasingDetailId"] = new SelectList(_context.LeasingDetails, "LeasingDetailId", "LeasingDetailId", report.LeasingDetailId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName", report.StatusId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,ReportDescription,LeasingDetailId,StatusId")] Report report)
        {
            if (id != report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ReportId))
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
            ViewData["LeasingDetailId"] = new SelectList(_context.LeasingDetails, "LeasingDetailId", "LeasingDetailId", report.LeasingDetailId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName", report.StatusId);
            return View(report);
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}
