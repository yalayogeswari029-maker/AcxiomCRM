using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers
{
    public class OpportunitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpportunitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST OPPORTUNITIES
        public async Task<IActionResult> Index()
        {
            return View(await _context.Opportunities.ToListAsync());
        }

        // SHOW CREATE PAGE
        public IActionResult Create()
        {
            return View();
        }

        // SAVE NEW OPPORTUNITY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Opportunity opportunity)
        {
            if (opportunity.ExpectedCloseDate.Date < DateTime.Today)
            {
                ModelState.AddModelError(
                    "ExpectedCloseDate",
                    "Expected close date cannot be in the past.");
            }

            if (ModelState.IsValid)
            {
                _context.Opportunities.Add(opportunity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(opportunity);
        }

        // SHOW EDIT PAGE
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opportunity = await _context.Opportunities.FindAsync(id);

            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        // SAVE EDITED OPPORTUNITY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Opportunity opportunity)
        {
            if (id != opportunity.Id)
            {
                return NotFound();
            }

            if (opportunity.ExpectedCloseDate.Date < DateTime.Today)
            {
                ModelState.AddModelError(
                    "ExpectedCloseDate",
                    "Expected close date cannot be in the past.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(opportunity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(opportunity);
        }

        // SHOW DELETE PAGE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opportunity = await _context.Opportunities.FindAsync(id);

            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        // DELETE OPPORTUNITY
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);

            if (opportunity != null)
            {
                _context.Opportunities.Remove(opportunity);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}