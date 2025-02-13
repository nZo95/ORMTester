﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORMTester.Data;
using ORMTester.Models;

namespace ORMTester.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly AppDbContext _context;

        public ProductsController(ILogger<ProductsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            return View(_context.Products.Include(p => p.ProductType).Include(p => p.Shop).ToList());
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");
            ViewBag.Shops = new SelectList(_context.Shops, "Id", "Name");

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", newProduct.ProductTypeId);
                ViewBag.Shops = new SelectList(_context.Shops, "Id", "Name", newProduct.ShopId);

                return View(newProduct);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Product? existingProduct = _context.Products
                    .Include(p => p.ProductType)
                    .Include(p => p.Shop)
                    .FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", existingProduct.ProductTypeId);
                ViewBag.Shops = new SelectList(_context.Shops, "Id", "Name", existingProduct.ShopId);

                return View(existingProduct);
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

                if (existingProduct != null && ModelState.IsValid)
                {
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.ProductTypeId = updatedProduct.ProductTypeId;
                    existingProduct.ShopId = updatedProduct.ShopId;

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", updatedProduct.ProductTypeId);
                ViewBag.Shops = new SelectList(_context.Shops, "Id", "Name", updatedProduct.ShopId);

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
