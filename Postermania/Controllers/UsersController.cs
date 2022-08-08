using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Postermania.DAL;
using Postermania.Models;

namespace Postermania.Controllers
{
    public class UserView
    {
        public User User { get; set; }
        public List<CreditCard> CreditCards { get; set; }
    }

    public class UsersController : Controller
    {
        private PosterManiaContext db = new PosterManiaContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.Include(x => x.CreditCard).ToList());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var userView = new UserView()
            {
                User = new User(),
                CreditCards = db.CreditCards.ToList()
            };
            return View("Form", userView);
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

            var userView = new UserView()
            {
                User = user,
                CreditCards = db.CreditCards.ToList()
            };

            return View("Form", userView);
        }

        // POST: Users/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "ID,UserName,Password,IsAdmin")] User user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            if (user.ID == 0)
                db.Users.Add(user);
            else { 
                var userDb = db.Users.FirstOrDefault(x => x.ID == user.ID);
                userDb.ID = user.ID;
                userDb.UserName = user.UserName;
                userDb.Password = user.Password;
                userDb.IsAdmin = user.IsAdmin;
                userDb.CreditCard = user.CreditCard;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
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
