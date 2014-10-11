using Gifter.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Gifter.Web.HelperClasses;

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

        [HttpGet]
        public ActionResult UserRequest()
        {
            ViewBag.Tags = ProcessStringData.getTags();

            return View();
        }

        [HttpPost]
        public ActionResult UserRequest(String tags)
        {

            var res = ProcessStringData.getAnswer(tags);
            List<GiftItem> resx = new List<GiftItem>();
            foreach (var x in res)
            {
                resx.Add(new GiftItem(x.Key, (int)(x.Value * 100.0)));
            }
            return Json(new { result = resx });
        }


        [HttpGet]
        public ActionResult AddContent()
        {
            ViewBag.Tags = ProcessStringData.getTags();
            ViewBag.Gifts = ProcessStringData.getGifts();
            return View();
        }

        [HttpGet]
        public ActionResult GetRegisteredUsers()
        {
            var users = (from usr in db.Users
                         select usr).ToList();

            return View(users);
        }

        [HttpPost]
        public ActionResult AddContent(String tags, String presents, String name)
        {
            ProcessStringData.createUser(name, tags, presents);
            List<int> tid = ProcessStringData.getTagsFromString(tags);
            List<int> pid = ProcessStringData.getPresentsFromString(presents);

            return Json(new { tid, pid });
        }
    }

    class GiftItem
    {
        public String name;
        public int percentage;

        public GiftItem(String n, int p)
        {
            name = n;
            percentage = p;
        }
    }
}
