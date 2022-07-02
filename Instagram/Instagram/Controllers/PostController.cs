using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instagram.Models;
using Instagram.viewModels;

namespace Instagram.Controllers
{
    public class PostController : Controller
    {
        InstagramDBEntities db = new InstagramDBEntities();

        [HttpGet]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(Post post, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/post_pic/"), pic);
                    file.SaveAs(path);
                    post.post_pic = pic;
                }
                post.fk_Acount_id = Convert.ToInt32(Session["accountId"]);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Home", "Home");
            }
            return View();
        }

        public ActionResult Show()
        {
            int id = Convert.ToInt32(Session["accountId"]);
            return View(db.Posts.Where( s => s.fk_Acount_id == id));
        }

        public ActionResult Details(int postId)
        {
            post_comment postcomment = new post_comment
            {
                post = db.Posts.Where(s => s.postId == postId).ToList(),
                comments = db.Comments.Where(y => y.postId == postId).ToList(),
            };
            ViewBag.CountLike = db.Likes.Where(u => u.postId == postId).Count();
            ViewBag.CountDisLike = db.DisLikes.Where(u => u.postId == postId).Count();
            return View(postcomment);
        }

        public ActionResult WriteComment(int postId, string writtenComment)
        {
            if(writtenComment != null)
            {
                Comment cmnt = new Comment();
                cmnt.postId = postId;
                cmnt.accountId = Convert.ToInt32(Session["accountId"]);
                cmnt.comment1 = writtenComment;
                db.Comments.Add(cmnt);
                db.SaveChanges();
                var id = postId;
                return RedirectToAction("Details", new {postId = id});
            }
            return View();
        }

        public ActionResult Like(int postId)
        {
            var accId = Convert.ToInt32(Session["accountId"]);
            var checkDisLikes = db.DisLikes.SingleOrDefault(t => t.accountId == accId && t.postId == postId);
            var checkLikes = db.Likes.SingleOrDefault(t => t.accountId == accId && t.postId == postId);
            if (checkDisLikes == null && checkLikes == null)
            {
                Like newlike = new Like();
                newlike.accountId = accId;
                newlike.postId = postId;
                db.Likes.Add(newlike);
                db.SaveChanges();
                var id = postId;
                return RedirectToAction("Details", new { postId = id });
            }
            else
            {
                db.DisLikes.Remove(checkDisLikes);
                db.SaveChanges();
                var id = postId;
                return RedirectToAction("Like", new { postId = id });
            }
        }

        public ActionResult DisLike(int postId)
        {
            var accId = Convert.ToInt32(Session["accountId"]);
            var checkLikes = db.Likes.SingleOrDefault(t => t.accountId == accId && t.postId == postId);
            var checkDisLikes = db.DisLikes.SingleOrDefault(t => t.accountId == accId && t.postId == postId);
            if (checkLikes == null && checkDisLikes == null)
            {
                DisLike newdislike = new DisLike();
                newdislike.accountId = accId;
                newdislike.postId = postId;
                db.DisLikes.Add(newdislike);
                db.SaveChanges();
                var id = postId;
                return RedirectToAction("Details", new { postId = id });
            }
            else
            {
                db.Likes.Remove(checkLikes);
                db.SaveChanges();
                var id = postId;
                return RedirectToAction("DisLike", new { postId = id });
            }
        }
    }
}