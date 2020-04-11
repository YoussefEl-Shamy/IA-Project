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
            if (product.id == 0)
            {
                if (imageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    product.image = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    imageFile.SaveAs(fileName);
                }
                else
                {
                    var viewModel = new ProductCategoriesViewModel
                    {
                        Product = product,
                        Category = context.categoryDb.ToList()
                    };
                    return View("AddProduct", viewModel);
                }

                Category category = new Category();
                category = context.categoryDb.Find(product.CategoryId);
                category.number_of_products++;

                context.productDb.Add(product);
            }

            else
            {
                if (imageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    product.image = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    imageFile.SaveAs(fileName);
                }
                else
                {
                    var viewModel = new ProductCategoriesViewModel
                    {
                        Product = product,
                        Category = context.categoryDb.ToList()
                    };
                    var editAction = EditProduct(product.id);
                    return editAction;
                }

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

            if (String.IsNullOrEmpty(categoryName))
                return RedirectToAction("listProducts");

            else if(!String.IsNullOrEmpty(categoryName))
            {
                categoryProducts = context.productDb.Where(p => p.Category.name.Contains(categoryName)).ToList();
                if(categoryProducts.Count == 0)
                {
                    var categories = context.categoryDb.ToList();
                    return View(categories);
                }
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