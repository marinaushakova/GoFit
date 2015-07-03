using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.Net.Http;
using GoFit.Controllers.ControllerHelpers;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines MyWorkouts page funtionality
    /// </summary>
    public class MyWorkoutsController : GoFitBaseController
    {

        private masterEntities db;
        private int currUserId;
        private const int PAGE_SIZE = 10;
        private UserAccess userAccess;

        /// <summary>
        /// Getter/setter for the pageSize instance variable
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public MyWorkoutsController() : base ()
        {
            pageSize = PAGE_SIZE;
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public MyWorkoutsController(masterEntities context) : base(context)
        {
            pageSize = PAGE_SIZE;
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        //
        // GET: /MyWorkouts/
        /// <summary>
        /// Returns the list of My Workouts for currently logged in user
        /// </summary>
        /// <param name="filterString">Parameter is used to filter my workouts list</param>
        /// <param name="sortBy">String parameter telling the controller how to sort the workouts</param>
        /// <param name="page">The request page in the list of workouts</param>
        /// <param name="workoutSearch">Search parameter</param>
        /// <returns>A list of users workouts from DB</returns>
        [Authorize]
        public ActionResult Index(string filterString, string sortBy, int? page, WorkoutSearch workoutSearch)
        {
            currUserId = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
            var user_workouts = from w in db.user_workout where w.user_id == currUserId select w;

            user_workouts = this.doFilter(user_workouts, filterString);
            user_workouts = this.doSearch(user_workouts, workoutSearch, filterString, sortBy, page);
            user_workouts = this.doSort(user_workouts, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", user_workouts.ToPagedList(pageNumber, pageSize));
            return view;
        }

        /// <summary>
        /// Shows an individual user workout
        /// </summary>
        /// <param name="user_workout_id">The user workout to show</param>
        /// <returns>The user workout view</returns>
        [Authorize]
        public ActionResult Details(int? user_workout_id)
        {
            workout workout;

            if (user_workout_id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Workout could not be retrieved with given parameters."));
            }

            int userId = userAccess.getUserId(User.Identity.Name);
            user_workout myworkout = db.user_workout.Find(user_workout_id);

            if (myworkout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Your workout could not be found."));
            }
            else
            {
                workout = myworkout.workout;
                ViewBag.myWorkoutId = myworkout.id;
                ViewBag.numExercisesCompleted = myworkout.number_of_ex_completed;
                ViewBag.isMyWorkout = true;

                ViewBag.timestampString = timestampByteArrToString(myworkout.timestamp);
                return View(workout);
            }
        }

        /// <summary>
        /// Marks an exercise or exercises of a user_workout as completed
        /// </summary>
        /// <param name="my_workout_id">the user_workout to mark exercises on</param>
        /// <param name="position">the position the user has marked</param>
        /// <returns>True if successful, false otherwise</returns>
        [HttpPost]
        [Authorize]
        public ActionResult MarkExercise(int my_workout_id, int position, string timestampString)
        {
            byte[] timestamp = timestampStringToByteArr(timestampString);
            user_workout userWorkout = new user_workout();
            userWorkout.id = my_workout_id;
            userWorkout.timestamp = timestamp;

            user_workout oldMyWorkout = db.user_workout.Find(my_workout_id);
            if (oldMyWorkout == null)
            {
                var result = new Dictionary<string, string>();
                result.Add("error", "true");
                result.Add("position", Convert.ToString(position));
                result.Add("message", "Failed to mark progress as the workout does not exist or may have been deleted");
                result.Add("code", "500");
                return Json(result);
            }
            try
            {
                var entry = db.Entry(oldMyWorkout);
                var state = entry.State;

                int totalNumExercises = oldMyWorkout.workout.workout_exercise.Count;
                if (position == 1 || oldMyWorkout.date_started == null)
                {
                    userWorkout.date_started = DateTime.Now;
                    if (position == totalNumExercises) userWorkout.date_finished = DateTime.Now;
                }
                else if (position == totalNumExercises)
                {
                    userWorkout.date_finished = DateTime.Now;
                }
                userWorkout.number_of_ex_completed = position;

                if (state == EntityState.Detached)
                {
                    db.Entry(userWorkout).State = EntityState.Modified;
                }
                else 
                {
                    entry.OriginalValues["timestamp"] = userWorkout.timestamp;                        
                    if (userWorkout.date_started != null) entry.CurrentValues["date_started"] = userWorkout.date_started;
                    if (userWorkout.date_finished != null) entry.CurrentValues["date_finished"] = userWorkout.date_finished;
                    entry.CurrentValues["number_of_ex_completed"] = userWorkout.number_of_ex_completed;
                }
                db.SaveChanges();

                user_workout updatedMyWorkout = db.user_workout.Find(my_workout_id);
                string newTimestamp = timestampByteArrToString(updatedMyWorkout.timestamp);
                var result = new Dictionary<string, string>();
                result.Add("success", "true");
                result.Add("timestampString", newTimestamp);
                result.Add("error", "false");
                return Json(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var result = new Dictionary<string, string>();
                result.Add("error", "true");
                result.Add("position", Convert.ToString(position));
                result.Add("message", "Failed to mark progress as the workout may have already been updated");
                result.Add("code", "500");
                return Json(result);
            }
            catch (DbUpdateException ex)
            {
                var result = new Dictionary<string, string>();
                result.Add("error", "true");
                result.Add("position", Convert.ToString(position));
                result.Add("message", "Failed to mark progress as the workout may have already been updated");
                result.Add("code", "500");
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new Dictionary<string, string>();
                result.Add("error", "true");
                result.Add("position", Convert.ToString(position));
                result.Add("message", "Failed to mark workout progress");
                result.Add("code", "500");
                return Json(result);
            }
        }

        /// <summary>
        /// Adds a workout to the user_workout table
        /// </summary>
        /// <param name="userWorkout">The user_workout object to add</param>
        /// <returns>The detail view for the added workout</returns>
        [Authorize]
        public ActionResult AddToMyWorkouts(user_workout userWorkout)
        {
            int userID = userAccess.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }
            userWorkout.user_id = userID;
            userWorkout.number_of_ex_completed = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    db.user_workout.Add(userWorkout);
                    db.SaveChanges();
                    return RedirectToAction("Details", "MyWorkouts", new { user_workout_id = userWorkout.id });
                }
                catch (Exception ex) 
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to add the requested workout to user workouts."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Failed to add the requested workout to user workouts due to an invalid entry."));
            }
        }

        /// <summary>
        /// Deletes the given workout from the users workouts
        /// </summary>
        /// <param name="userWorkout_id">The id of the instance of user_workout to be deleted</param>
        /// <returns>The MyWorkouts index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteFromMyWorkouts([Bind(Include = "id,timestamp")] user_workout userWorkout)
        {
            int userID = userAccess.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }
            try
            {
                user_workout oldUserWorkout = db.user_workout.Find(userWorkout.id);
                if (oldUserWorkout == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The workout does not exist or has already been deleted"));
                }
                var entry = db.Entry(oldUserWorkout);
                var state = entry.State;
                if (state == EntityState.Detached)
                {
                    db.user_workout.Remove(userWorkout);
                }
                else
                {
                    entry.OriginalValues["timestamp"] = userWorkout.timestamp;
                    db.user_workout.Remove(oldUserWorkout);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "MyWorkouts");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to remove workout as another user may have updated this workout"));
            }
            catch (DbUpdateException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to remove the workout."));
            }
            catch (Exception ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the requested workout from MyWorkouts."));
            }
            
        }

        /// <summary>
        /// Converts a space separated string of bytes into a byte array. Used 
        /// to pass the timestamp via json
        /// </summary>
        /// <param name="timestampString">The timestamp in space separated string format</param>
        /// <returns>The timestamp in byte[] format</returns>
        private byte[] timestampStringToByteArr(string timestampString)
        {
            byte[] timestamp = new byte[8];
            string[] sep = new string[1];
            sep[0] = " ";
            if (!String.IsNullOrEmpty(timestampString))
            {
                string[] split = timestampString.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < timestamp.Count(); i++)
                {
                    timestamp[i] = Convert.ToByte(split[i]);
                }
            }
            return timestamp;
        }

        /// <summary>
        /// Takes a byte[] and converts it to a space separated string
        /// </summary>
        /// <param name="timestamp">The byte[] timestamp</param>
        /// <returns>The space separated string timestamp</returns>
        private string timestampByteArrToString(byte[] timestamp)
        {
            string stringTimestamp = "";
            for (var i = 0; i < timestamp.Count(); i++)
            {
                stringTimestamp += timestamp[i];
                stringTimestamp += " ";
            }
            return stringTimestamp;
        }

        /// <summary>
        /// Filters user_workouts by their date started and date finished attributes
        /// </summary>
        /// <param name="user_workouts">A given users workouts </param>
        /// <param name="filterString">The string to filter by. One of "in_progress",
        /// "not_started", or "completed"</param>
        /// <returns>The filtered user_workouts list</returns>
        private IQueryable<user_workout> doFilter(IQueryable<user_workout> user_workouts, String filterString)
        {

            if (!String.IsNullOrEmpty(filterString))
            {
                ViewBag.FilterParam = filterString;
            }
            else ViewBag.FilterParam = "";

            switch (filterString)
            {
                case "in_progress":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started != null && w.date_finished == null
                                    select w;
                    break;
                case "not_started":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started == null && w.date_finished == null
                                    select w;
                    break;
                case "completed":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started != null && w.date_finished != null
                                    select w;
                    break;
                default:
                    break;
            }

            return user_workouts;
        }


        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<user_workout> doSearch(IQueryable<user_workout> user_workouts, WorkoutSearch search, String filterString, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy) || !String.IsNullOrEmpty(filterString))
            {
                search = SessionVariableManager.setSearchFromSession(Session, search);
            }
            else SessionVariableManager.setSessionFromSearch(Session, search);

            if (!String.IsNullOrEmpty(search.name)) user_workouts = user_workouts.Where(w => w.workout.name.Contains(search.name));
            if (!String.IsNullOrEmpty(search.category)) user_workouts = user_workouts.Where(w => w.workout.category.name.Contains(search.category));
            if (!String.IsNullOrEmpty(search.username)) user_workouts = user_workouts.Where(w => w.workout.user.username.Contains(search.username));
            if (!String.IsNullOrEmpty(search.dateAdded))
            {
                string[] dateArrayString = search.dateAdded.Split('-');
                int year = Convert.ToInt16(dateArrayString[0]);
                int month = Convert.ToInt16(dateArrayString[1]);
                int day = Convert.ToInt16(dateArrayString[2]);

                user_workouts = user_workouts.Where(w =>
                    w.workout.created_at.Year == year &&
                    w.workout.created_at.Month == month &&
                    w.workout.created_at.Day == day);
            }
            return user_workouts;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<user_workout> doSort(IQueryable<user_workout> user_workouts, string sortBy)
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
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.name);
                    break;
                case "date":
                    user_workouts = user_workouts.OrderBy(w => w.workout.created_at);
                    break;
                case "date_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.created_at);
                    break;
                case "category":
                    user_workouts = user_workouts.OrderBy(w => w.workout.category.name);
                    break;
                case "category_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.category.name);
                    break;
                case "user":
                    user_workouts = user_workouts.OrderBy(w => w.workout.user.username);
                    break;
                case "user_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.user.username);
                    break;
                default:
                    user_workouts = user_workouts.OrderBy(w => w.workout.name);
                    break;
            }

            return user_workouts;
        }
	}
}