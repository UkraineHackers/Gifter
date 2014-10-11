using Gifter.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Gifter.DataAccess;

namespace Gifter.Web.Controllers
{
    public class HomeController : Controller
    {
        GifterDBEntities db = new GifterDBEntities();

        public ActionResult Index()
        {
            var users = (from usr in db.Users
                        select usr).ToList();

            return View(users);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult AddContent()
        {

            ViewBag.Message = "Add Content";

            return View();
        }

        [HttpPost]
        public ActionResult AddContent(String tags, String presents)
        {
            ViewBag.Message = "Add Content";
            String res = tags + "&" + presents;
            return Json(new {res});
        }
    }
}
