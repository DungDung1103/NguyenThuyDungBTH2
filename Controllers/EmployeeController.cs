using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;
using NguyenThuyDungBTH2.Models.Process;

namespace NguyenThuyDungBTH2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }

        // public async Task<IActionResult> Index()
        // {
        //     var model = await _context.Employees.ToListAsync();
        //     return View(model);
        // }
        // public IActionResult Create()
        // {
        //     return View();
        // }
        // [HttpPost]
        // public async Task<IActionResult> Create (Employee std)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _context.Add(std);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(std);
            
        // }
        //  //GET: Student/Edit/5
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if(id==null)
        //     {
        //         //return NotFound
        //         return View("NotFound");
        //     }
        //     //tìm dữ liệu tring database theo id
        //     var employee = await _context.Employees.FindAsync(id);
        //     if (employee ==null)
        //     {
        //         return View("NotFound");
        //     }
        //     //trả về view kèm dữ liệu
        //     return View(employee);
        // }

        // //POST: Student/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit (string id, [Bind("EmployeeID,EmployeeName,EmployeeAge")] Employee std)
        // {
        //     if(id != std.EmployeeID)
        //     {
        //         return View("NotFound");
        //     }
        //     if(ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(std);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!EmployeeExists(std.EmployeeID))
        //             {
        //                return View("NotFound");
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(std);
        // }

        // // Get: Product/Delete/5
        // public async Task<IActionResult> Delete(string id)
        // {
        //     if(id == null)
        //     {
        //         return View("NotFound");
        //     }
        //     var std =  await _context.Employees
        //     .FirstOrDefaultAsync(m => m.EmployeeID == id);
        //     if (std == null)
        //     {
        //         return View("NotFound");
        //     }

        //     return View(std);
        // }

        // //POST: Product/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]

        //    public async Task<IActionResult> DeleteConfirmed(string id)
        //    {
        //     var std = await _context.Employees.FindAsync(id);
        //     _context.Employees.Remove(std);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        //    } 

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());

        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension !=".xls" && fileExtension!= ".xlsx")
                {
                    ModelState.AddModelError("","Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to server
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory()+ "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using ( var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //reade data form file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt
                        for (int i=0 ; i<dt.Rows.Count;i++)
                        {
                        //create a new Employee object
                        var emp = new Employee();
                        //set values for attributes
                        emp.EmployeeID = dt.Rows[i][0].ToString();
                        emp.EmployeeName = dt.Rows[i][1].ToString();
                        emp.EmployeeAge = dt.Rows[i][2].ToString();
                        //add object to context
                        _context.Employees.Add(emp);
                        }
                         //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                }
            }
        }
            return View();
        
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    
    }
}