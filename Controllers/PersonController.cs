using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;
using NguyenThuyDungBTH2.Models.Process;

namespace NguyenThuyDungBTH2.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController (ApplicationDbContext context)
        {
            _context = context;
        }
        private ExcelProcess _excelProcess = new ExcelProcess();
      

        // xây dựng action trả về danh sách Student
        
        
        public async Task<IActionResult> Index()
        {
            var model = await _context.Persons.ToListAsync();
            return View(model);
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
                        var per = new Person();
                        //set values for attributes
                       per.PersonID = dt.Rows[i][0].ToString();
                        per.PersonName = dt.Rows[i][1].ToString();
                        per.PersonAge = dt.Rows[i][2].ToString();
                       
                        //add object to context
                        _context.Persons.Add(per);
                        }
                         //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                }
            }
        }
            return View();
}
        // public IActionResult Create()
        // {
        //     return View();
        // }
        // [HttpPost]
        // public async Task<IActionResult> Create (Person std)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _context.Add(std);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(std);
            
        // }
        //  //GET: Person/Edit/5
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if(id==null)
        //     {
        //         //return NotFound
        //         return View("NotFound");
        //     }
        //     //tìm dữ liệu tring database theo id
        //     var person = await _context.Persons.FindAsync(id);
        //     if (person ==null)
        //     {
        //         return View("NotFound");
        //     }
        //     //trả về view kèm dữ liệu
        //     return View(person);
        // }

        // //POST: Student/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit (string id, [Bind("PersonID,PersonName,PersonAge")] Person std)
        // {
        //     if(id != std.PersonID)
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
        //             if (!StudentExists(std.PersonID))
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
        //     var std =  await _context.Persons
        //     .FirstOrDefaultAsync(m => m.PersonID == id);
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
        //     var std = await _context.Persons.FindAsync(id);
        //     _context.Persons.Remove(std);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        //    } 

        private bool StudentExists(string id)
        {
            return _context.Persons.Any(e => e.PersonID == id);
        }
    
    }
}