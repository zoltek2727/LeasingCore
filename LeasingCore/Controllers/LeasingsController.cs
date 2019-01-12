using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeasingCore.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection.Metadata;
using System.Net.Mail;
using System.Net;

namespace LeasingCore.Controllers
{
    public class LeasingsController : Controller
    {
        LeasingContext _context = new LeasingContext();

        private UserManager<ApplicationUser> _userManager;

        public LeasingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Leasings
        [Authorize]
        public async Task<IActionResult> Index(int page = 1, string sortExpression = "LeasingStart")
        {
            var id = _userManager.GetUserId(User);

            var qry = _context.Leasings            
                .Include(l => l.LeasingDetails)
                    .ThenInclude(p => p.Product)
                .Where(l => l.Id == id)
                .OrderByDescending(l => l.LeasingStart)
                .AsNoTracking().AsQueryable();

            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "CategoryName");

            return View(model);
        }

        // GET: Leasings/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leasing = await _context.Leasings
                .Include(l => l.ApplicationUser)
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
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Leasings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeasingId,LeasingStart,LeasingEnd,LeasingExtend,Id")] Leasing leasing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leasing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", leasing.Id);
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
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", leasing.Id);
            return View(leasing);
        }

        // POST: Leasings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeasingId,LeasingStart,LeasingEnd,LeasingExtend,Id")] Leasing leasing)
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
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", leasing.Id);
            return View(leasing);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Extend(int id)
        {
            var leasingDetail = await _context.LeasingDetails.FindAsync(id);
            leasingDetail.LeasingDetailEnd = leasingDetail.LeasingDetailEnd.AddYears(1);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int id)
        {
            var leasingDetail = await _context.LeasingDetails.FindAsync(id);
            leasingDetail.LeasingDetailEnd = DateTime.Now;

            var product = await _context.Products.FindAsync(leasingDetail.ProductId);
            product.ProductAvailability += leasingDetail.LeasingDetailAmount;

            leasingDetail.LeasingDetailAmount = 0;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeasingExists(int id)
        {
            return _context.Leasings.Any(e => e.LeasingId == id);
        }
    }
}
