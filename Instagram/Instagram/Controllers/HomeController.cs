using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instagram.Models;
using Instagram.viewModels;

namespace Instagram.Controllers
{
    public class HomeController : Controller
    {

        private InstagramDBEntities db = new InstagramDBEntities();

        public ActionResult Home()
        {
            ViewBag.Count = db.Posts.SqlQuery("Select * from Likes join Post on Likes.postId = Post.postId").Count();
            return View(db.Posts);
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