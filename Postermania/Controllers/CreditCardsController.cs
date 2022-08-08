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
    public class CreditCardsController : Controller
    {
        private PosterManiaContext db = new PosterManiaContext();

        // GET: CreditCards
        public ActionResult Index()
        {
            return View(db.CreditCards.ToList());
        }

        // GET: CreditCards/Create
        public ActionResult Create()
        {
            return View("Form", new CreditCard());
        }

        // GET: CreditCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View("Form", creditCard);
        }

        // POST: CreditCards/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "ID,Brand,Number,Secret")] CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            if (creditCard.ID == 0)
                db.CreditCards.Add(creditCard);
            else
            {
                var creditCardDB = db.CreditCards.FirstOrDefault(x => x.ID == creditCard.ID);
                creditCardDB.ID = creditCard.ID;
                creditCardDB.Number = creditCard.Number;
                creditCardDB.Secret = creditCard.Secret;
                creditCardDB.Brand = creditCard.Brand;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditCard);
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
