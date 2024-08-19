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
    public class UsersController : Controller
    {
        private EcommerceDBEntities db = new EcommerceDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,username,email,password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,username,email,password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }










        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user, string confermPassword)
        {
            if ( user.password != confermPassword)
                return View();
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }




        public ActionResult Login()
        {
            if (Session["isLoggedIn"] == "true")
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var theUser = db.Users.SingleOrDefault(u => u.email == user.email);
            if (theUser != null && user.password == theUser.password)
            {
                Session["isLoggedIn"] = "true";
                Session["id"] = theUser.user_id;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult Profile()
        {
            if (Session["isLoggedIn"] != "true")
                return RedirectToAction("Index", "Home");
            User user = db.Users.Find(Session["id"]);
            return View(user);
        }

        [HttpPost]
        public ActionResult Profile(User user)
        {
            User theUser = db.Users.Find(Session["id"]);
            if (Session["isLoggedIn"] != "true")
                return RedirectToAction("Index", "Home");
            theUser.username = user.username;
            theUser.email = user.email;
/*            db.Users.AddOrUpdate(theUser);
*/            db.SaveChanges();
            return View(user);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["isLoggedIn"] != "true")
                return RedirectToAction("Index", "Home");

            User user = db.Users.Find(Session["id"]);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(User user, string currentPassword, string newPassword, string confirmNewPassword)
        {
            if (Session["isLoggedIn"] != "true")
                return RedirectToAction("Index", "Home");

            User theUser = db.Users.Find(Session["id"]);
            if (theUser != null)
            {
                // التحقق من صحة كلمة المرور الحالية
                if (theUser.password != currentPassword)
                {
                    ModelState.AddModelError("currentPassword", "Current password is incorrect.");
                    return View(user);
                }

                // التحقق من صحة كلمة المرور الجديدة وتأكيدها
                if (!string.IsNullOrEmpty(newPassword))
                {
                    if (newPassword != confirmNewPassword)
                    {
                        ModelState.AddModelError("confirmNewPassword", "New passwords do not match.");
                        return View(user);
                    }

                    // تحديث كلمة المرور الجديدة
                    theUser.password = newPassword;
                }

                // تحديث بيانات المستخدم الأخرى
                theUser.username = user.username;
                theUser.email = user.email;
                theUser.phone = user.phone;

                db.SaveChanges();
            }

            return RedirectToAction("Profile"); // العودة إلى صفحة الملف الشخصي أو صفحة أخرى حسب الحاجة
        }





        public ActionResult Logout()
        {
            Session.Remove("id");
            Session.Remove("isLoggedIn");
            return RedirectToAction("Index", "Home");
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
