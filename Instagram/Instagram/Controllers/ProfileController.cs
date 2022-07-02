using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class ProfileController : Controller
    {
        InstagramDBEntities db = new InstagramDBEntities();

        public ActionResult Show(int FriendId)
        {
            return View(db.Posts.Where(s => s.fk_Acount_id == FriendId));
        }
    }
}