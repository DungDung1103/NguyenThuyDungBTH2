using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThuyDungBTH2.Data;
using NguyenThuyDungBTH2.Models;
using NguyenThuyDungBTH2.Models.Process;

namespace NguyenThuyDungBTH2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }
         private ExcelProcess _excelProcess = new ExcelProcess();

        public async Task<IActionResult> Index()
        {
            var model = await _context.Customers.ToListAsync();
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
                        var cus = new Customer();
                        //set values for attributes
                        cus.CustomerID = dt.Rows[i][0].ToString();
                        cus.CustomerName = dt.Rows[i][1].ToString();
                        cus.CustomerAge = dt.Rows[i][2].ToString();
                        //add object to context
                        _context.Customers.Add(cus);
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
        // public async Task<IActionResult> Create (Customer std)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _context.Add(std);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(std);
            
        // }
        //  //GET: Customer/Edit/5
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if(id==null)
        //     {
        //         //return NotFound
        //         return View("NotFound");
        //     }
        //     //tìm dữ liệu tring database theo id
        //     var customer = await _context.Customers.FindAsync(id);
        //     if (customer ==null)
        //     {
        //         return View("NotFound");
        //     }
        //     //trả về view kèm dữ liệu
        //     return View(customer);
        // }

        // //POST: Customer/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit (string id, [Bind("CustomerID,CustomerName,CustomerAge")] Customer std)
        // {
        //     if(id != std.CustomerID)
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
        //             if (!CustomerExists(std.CustomerID))
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
        //     var std =  await _context.Customers
        //     .FirstOrDefaultAsync(m => m.CustomerID == id);
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
        //     var std = await _context.Customers.FindAsync(id);
        //     _context.Customers.Remove(std);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        //    } 

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    
    }
}