using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;

namespace NguyenThuyDungBTH2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Customers.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Customer std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
            
        }
    }
}