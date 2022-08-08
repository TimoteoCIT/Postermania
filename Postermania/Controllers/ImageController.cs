using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Postermania.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        // GET: Image/Retrieve/BYTE[]
        public ActionResult Retrieve(byte[] data)
        {
            return File(data, "image");
        }
    }
}