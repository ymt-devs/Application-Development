using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCoursework.Data;
using GroupCoursework.Models;
using Microsoft.AspNetCore.Authorization;

namespace GroupCoursework.Controllers
{
    [Authorize]
    public class PurchaseDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseDetails.Include(p => p.Product).Include(p => p.Purchase);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .Include(p => p.Product)
                .Include(p => p.Purchase)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ProductName");
            ViewData["PurchaseID"] = new SelectList(_context.Purchase, "ID", "ID");
            return View();
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PurchaseID,ProductID,Quantity,Price")] PurchaseDetails purchaseDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ProductName", purchaseDetails.ProductID);
            ViewData["PurchaseID"] = new SelectList(_context.Purchase, "ID", "ID", purchaseDetails.PurchaseID);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails.FindAsync(id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ProductName", purchaseDetails.ProductID);
            ViewData["PurchaseID"] = new SelectList(_context.Purchase, "ID", "ID", purchaseDetails.PurchaseID);
            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PurchaseID,ProductID,Quantity,Price")] PurchaseDetails purchaseDetails)
        {
            if (id != purchaseDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailsExists(purchaseDetails.ID))
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
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ProductName", purchaseDetails.ProductID);
            ViewData["PurchaseID"] = new SelectList(_context.Purchase, "ID", "ID", purchaseDetails.PurchaseID);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .Include(p => p.Product)
                .Include(p => p.Purchase)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseDetails = await _context.PurchaseDetails.FindAsync(id);
            _context.PurchaseDetails.Remove(purchaseDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseDetailsExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.ID == id);
        }
    }
}
