using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class UpdateController : Controller
    {
        private InstagramDBEntities db = new InstagramDBEntities();

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            int id = Convert.ToInt32(Session["accountId"]);
            return View(db.Accounts.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateProfile(Account account, HttpPostedFileBase file)
        {
            if(ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profile_pic/"), pic);
                    file.SaveAs(path);
                    account.profile_pic = pic;
                }
                else
                {
                    account.profile_pic = account.profile_pic;
                }
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home", "Home");
            }
            return View();
        }
    }
}