
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;

namespace NguyenThuyDungBTH2.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FacultyController (ApplicationDbContext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index()
        {
            //lấy danh sách sinh viên và trả về View
            var model = await _context.Faculties.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Faculty fac)
        {
            if(ModelState.IsValid)
            {
                _context.Add(fac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fac);
        }


    }
}