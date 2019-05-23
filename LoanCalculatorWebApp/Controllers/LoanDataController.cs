using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoanCalculatorWebApp.Models;

namespace LoanCalculatorWebApp.Controllers
{
    public class LoanDataController : Controller
    {
        private readonly LoanDataContext _context;

        public LoanDataController(LoanDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.LoanData.ToListAsync().ConfigureAwait(false));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanData = await _context.LoanData
                .FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
            if (loanData == null)
            {
                return NotFound();
            }

            return View(loanData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanData loanData)
        {
            if (ModelState.IsValid)
            {
                loanData.Id = Guid.NewGuid();
                CalculateLoanData(loanData);
                _context.Add(loanData);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(loanData);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanData = await _context.LoanData.FindAsync(id).ConfigureAwait(false);
            if (loanData == null)
            {
                return NotFound();
            }
            return View(loanData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LoanData loanData)
        {
            if (id != loanData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CalculateLoanData(loanData);
                    _context.Update(loanData);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanDataExists(loanData.Id))
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
            return View(loanData);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanData = await _context.LoanData
                .FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
            if (loanData == null)
            {
                return NotFound();
            }

            return View(loanData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var loanData = await _context.LoanData.FindAsync(id).ConfigureAwait(false);
            _context.LoanData.Remove(loanData);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Snowball() {

            var sortedLoanData = from row in _context.LoanData.AsEnumerable() orderby row.LoanAmount select row;

            return View(sortedLoanData);
        }

        public IActionResult Avalanche()
        {

            var sortedLoanData = from row in _context.LoanData.AsEnumerable() orderby row.LoanAmount descending select row;

            return View(sortedLoanData);
        }

        private bool LoanDataExists(Guid id)
        {
            return _context.LoanData.Any(e => e.Id == id);
        }

        private static void CalculateLoanData(LoanData loanData) {
            loanData.SetTotalCost();
            loanData.SetMonthlyBill();
        }
    }
}
