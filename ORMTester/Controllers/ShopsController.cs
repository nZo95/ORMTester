using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORMTester.Data;
using ORMTester.Models;

namespace ORMTester.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ILogger<ShopsController> _logger;

        private readonly AppDbContext _context;

        public ShopsController(ILogger<ShopsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Shops.Include(e => e.Products).ToList());
        }

        public ActionResult Details(int id)
        {
            Shop? shop = _context.Shops
                .Include(p => p.Products)
                .FirstOrDefault(p => p.Id == id);

            if (shop == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(shop);
        }

        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shop newShop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Shops.Add(newShop);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Products = new SelectList(_context.Products, "Id", "Name");

                return View(newShop);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Shop? existingShop = _context.Shops.FirstOrDefault(p => p.Id == id);

                if (existingShop == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Products = new SelectList(_context.Products, "Id", "Name", existingShop.Products);

                return View(existingShop);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Shop updatedShop)
        {
            try
            {
                Shop? existingShop = _context.Shops.FirstOrDefault(p => p.Id == id);

                if (existingShop != null && ModelState.IsValid)
                {
                    existingShop.Name = updatedShop.Name;
                    existingShop.Products = updatedShop.Products;

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Products = new SelectList(_context.Products, "Id", "Name", updatedShop.Products);

                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Shop? shop = _context.Shops.FirstOrDefault(p => p.Id == id);
                
                if (shop != null)
                {
                    _context.Remove(shop);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
