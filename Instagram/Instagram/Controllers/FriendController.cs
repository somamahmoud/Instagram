using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class FriendController : Controller
    {
        private InstagramDBEntities db = new InstagramDBEntities();

        public ActionResult FriendRequest(int recieverId)
        {
                Friend_Request request = new Friend_Request();
                request.sender = Convert.ToInt32(Session["accountId"]);
                request.reciever = recieverId;
                db.Friend_Request.Add(request);
                db.SaveChanges();
                return RedirectToAction("Home", "Home");
        }

        public ActionResult ShowRequests() 
        {
            int recieverId = Convert.ToInt32(Session["accountId"]);
            var result = db.Friend_Request.Where(x => x.reciever == recieverId);
            if(result.Count() != 0)
            {
                return View(result);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Approve(string submitButton, int senderId)
        {
            if (submitButton == "Approve")
            {
                ApprovedFriend approved = new ApprovedFriend();
                approved.frienduser = senderId;
                approved.mainuser = Convert.ToInt32(Session["accountId"]);
                db.ApprovedFriends.Add(approved);
                ApprovedFriend approved2 = new ApprovedFriend();
                approved2.frienduser = Convert.ToInt32(Session["accountId"]);
                approved2.mainuser = senderId;
                db.ApprovedFriends.Add(approved2);
                var recieverId = Convert.ToInt32(Session["accountId"]);
                var result = db.Friend_Request.Where(z => z.sender == senderId && z.reciever == recieverId).First();
                db.Friend_Request.Remove(result);
                db.SaveChanges();
                return RedirectToAction("ShowRequests");
            }
            else
            {
                var recieverId = Convert.ToInt32(Session["accountId"]);
                var result = db.Friend_Request.Where(z => z.sender == senderId && z.reciever == recieverId).First();
                db.Friend_Request.Remove(result);
                db.SaveChanges();
                return RedirectToAction("ShowRequests");
            }
        }

        public ActionResult ShowFriends()
        {
            var mainUser = Convert.ToInt32(Session["accountId"]);
            var result = db.ApprovedFriends.Where( y => y.mainuser == mainUser);
            if (result.Count() != 0)
            {
                return View(result);
            }
            return View(result);
        }
    }
}