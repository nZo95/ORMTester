using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORMTester.Data;
using ORMTester.Models;

namespace ORMTester.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        public ProductsController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string ?name = collection["ProductName"];
                string ?type = collection["ProductType"];

                Product product = new Product
                {
                    Name = name,
                    Type = type,
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Product? product = _context.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(product);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product updatedProduct)
        {
            try
            {
                Product? existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);

                if (existingProduct != null)
                {
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Type = updatedProduct.Type;

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Product ?product = _context.Products.FirstOrDefault(p => p.Id == id);
                
                if (product != null)
                {
                    _context.Remove(product);
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
