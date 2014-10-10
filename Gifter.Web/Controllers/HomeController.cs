using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gifter.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
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
