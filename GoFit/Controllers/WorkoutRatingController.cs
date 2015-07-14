using GoFit.Controllers.ControllerHelpers;
using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class WorkoutRatingController : GoFitBaseController
    {
        private masterEntities db;
        private int currUserId;
        private UserAccess userAccess;

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public WorkoutRatingController() : base ()
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public WorkoutRatingController(masterEntities context) : base(context)
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Culculates workout average rating based on newly added one
        /// </summary>
        /// <param name="workout_id">id of workout being rated</param>
        /// <param name="rating">value of newly added rating</param>
        /// <returns>Page that called the action</returns>
        [Authorize]
        public ActionResult AddWorkoutRating(int? workout_id, int? rating)
        {
            if (workout_id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No workout id was specified."));
            }
            if (rating == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No rating value was specified."));
            }

            int userID = userAccess.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }

            workout_rating w_rating = db.workout_rating.Where(m => m.workout_id == (int)workout_id).FirstOrDefault();
            w_rating.average_rating = (w_rating.average_rating * w_rating.times_rated + (int)rating) / (w_rating.times_rated + 1);
            w_rating.times_rated++;

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(w_rating).State = EntityState.Modified;
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
                catch (Exception ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to rate the requested workout."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Failed to rate the requested workout."));
            }

        }
	}
}