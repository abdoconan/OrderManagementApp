using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Data;
using OrderManagementApp.Models;

namespace OrderManagementApp.Controllers
{
    public class ItemController : Controller
    {

        private readonly OrderManagementDbContext _context;

        public ItemController(OrderManagementDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string searchQuery, string sortOrder)
        {
            var items = _context.Items.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
                items = items.Where(i => i.Name.Contains(searchQuery));

            if (sortOrder == "Price")
                items = items.OrderBy(i => i.Price);

            return View(await items.ToListAsync());
        }


        // GET: /Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item, IFormFile imageFile)
        {


            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Generate a unique file name to avoid conflicts
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                    // Define the path where the file will be saved
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    // Save the file to the wwwroot/images directory
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Save the relative URL path to the database
                    item.ImageUrl = "/images/" + fileName;
                }
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Or use your logging mechanism
                }
            }
            return View(item);
        }
    }
}
