using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORMTester.Data;
using ORMTester.Models;

namespace ORMTester.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly ILogger<ProductTypesController> _logger;

        private readonly AppDbContext _context;

        public ProductTypesController(ILogger<ProductTypesController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            return View(_context.ProductTypes.ToList());
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
        public ActionResult Create(ProductType newProductType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.ProductTypes.Add(newProductType);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(newProductType);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ProductType? existingProductType = _context.ProductTypes.FirstOrDefault(p => p.Id == id);

                if (existingProductType == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(existingProductType);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductType updatedProductType)
        {
            try
            {
                ProductType? existingProductType = _context.ProductTypes.FirstOrDefault(p => p.Id == id);

                if (existingProductType != null && ModelState.IsValid)
                {
                    existingProductType.Name = updatedProductType.Name;

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
                ProductType? productType = _context.ProductTypes.FirstOrDefault(p => p.Id == id);
                
                if (productType != null)
                {
                    _context.Remove(productType);
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
