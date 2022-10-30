using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;

namespace NguyenThuyDungBTH2.Controllers
{
    public class StudentController : Controller
    {
        // khai báo ApplicationDbContext
        private readonly ApplicationDbContext _context;
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }

        // xây dựng action trả về danh sách Student
        public async Task<IActionResult> Index()
        {
            //lấy danh sách sinh viên và trả về View
            var model = await _context.Students.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //async: xử lý bất đồng bộ
        public async Task<IActionResult> Create (Student std)
        {
            if(ModelState.IsValid)
            {
                // add vào ApplicationDbContext
                _context.Add(std);
                //luu thay đổi vào db
                await _context.SaveChangesAsync();
                //sau khi lưu thay đổi, điều hướng về trang index
                return RedirectToAction(nameof(Index));
            }
            return View(std);
            
        }
        //GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if(id==null)
            {
                //return NotFound
                return View("NotFound");
            }
            //tìm dữ liệu tring database theo id
            var student = await _context.Students.FindAsync(id);
            if (student ==null)
            {
                return View("NotFound");
            }
            //trả về view kèm dữ liệu
            return View(student);
        }

        //POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (string id, [Bind("StudentID,StudentName")] Student std)
        {
            if(id != std.StudentID)
            {
                return View("NotFound");
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(std.StudentID))
                    {
                       return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }

        // Get: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            var std =  await _context.Students
            .FirstOrDefaultAsync(m => m.StudentID == id);
            if (std == null)
            {
                return View("NotFound");
            }

            return View(std);
        }

        //POST: Product/Delete/5
           [HttpPost, ActionName("Delete")]
           [ValidateAntiForgeryToken]

           public async Task<IActionResult> DeleteConfirmed(string id)
           {
            var std = await _context.Students.FindAsync(id);
            _context.Students.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           } 

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}