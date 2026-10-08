using AcxiomCRM.Data;
using Microsoft.AspNetCore.Mvc;

namespace AcxiomCRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CustomerCount = _context.Customers.Count();
            ViewBag.LeadCount = _context.Leads.Count();
            ViewBag.FollowUpCount = _context.FollowUps.Count();
            ViewBag.OpportunityCount = _context.Opportunities.Count();

            return View();
        }
    }
}