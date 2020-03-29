using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMarket.Models;
using EMarket.Context;
using EMarket.ViewModel;

namespace EMarket.Controllers
{
    public class ProductsController : Controller
    {
        public DB context; 
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ProductsController()
        {
            context = new DB();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            var cateogry = context.categoryDb.ToList();
            var viewModel = new ProductCategoriesViewModel
            {
                Category = cateogry
            };
            return View(viewModel);
        }

        public ActionResult EditProduct(int id)
        {
            var category = context.categoryDb.ToList();
            var product = context.productDb.SingleOrDefault(p => p.id == id);
            product.oldCategoryId = product.CategoryId;
            var viewModel = new ProductCategoriesViewModel
            {
                Category = category,
                Product = product
            };
            if (product == null)
                return HttpNotFound();

            return View("EditProduct",viewModel);
        }

        [HttpPost]
        public ActionResult AddConfirmmed(Product product, HttpPostedFileBase imageFile)
        {
            if(imageFile != null)
            { 
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.image = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                imageFile.SaveAs(fileName);
            }

            if (product.id == 0)
            {
                Category category = new Category();
                category = context.categoryDb.Find(product.CategoryId);
                category.number_of_products++;

                context.productDb.Add(product);
            }

            else
            {
                var productInDb = context.productDb.Single(p => p.id == product.id);
                productInDb.name = product.name;
                productInDb.price = product.price;
                productInDb.image = product.image;
                productInDb.description = product.description;
                productInDb.CategoryId = product.CategoryId;
                if(productInDb.CategoryId != product.oldCategoryId)
                {
                    Category category = new Category();
                    category = context.categoryDb.Find(productInDb.CategoryId);
                    category.number_of_products++;

                    category = context.categoryDb.Find(product.oldCategoryId);
                    category.number_of_products--;
                }
            }
            context.SaveChanges();
            return RedirectToAction("ListProducts", "Products");
        }

        public ActionResult listProducts()
        {
            var products = context.productDb.ToList();
            return View(products);
        }

        public ActionResult ViewDetails(int id)
        {
            Product product = new Product();
            product = context.productDb.Where(p => p.id == id).FirstOrDefault();
            return View(product);
        }

        public ActionResult deleteProduct(short id)
        {
            Product product = context.productDb.Find(id);

            Category category = new Category();
            category = context.categoryDb.Find(product.CategoryId);
            category.number_of_products--;

            context.productDb.Remove(product);
            context.SaveChanges();
            return RedirectToAction("listProducts", "Products");
        }

        [HttpPost]
        public ActionResult ListOneCategory(string categoryName)
        {
            List<Product> categoryProducts = null;

            if (categoryName == null)
                return RedirectToAction("listProducts");

            if (categoryName.Equals("Mobile") || categoryName.Equals("Mobiles") || categoryName.Equals("mobiles") || categoryName.Equals("mobile"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 1).ToList();

            else if (categoryName.Equals("Refrigerator") || categoryName.Equals("Refrigerators") || categoryName.Equals("refrigerator") || categoryName.Equals("refrigerators"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 2).ToList();

            else if (categoryName.Equals("Laptop") || categoryName.Equals("Laptops") || categoryName.Equals("laptop") || categoryName.Equals("laptops"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 3).ToList();

            else if (categoryName.Equals("Clothes") || categoryName.Equals("clothes"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 4).ToList();

            else if (categoryName.Equals("Washer") || categoryName.Equals("Washers") || categoryName.Equals("washer") || categoryName.Equals("washers"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 5).ToList();

            else if (categoryName.Equals("Flash Drive") || categoryName.Equals("flash drive") || categoryName.Equals("Flash drive") || categoryName.Equals("flash Drive") || categoryName.Equals("Flash Drive") || categoryName.Equals("flash drives") || categoryName.Equals("Flash drives") || categoryName.Equals("flash Drives"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 6).ToList();

            else if (categoryName.Equals("Camera") || categoryName.Equals("Cameras") || categoryName.Equals("camera") || categoryName.Equals("cameras"))
                categoryProducts = context.productDb.Where(p => p.CategoryId == 7).ToList();

            else
            {
                var category = context.categoryDb.ToList();
                return View(category);
            }

            return View("listProducts", categoryProducts);
        }

        public ActionResult ListOneCategoryId(int categoryId)
        {
            var categories = context.categoryDb.ToList();
            List<Product> categoryProducts= null;

            for (int i = 1; i <= categories.Count; i++)
                if (categoryId == i)
                {
                    categoryProducts = context.productDb.Where(p => p.CategoryId == i).ToList();
                    break;
                }

            return View("listProducts", categoryProducts);
        }
    }
}