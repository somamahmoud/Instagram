using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class UserSearchController : Controller
    {
        private InstagramDBEntities db = new InstagramDBEntities();

        public ActionResult Search(string search)
        {
            var searched = db.Accounts.Where(x => x.Email.Contains(search));
            if (searched.Count() != 0)
            {
                return View(searched);
            }
            return View();
        }
    }
}