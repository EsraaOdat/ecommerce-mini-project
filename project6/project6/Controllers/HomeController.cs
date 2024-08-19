using project6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project6.Controllers
{
    public class HomeController : Controller
    {



        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            // Retrieve the list of categories from the database
            var categories = db.Categories.ToList();
            return View(categories);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}