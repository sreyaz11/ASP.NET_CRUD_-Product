using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Models;
using System.Diagnostics;

namespace Student.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductDbContext dbContext;

        public HomeController(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var productData = dbContext.products.ToList();
            return View(productData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await dbContext.products.AddAsync(product);
                await dbContext.SaveChangesAsync();
                TempData["insert_success"] = "Inserted";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            var productData = await dbContext.products.FirstOrDefaultAsync(x => x.Id == id);
            return View(productData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || dbContext.products == null)
                return NotFound();
            var product = await dbContext.products.FindAsync(id);
            if(product == null)
                return NotFound();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Product product)
        {
            if(id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                dbContext.products.Update(product);
                await dbContext.SaveChangesAsync();
                TempData["update_success"] = "Updated";
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || dbContext.products == null)
            {
                return NotFound();
            }
            var product = await dbContext.products.FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var product = await dbContext.products.FindAsync(id);
            if( product != null)
            {
                dbContext.products.Remove(product);
            }
            await dbContext.SaveChangesAsync();
            TempData["delete_success"] = "Deleted";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
