using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing.Models;

namespace Testing.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepo repo;

        public ProductController(IProductRepo repo)
        {
            this.repo = repo;
        }

        //GET - Index page
        public IActionResult Index()
        {
            var products = repo.GetAllProducts();

            return View(products);
        }
        
        //GET - Product page
        public IActionResult ViewProduct(int id)
        {
            var product = repo.GetProduct(id);

            return View(product);
        }
        
        //GET - Update product page
        public IActionResult UpdateProduct(int id)
        {
            Product product = repo.GetProduct(id);

            if (product == null)
            {
                return View("ProductNotFound");
            }

            return View(product);
        }

        //POST 
        public IActionResult UpdateProductToDatabase(Product product)
        { 
            repo.UpdateProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ProductID });
        }

        //GET
        public IActionResult InsertProduct()
        {
            var product = repo.AssignCategory();
            return View(product);
        }
        //POST
        public IActionResult InsertProductToDatabase(Product productToInsert)
        {
            repo.InsertProduct(productToInsert);

            return RedirectToAction("Index");
        }

        //POST 
        public IActionResult DeleteProduct(Product product)
        {
            repo.DeleteProduct(product);

            return RedirectToAction("Index");
        }

    }
}
