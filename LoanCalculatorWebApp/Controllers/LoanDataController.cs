﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanCalculatorWebApp.Models;
using LoanCalculatorWebApp.Data;

namespace LoanCalculatorWebApp.Controllers
{
    public class LoanDataController : Controller
    {

        IRepository<LoanData> LoanDataRepo;

        public LoanDataController(IRepository<LoanData> repository)
        {
            LoanDataRepo = repository;
        }

        public async Task<IActionResult> Index()
        {
            var loanDataItems = await LoanDataRepo.GetAll().ConfigureAwait(false);
            return View(loanDataItems);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanData = await LoanDataRepo.FindById(id).ConfigureAwait(false);

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
                await LoanDataRepo.Add(loanData).ConfigureAwait(false);
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

            var loanData = await LoanDataRepo.FindById(id).ConfigureAwait(false);

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
                    await LoanDataRepo.Update(loanData).ConfigureAwait(false);
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

            var loanData = await LoanDataRepo.FindById(id).ConfigureAwait(false);
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
            var loanData = await LoanDataRepo.FindById(id).ConfigureAwait(false);
            await LoanDataRepo.Delete(loanData).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Snowball() {

            var sortedLoanData = from row in LoanDataRepo.List orderby row.LoanAmount select row;

            return View(sortedLoanData);
        }

        public IActionResult Avalanche()
        {

            var sortedLoanData = from row in LoanDataRepo.List orderby row.LoanAmount descending select row;

            return View(sortedLoanData);
        }

        private bool LoanDataExists(Guid id)
        {
            return LoanDataRepo.List.Any(e => e.Id == id);
        }

        private static void CalculateLoanData(LoanData loanData) {
            loanData.SetTotalCost();
            loanData.SetMonthlyBill();
        }
    }
}
