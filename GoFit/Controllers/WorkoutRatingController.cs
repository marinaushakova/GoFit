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
        public WorkoutRatingController()
            : base()
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public WorkoutRatingController(masterEntities context)
            : base(context)
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        public ActionResult AddWorkoutRating()
        {
            return PartialView("_RateWorkoutPartial");
        }


        /// <summary>
        /// Culculates workout average rating based on newly added one
        /// </summary>
        /// <param name="workout_id">id of workout being rated</param>
        /// <param name="rating">value of newly added rating</param>
        /// <returns>Page that called the action</returns>
        [Authorize]
        [HttpPost]
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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "No user could be associated with the rating being added"));
            }

            bool isFirstRating = false;
            workout_rating w_rating = db.workout_rating.Find(workout_id);
            //workout_rating w_rating = db.workout_rating.Where(m => m.workout_id == (int)workout_id).FirstOrDefault();
            if (w_rating != null)
            {
                w_rating.average_rating = (w_rating.average_rating * w_rating.times_rated + (int)rating) / (w_rating.times_rated + 1);
                w_rating.times_rated++;
            }
            else
            {
                w_rating = new workout_rating();
                w_rating.workout_id = (int)workout_id;
                w_rating.average_rating = (int)rating;
                w_rating.times_rated = 1;
                isFirstRating = true;
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    if (isFirstRating)
                    {
                        db.workout_rating.Add(w_rating);
                    }
                    else
                    {
                        db.Entry(w_rating).State = EntityState.Modified;
                    }
                    
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