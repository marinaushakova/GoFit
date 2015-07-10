using GoFit.Controllers.ControllerHelpers;
using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class CommentsController : GoFitBaseController
    {
        private masterEntities db;
        private int currUserId;
        private UserAccess userAccess;


        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public CommentsController() : base ()
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public CommentsController(masterEntities context) : base(context)
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Adds comment to workout
        /// </summary>
        /// <param name="comment">Comment being added</param>
        /// <returns>AddComment pertial view</returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddComment(comment comment)
        {
            if (comment == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No comment to add was specified."));
            }

            comment.date_created = DateTime.Now;
            comment.User_id = userAccess.getUserId(User.Identity.Name);
            if (Session["workout_id"] != null) comment.Workout_id = (int)Session["workout_id"];
            if (ModelState.IsValid)
            {
                try
                {
                    db.comments.Add(comment);
                    db.SaveChanges();
                    if (this.Request.UrlReferrer != null)
                    {
                        string url = this.Request.UrlReferrer.PathAndQuery;
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch
                {
                    return PartialView("_ErrorPartial", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Comment could not be added."));
                }
            }
            else
            {
                return PartialView("_ErrorPartial", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid comment."));
            }
        }
	}
}