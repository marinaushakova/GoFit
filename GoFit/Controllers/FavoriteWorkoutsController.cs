using GoFit.Controllers.ControllerHelpers;
using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net;
using System.Net.Http;

namespace GoFit.Controllers
{
    public class FavoriteWorkoutsController : GoFitBaseController
    {
        private masterEntities db;
        private int currUserId;
        private UserAccess userAccess;

        private const int PAGE_SIZE = 10;

        /// <summary>
        /// Getter/setter for the pageSize instance variable
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public FavoriteWorkoutsController() : base ()
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
            pageSize = PAGE_SIZE;
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public FavoriteWorkoutsController(masterEntities context) : base(context)
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
            pageSize = PAGE_SIZE;
        }
        //
        // GET: /FavoriteWorkouts/
        public ActionResult Index()
        {
            currUserId = userAccess.getUserId(User.Identity.Name);
            var user_favorite_workouts = from w in db.user_favorite_workout where w.user_id == currUserId select w;
            return View(user_favorite_workouts);
        }

        [Authorize]
        public ActionResult AddWorkoutToFavorites(int? workout_id)
        {
            if (workout_id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No workout id was specified."));
            }
            
            int userID = userAccess.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }

            user_favorite_workout fav_workout = new user_favorite_workout();
            fav_workout.user_id = userID;
            fav_workout.workout_id = (int)workout_id;

            if (ModelState.IsValid)
            {
                try
                {
                    db.user_favorite_workout.Add(fav_workout);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Home", new { workoutId = workout_id });
                }
                catch (Exception ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to add the requested workout to favorites."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Failed to add the requested workout to favorites."));
            }

        }

        [Authorize]
        public ActionResult RemoveWorkoutFromFavorites([Bind(Include = "id,timestamp")] int? workout_id)
        {
            if (workout_id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No workout id was specified."));
            }

            int userID = userAccess.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }

            try
            {
                user_favorite_workout favWorkout = db.user_favorite_workout.Where(m => m.workout_id == (int)workout_id &&
                                                            m.user_id == userID).FirstOrDefault();
                if (favWorkout == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The workout is not in Favorite list"));
                }
                db.user_favorite_workout.Remove(favWorkout);
                db.SaveChanges();
                return RedirectToAction("Details", "Home", new { workoutId = workout_id });
            }
            catch (Exception ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the requested workout from favorites."));
            }

        }
	}
}