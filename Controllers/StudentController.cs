using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;
using NguyenThuyDungBTH2.Models.Process;

namespace NguyenThuyDungBTH2.Controllers
{
    public class StudentController : Controller
    {
        // khai báo ApplicationDbContext
        private readonly ApplicationDbContext _context;
        // private ExcelProcess _excelProcess = new ExcelProcess();
         private IdAuto auto = new IdAuto();
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }

        // xây dựng action trả về danh sách Student
        public async Task<IActionResult> Index()
        {
            //lấy danh sách sinh viên và trả về View
            //  var id = _context.Students.OrderByDescending(m => m.StudentID).First().StudentID;
            return View(await _context.Students.ToListAsync());
        }

                public IActionResult Create()
            {
            //   var id = _context.Students.OrderByDescending(m => m.StudentID).First().StudentID;
             ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
            }
            //POST: Student//Create
            //To Protect
            //For more details,
            [HttpPost]
            [ValidateAntiForgeryToken]
        //async: xử lý bất đồng bộ
            public async Task<IActionResult> Create ([Bind("StudentID,StudentName,FacultyID")] Student student)
            {
               if(ModelState.IsValid)
              {
                // add vào ApplicationDbContext
                _context.Add(student);
                //luu thay đổi vào db
                await _context.SaveChangesAsync();
                //sau khi lưu thay đổi, điều hướng về trang index
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties,"FacultyID","FacultyName", student.FacultyID);
            return View(student);
        }
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
            

        // public async Task<IActionResult> Upload()
        //     {
        //         return View();
        //     }
        // public async Task<IActionResult> Upload(IFormFile file)
        // {
        //     if(file!=null)
        //         {
        //             string fileExtension = Path.GetExtension(file.FileName);
        //             if(fileExtension !=".xls" && fileExtension!= ".xlsx")
        //                 {
        //                     ModelState.AddModelError("","Please choose excel file to upload!");
        //                 }
        //             else
        //                 {
        //                     //rename file when upload to server
        //                     var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
        //                     var filePath = Path.Combine(Directory.GetCurrentDirectory()+ "/Uploads/Excels", fileName);
        //                     var fileLocation = new FileInfo(filePath).ToString();
        //                     using ( var stream = new FileStream(filePath, FileMode.Create))
        //                         {
        //                             //save file to server
        //                             await file.CopyToAsync(stream);
        //                             //reade data form file and write to database
        //                             var dt = _excelProcess.ExcelToDataTable(fileLocation);
                    
        //                             //using for loop to read data from dt
        //                             for (int i=0 ; i<dt.Rows.Count;i++)
        //                                {
        //                                      //create a new Employee object
        //                                     var std = new Student();
        //                                     //set values for attributes
        //                                     std.StudentID = dt.Rows[i][0].ToString();
        //                                     std.StudentName = dt.Rows[i][1].ToString();
                       
        //                                      //add object to context
        //                                     _context.Students.Add(std);
        //                                 }
        //                             //save to database
        //                             await _context.SaveChangesAsync();
        //                             return RedirectToAction(nameof(Index));
        //                          }
        //                 }
        //         }
        //     return View();
        // }
        
    }
  }


        // }
        //GET: Student/Edit/5
       