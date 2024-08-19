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
    public class ProductsController : Controller
    {
        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }








        [HttpPost]
        public ActionResult AddToCart(int product_id, int quantity)
        {
            int currentUserId = Convert.ToInt32(Session["id"]);

            // تحقق من أن المستخدم قد سجل الدخول
            if (currentUserId == 0)
            {
                return RedirectToAction("Login", "Users"); // إعادة التوجيه إلى صفحة تسجيل الدخول إذا لم يكن المستخدم قد سجل الدخول
            }

            // تحقق مما إذا كان المنتج موجودًا بالفعل في عربة المستخدم
            var existingCartItem = db.Carts
                .FirstOrDefault(c => c.user_id == currentUserId && c.product_id == product_id);

            if (existingCartItem != null)
            {
                // زيادة الكمية إذا كان المنتج موجودًا بالفعل في العربة
                existingCartItem.quantity += quantity;
                db.Entry(existingCartItem).State = EntityState.Modified;
            }
            else
            {
                // إضافة منتج جديد إلى العربة
                var newCartItem = new Cart
                {
                    user_id = currentUserId,
                    product_id = product_id,
                    quantity = quantity
                };
                db.Carts.Add(newCartItem);
            }

            db.SaveChanges();

            // إعادة التوجيه إلى صفحة المنتجات بعد الإضافة إلى العربة
            return RedirectToAction("Index", "Products");
        }









        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,description,price,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,description,price,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
