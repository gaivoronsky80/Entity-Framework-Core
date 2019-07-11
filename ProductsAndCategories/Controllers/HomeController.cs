using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Products");
        }

        [Route("products")]
        [HttpGet]
        public IActionResult Products()
        {
            List<Product> allProducts = dbContext.Products.ToList();
            ViewBag.allProducts = allProducts;
            return View();
        }

        [Route("newproduct")]
        [HttpPost]
        public IActionResult NewProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                System.Console.WriteLine("Success", product.Name);
                return RedirectToAction("Products");
            }
            List<Product> allProducts = dbContext.Products.ToList();
            ViewBag.allProducts = allProducts;
            return View("Products");
        }

        [Route("categories")]
        [HttpGet]
        public IActionResult Categories()
        {
            List<Category> allCategories = dbContext.Categories.ToList();
            ViewBag.allCategories = allCategories;
            return View();
        }

        [Route("newcategory")]
        [HttpPost]
        public IActionResult NewCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                System.Console.WriteLine("Success", category.Name);
                return RedirectToAction("Categories");
            }
            List<Category> allCategories = dbContext.Categories.ToList();
            ViewBag.allCategories = allCategories;
            return View("Categories");
        }

        [Route("products/{ProductId}")]
        [HttpGet]
        public IActionResult Product(int ProductId)
        {
            Product product = dbContext.Products
                .Include(j => j.Categories)
                .ThenInclude(k => k.Category)
                .FirstOrDefault(i => i.ProductId == ProductId );
            ViewBag.thisProduct = product;

            List<Category> allCategories = dbContext.Categories.ToList();
            List<Category> myCategories = new List<Category>();
            foreach (Category i in allCategories)
            {
                bool Unique = true;
                foreach (var j in product.Categories)
                {
                    if (j.Category.CategoryId == i.CategoryId)
                    {
                        Unique = false;
                        break;
                    }
                }
                if(Unique)
                {
                    myCategories.Add(i);
                }
            }
            ViewBag.MyCategories = myCategories;
            return View();
        }

        [Route("addproduct/{ProductId}")]
        [HttpPost]
        public IActionResult AddProduct(Add category, int ProductId)
        {
            Category addCat = dbContext.Categories
                .FirstOrDefault(i => i.CategoryId == category.FindId );
            Association newAssocation = new Association();
            newAssocation.CategoryId = addCat.CategoryId;
            newAssocation.ProductId = ProductId;
            dbContext.Associations.Add(newAssocation);
            dbContext.SaveChanges();
            return RedirectToAction("Product", new {ProductId = ProductId});
        }

        [Route("categories/{CategoryId}")]
        [HttpGet]
        public IActionResult Category(int CategoryId)
        {
            Category category = dbContext.Categories
                .Include(j => j.Products)
                .ThenInclude(k => k.Product)
                .FirstOrDefault(i => i.CategoryId == CategoryId );
            ViewBag.thisCategory = category;

            List<Product> allProducts = dbContext.Products.ToList();
            List<Product> myProducts = new List<Product>();
            foreach (Product i in allProducts)
            {
                bool Unique = true;
                foreach (var j in category.Products)
                {
                    if (j.Product.ProductId == i.ProductId)
                    {
                        Unique = false;
                        break;
                    }
                }
                if(Unique)
                {
                    myProducts.Add(i);
                }
            }
            ViewBag.MyProducts = myProducts;
            return View();
        }

        [Route("addcategory/{CategoryId}")]
        [HttpPost]
        public IActionResult AddCategory(Add product, int CategoryId)
        {
            Product addProd = dbContext.Products
                .FirstOrDefault(i => i.ProductId == product.FindId );
            Association newAssocation = new Association();
            newAssocation.ProductId = addProd.ProductId;
            newAssocation.CategoryId = CategoryId;
            dbContext.Associations.Add(newAssocation);
            dbContext.SaveChanges();
            return RedirectToAction("Category", new {CategoryId = CategoryId});
        }
    }
}