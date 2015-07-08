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
using PagedList;

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


        /// <summary>
        /// Returns the list of Favorite Workouts for currently logged in user
        /// </summary>
        /// <param name="sortBy">String parameter telling the controller how to sort the workouts</param>
        /// <param name="page">The request page in the list of workouts</param>
        /// <param name="workoutSearch">Search parameter</param>
        /// <returns>A list of user favorite workouts from DB</returns>
        [Authorize]
        public ActionResult Index(string sortBy, int? page, WorkoutSearch workoutSearch)
        {
            currUserId = userAccess.getUserId(User.Identity.Name);
            var user_favorite_workouts = from w in db.user_favorite_workout where w.user_id == currUserId select w;

            user_favorite_workouts = this.doSearch(user_favorite_workouts, workoutSearch, sortBy, page);
            user_favorite_workouts = this.doSort(user_favorite_workouts, sortBy);

            int pageNumber = (page ?? 1);
            return View("Index", user_favorite_workouts.ToPagedList(pageNumber, pageSize));
        }


        /// <summary>
        /// Gets list of favorite workouts for currently logged in user
        /// </summary>
        /// <returns>List of favorite workouts</returns>
        public PartialViewResult FavoriteList()
        {
            currUserId = userAccess.getUserId(User.Identity.Name);
            var favoriteList = db.user_favorite_workout.Where(m => m.user_id.Equals(currUserId)).ToList();
            return PartialView(favoriteList);
        }

        /// <summary>
        /// Adds workout with passed in id to favorite list of currently logged in user
        /// </summary>
        /// <param name="workout_id">id of workout being added to favorites</param>
        /// <returns>Page that called the action</returns>
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
                    if (this.Request.UrlReferrer != null) 
                    {
                        string url = this.Request.UrlReferrer.PathAndQuery;
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "FavoriteWorkouts");
                    }
                    
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

        /// <summary>
        /// Removes workout with passed in id from favorite list of currently logged in user
        /// </summary>
        /// <param name="workout_id">id of workout being removed from favorites</param>
        /// <returns>Page that called the action</returns>
        [Authorize]
        public ActionResult RemoveWorkoutFromFavorites(int? workout_id)
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
                if (this.Request.UrlReferrer != null)
                {
                    string url = this.Request.UrlReferrer.PathAndQuery;
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("Index", "FavoriteWorkouts");
                }
            }
            catch (Exception ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the requested workout from favorites."));
            }

        }



        //---------------------Private methods-------------------------


        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="fav_workouts">The base user_favorite_workout query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<user_favorite_workout> doSearch(IQueryable<user_favorite_workout> fav_workouts, WorkoutSearch search, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy))
            {
                search = SessionVariableManager.setSearchFromSession(Session, search);
            }
            else SessionVariableManager.setSessionFromSearch(Session, search);

            if (!String.IsNullOrEmpty(search.name)) fav_workouts = fav_workouts.Where(w => w.workout.name.Contains(search.name));
            if (!String.IsNullOrEmpty(search.category)) fav_workouts = fav_workouts.Where(w => w.workout.category.name.Contains(search.category));
            if (!String.IsNullOrEmpty(search.username)) fav_workouts = fav_workouts.Where(w => w.workout.user.username.Contains(search.username));
            if (!String.IsNullOrEmpty(search.dateAdded))
            {
                string[] dateArrayString = search.dateAdded.Split('-');
                int year = Convert.ToInt16(dateArrayString[0]);
                int month = Convert.ToInt16(dateArrayString[1]);
                int day = Convert.ToInt16(dateArrayString[2]);

                fav_workouts = fav_workouts.Where(w =>
                    w.workout.created_at.Year == year &&
                    w.workout.created_at.Month == month &&
                    w.workout.created_at.Day == day);
            }
            return fav_workouts;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="fav_workouts">The base user_favorite_workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<user_favorite_workout> doSort(IQueryable<user_favorite_workout> fav_workouts, string sortBy)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                SessionVariableManager.setSessionFromSort(Session, sortBy);
            }
            else
            {
                sortBy = SessionVariableManager.setSortFromSession(Session, sortBy);
            }

            ViewBag.NameSortParam = (sortBy == "name") ? "name_desc" : "name";
            ViewBag.DateSortParam = (sortBy == "date") ? "date_desc" : "date";
            ViewBag.CategorySortParam = (sortBy == "category") ? "category_desc" : "category";
            ViewBag.UserSortParam = (sortBy == "user") ? "user_desc" : "user";

            switch (sortBy)
            {
                case "name_desc":
                    fav_workouts = fav_workouts.OrderByDescending(w => w.workout.name);
                    break;
                case "date":
                    fav_workouts = fav_workouts.OrderBy(w => w.workout.created_at);
                    break;
                case "date_desc":
                    fav_workouts = fav_workouts.OrderByDescending(w => w.workout.created_at);
                    break;
                case "category":
                    fav_workouts = fav_workouts.OrderBy(w => w.workout.category.name);
                    break;
                case "category_desc":
                    fav_workouts = fav_workouts.OrderByDescending(w => w.workout.category.name);
                    break;
                case "user":
                    fav_workouts = fav_workouts.OrderBy(w => w.workout.user.username);
                    break;
                case "user_desc":
                    fav_workouts = fav_workouts.OrderByDescending(w => w.workout.user.username);
                    break;
                default:
                    fav_workouts = fav_workouts.OrderBy(w => w.workout.name);
                    break;
            }

            return fav_workouts;
        }
	}
}