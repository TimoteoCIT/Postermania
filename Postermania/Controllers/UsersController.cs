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
        public int SelectedCreditCardID { get; set; }
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
        public ActionResult Save(UserView userView)
        {
            userView.User.CreditCard = db.CreditCards.First(creditCard => creditCard.ID == userView.SelectedCreditCardID);

            if (userView.User.ID == 0)
                db.Users.Add(userView.User);
            else { 
                var userDb = db.Users.FirstOrDefault(x => x.ID == userView.User.ID);
                userDb.ID = userView.User.ID;
                userDb.UserName = userView.User.UserName;
                userDb.Password = userView.User.Password;
                userDb.IsAdmin = userView.User.IsAdmin;
                userDb.CreditCard = userView.User.CreditCard;
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
