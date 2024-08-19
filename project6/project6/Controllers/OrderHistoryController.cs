using project6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace project6.Controllers
{
    public class OrderHistoryController : Controller
    {
        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: OrderHistory
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OrderHistory()
        {
            int userId = Convert.ToInt32(Session["id"]);

            // Retrieve orders for the logged-in user
            var orders = db.Orders
                           .Where(o => o.user_id == userId)
                           .Include(o => o.OrderItems.Select(oi => oi.Product))  // Include related OrderItems and Products
                           .ToList();

            return View(orders);
        }


    }
}