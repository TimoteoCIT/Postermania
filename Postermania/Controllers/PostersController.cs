﻿using System;
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
            return View(db.Posters.Include(x=>x.Dimensions).ToList());
        }

        // GET: Posters/Details/5
        public ActionResult Details(int? id)
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

        // GET: Posters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posters/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Poster poster)
        {
            HttpPostedFileBase image = Request.Files["ImageData"];
            poster.Image = Util.Images.ReadImage(image);

            //if (ModelState.IsValid)
            {
                db.Posters.Add(poster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poster);
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

            return View(posterView);
        }

        // POST: Posters/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BasePrice,PricePerCm,type,Image")] Poster poster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poster);
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
