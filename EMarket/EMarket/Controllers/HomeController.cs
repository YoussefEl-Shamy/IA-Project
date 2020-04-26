using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMarket.Context;

namespace EMarket.Controllers
{
    public class HomeController : Controller
    {
        DB db = new DB();

        public HomeController()
        { ViewBag.products = db.productDb.ToList(); }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This web application helps you to manage your market.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}