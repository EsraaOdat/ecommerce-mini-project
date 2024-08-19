using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project6.Models;

namespace project6.Controllers
{
    public class CartsController : Controller
    {
        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: Carts
        public ActionResult Index()
        {
            int currentUserId = Convert.ToInt32(Session["id"]);

            var carts = db.Carts.Include(c => c.Product).Include(c => c.User)
                        .Where(c => c.user_id == currentUserId).ToList();

            return View(carts);
        }


        /*        [HttpPost]
        */        /* public ActionResult AddToCart(int product_id, int quantity)
                 {
                     int currentUserId = Convert.ToInt32(Session["id"]);

                     if (currentUserId == 0)
                     {
                         return RedirectToAction("Login", "Users");
                     }

                     var existingCartItem = db.Carts
                         .FirstOrDefault(c => c.user_id == currentUserId && c.product_id == product_id);

                     if (existingCartItem != null)
                     {
                         existingCartItem.quantity += quantity;
                         db.Entry(existingCartItem).State = EntityState.Modified;
                     }
                     else
                     {
                         var newCartItem = new Cart
                         {
                             user_id = currentUserId,
                             product_id = product_id,
                             quantity = quantity
                         };
                         db.Carts.Add(newCartItem);
                     }

                     db.SaveChanges();

                     return RedirectToAction("Index", "Products");


                 }


         */ // Action Method لتحديث الكميات

        public ActionResult UpdateCart(int id, int quantity)
        {
            var cartItem = db.Carts.Find(id);

            if (cartItem != null && quantity > 0)
            {
                cartItem.quantity = quantity;
                db.SaveChanges();
            }
            else if (cartItem != null && quantity == 0)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Carts");
        }

        //[HttpPost]
        ///*        [ValidateAntiForgeryToken]
        //*/
        //public ActionResult UpdateCart(int id, int quantity)
        //{
        //    //int id = Convert.ToInt32(Session["id"]);
        //    //int quantity = Convert.ToInt32(Session["quantity1"]);
        //    var cartItem = db.Carts.Find(id);

        //    if (cartItem != null && quantity > 0)
        //    {
        //        cartItem.quantity = quantity;
        //        db.SaveChanges();
        //    }
        //    else if (cartItem != null && quantity == 0)
        //    {
        //        db.Carts.Remove(cartItem);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Carts", "Index");
        //}



        /*  public ActionResult UpdateCartItems() { return View(); }


          [HttpPost]
          public ActionResult UpdateCartItems(IEnumerable<Cart> cartItems)
          {
              if (cartItems != null)
              {
                  foreach (var item in cartItems)
                  {
                      var cartItem = db.Carts.Find(item.cart_id);
                      if (cartItem != null)
                      {
                          cartItem.quantity = item.quantity;
                          db.Entry(cartItem).State = EntityState.Modified;
                      }
                  }
                  db.SaveChanges();
              }
              return RedirectToAction("Index");
          }

  */
























        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cart_id,user_id,product_id,quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", cart.product_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", cart.user_id);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", cart.product_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", cart.user_id);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cart_id,user_id,product_id,quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", cart.product_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", cart.user_id);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
