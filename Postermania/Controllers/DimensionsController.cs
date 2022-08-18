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
    public class DimensionsController : Controller
    {
        private PosterManiaContext db = new PosterManiaContext();

        // GET: Dimensions
        public ActionResult Index()
        {
            return View(db.Dimensions.ToList());
        }

        // GET: Dimensions/Create
        public ActionResult Create()
        {
            return View("Form", new Dimension());
        }

        // GET: Dimensions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dimension dimension = db.Dimensions.Find(id);
            if (dimension == null)
            {
                return HttpNotFound();
            }
            return View("Form", dimension);
        }

        // POST: Dimensions/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "ID,Width,Height")] Dimension dimension)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            if (dimension.ID == 0)
                db.Dimensions.Add(dimension);
            else
            {
                var dimensionDB = db.Dimensions.FirstOrDefault(x => x.ID == dimension.ID);
                dimensionDB.ID = dimension.ID;
                dimensionDB.Width = dimension.Width;
                dimensionDB.Height = dimension.Height;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Dimensions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dimension dimension = db.Dimensions.Find(id);
            if (dimension == null)
            {
                return HttpNotFound();
            }
            return View(dimension);
        }

        // POST: Dimensions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dimension dimension = db.Dimensions.Find(id);
            db.Dimensions.Remove(dimension);
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
