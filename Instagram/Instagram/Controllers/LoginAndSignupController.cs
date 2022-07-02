using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class LoginAndSignupController : Controller
    {
        private InstagramDBEntities db = new InstagramDBEntities();

        [HttpGet]
        public ActionResult Login()
        {
            if(Session["accountId"] != null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            var user = db.Accounts.SingleOrDefault(u => u.Email == account.Email && u.Password == account.Password);
            if(user != null)
            {
                Session["accountId"] = user.accountId.ToString();
                Session["Email"] = user.Email.ToString();
                Session["Fname"] = user.Fname.ToString();
                Session["Lname"] = user.Lname.ToString();
                return RedirectToAction("Home","Home", new { id = user.accountId });
            }
            else
            {
                ModelState.AddModelError("","Username Or Password is Wrong");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Account account, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path= System.IO.Path.Combine(Server.MapPath("~/images/profile_pic/"), pic);
                    file.SaveAs(path);
                    account.profile_pic = pic;
                }
                else
                {
                    account.profile_pic = "usericon.jpg";
                }
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Home");
        }

    }
}