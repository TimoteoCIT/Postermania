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
    public class PosterView
    {
        public Poster Poster { get; set; }
        public List<Dimension> Dimensions { get; set; }
    }

    // https://www.c-sharpcorner.com/article/crud-using-asp-net-mvc-5-entity-framework/
    public class PostersController : Controller
    {
        private PosterManiaContext db = new PosterManiaContext();

        // GET: Posters
        public ActionResult Index()
        {
            return View(db.Posters.Include(x => x.Dimensions).ToList());
        }

        // GET: Posters/Create
        public ActionResult Create()
        {
            var posterView = new PosterView()
            {
                Poster = new Poster(),
                Dimensions = db.Dimensions.ToList()
            };
            return View("Form", posterView);
        }

        // GET: Posters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = db.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }

            var posterView = new PosterView()
            {
                Poster = poster,
                Dimensions = db.Dimensions.ToList()
            };

            return View("Form", posterView);
        }

        // POST: Posters/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Poster poster)
        {
            if (!ModelState.IsValid)
            {
                //return RedirectToAction("Create");
            }

            HttpPostedFileBase image = Request.Files["ImageData"];
            poster.Image = Util.Images.ReadImage(image);

            if (poster.ID == 0)
                db.Posters.Add(poster);
            else
            {
                var posterDb = db.Posters.FirstOrDefault(x => x.ID == poster.ID);
                posterDb.ID = poster.ID;
                posterDb.Name = poster.Name;
                posterDb.BasePrice = poster.BasePrice;
                posterDb.BasePrice = poster.BasePrice;
                posterDb.PricePerCm = poster.PricePerCm;
                posterDb.type = poster.type;
                posterDb.Image = poster.Image;
                posterDb.Dimensions = poster.Dimensions;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Posters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = db.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }
            return View(poster);
        }

        // POST: Posters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poster poster = db.Posters.Find(id);
            db.Posters.Remove(poster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Posters/RetrieveImage/5
        public ActionResult RetrieveImage(int id)
        {
            Poster poster = db.Posters.Find(id);
            return File(poster.Image, "image");
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
