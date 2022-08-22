using Postermania.DAL;
using Postermania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Postermania.Controllers
{
    public class HomeView
    {
        public List<Poster> Posters { get; set; }
        public List<Poster> Scrolls { get; set; }
        public List<Poster> Frameds { get; set; }
    }

    public class HomeController : Controller
    {
        private PosterManiaContext db = new PosterManiaContext();

        public ActionResult Index()
        {
            var homeView = new HomeView()
            {
                Posters = db.Posters.Where(x => x.Type == ItemType.Poster).Take(5).ToList(),
                Scrolls = db.Posters.Where(x => x.Type == ItemType.Scroll).Take(5).ToList(),
                Frameds = db.Posters.Where(x => x.Type == ItemType.Framed).Take(5).ToList()
            };

            return View(homeView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}