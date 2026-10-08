using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers
{
    public class FollowUpsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowUpsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST FOLLOW-UPS
        public async Task<IActionResult> Index()
        {
            return View(await _context.FollowUps.ToListAsync());
        }

        // SHOW CREATE PAGE
        public IActionResult Create()
        {
            return View();
        }

        // SAVE NEW FOLLOW-UP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                _context.FollowUps.Add(followUp);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(followUp);
        }

        // SHOW EDIT PAGE
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps.FindAsync(id);

            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // SAVE EDITED FOLLOW-UP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FollowUp followUp)
        {
            if (id != followUp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(followUp);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(followUp);
        }

        // SHOW DELETE PAGE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps.FindAsync(id);

            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // DELETE FOLLOW-UP
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);

            if (followUp != null)
            {
                _context.FollowUps.Remove(followUp);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}