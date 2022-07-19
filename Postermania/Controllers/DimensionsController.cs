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

        // GET: Dimensions/Details/5
        public ActionResult Details(int? id)
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

        // GET: Dimensions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dimensions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Width,Height")] Dimension dimension)
        {
            if (ModelState.IsValid)
            {
                db.Dimensions.Add(dimension);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimension);
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
            return View(dimension);
        }

        // POST: Dimensions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Width,Height")] Dimension dimension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimension).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimension);
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
