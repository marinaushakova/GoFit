using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.Net.Http;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines MyWorkouts page funtionality
    /// </summary>
    public class MyWorkoutsController : Controller
    {

        private masterEntities db;
        private int currUserId;
        private const int PAGE_SIZE = 10;
        private ControllerHelpers helper;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;
            }

            // Redirect admins to admin home page upon authorization
            if (isAdmin == 1)
            {
                filterContext.Result = new RedirectResult("/AdminHome/Index");
            }
        }


        /// <summary>
        /// Getter/setter for the pageSize instance variable
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public MyWorkoutsController()
        {
            db = new masterEntities();
            pageSize = PAGE_SIZE;
            helper = new ControllerHelpers(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public MyWorkoutsController(masterEntities context)
        {
            db = context;
            pageSize = PAGE_SIZE;
            helper = new ControllerHelpers(db);
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
        public ActionResult Details(int user_workout_id)
        {
            workout workout;

            int userId = helper.getUserId(User.Identity.Name);
            user_workout myworkout = db.user_workout.Find(user_workout_id);

            if (myworkout == null) return new HttpNotFoundResult("Workout not found");
            else
            {
                workout = myworkout.workout;
                ViewBag.myWorkoutId = myworkout.id;
                ViewBag.numExercisesCompleted = myworkout.number_of_ex_completed;
                ViewBag.isMyWorkout = true;
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
        public ActionResult MarkExercise(int my_workout_id, int position)
        {

            user_workout myWorkout = db.user_workout.Find(my_workout_id);
            if (myWorkout == null) return new HttpNotFoundResult("Unable to find the given user workout");
            try
            {
                if (position == 1 || myWorkout.date_started == null)
                {
                    myWorkout.number_of_ex_completed = position;
                    myWorkout.date_started = DateTime.Now;
                    db.SaveChanges();
                }
                else if (position == myWorkout.workout.workout_exercise.Count)
                {
                    myWorkout.number_of_ex_completed = position;
                    myWorkout.date_finished = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    myWorkout.number_of_ex_completed = position;
                    db.SaveChanges();
                }
                var result = new Dictionary<string, string>();
                result.Add("result", "true");
                result.Add("error", "false");
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new Dictionary<string, string>();
                result.Add("error", "true");
                result.Add("result", "false");
                result.Add("message", "Failed to mark wokrout progress");
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
            int userID = helper.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }
            userWorkout.user_id = userID;
            userWorkout.number_of_ex_completed = 0;
            // Not sure why the timestamp is automatically set to an invalid value
            // This is a temporary workaround
            //userWorkout.timestamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                // TODO: Validate user_workout object
                // TODO: Add error handling
                try
                {
                    db.user_workout.Add(userWorkout);
                    db.SaveChanges();
                    return RedirectToAction("Details", "MyWorkouts", new { user_workout_id = userWorkout.id });
                }
                catch (Exception ex) 
                {
                    var err = new HandleErrorInfo(ex, "MyWorkouts", "AddToMyWorkouts");
                    //return View("Error", err);
                    //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to add the requested workout to user workouts.");
                    return View("Error", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to add the requested workout to user workouts."));
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to add this workout to user MyWorkouts page");
            }
        }

        
        [Authorize]
        public ActionResult DeleteFromMyWorkouts(int? userWorkout_id)
        {
            int userID = helper.getUserId(User.Identity.Name);
            if (userID == -1)
            {
                return View();
            }
            try
            {
                user_workout user_workout = db.user_workout.Find(userWorkout_id);
                db.user_workout.Remove(user_workout);
                db.SaveChanges();
                return RedirectToAction("Index", "MyWorkouts");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to add this workout to user MyWorkouts page");
            }
            
        }

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
                search = setSearchFromSession(search);
            }
            else setSessionFromSearch(search);

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
                setSessionFromSort(sortBy);
            }
            else
            {
                sortBy = setSortFromSession(sortBy);
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

        /// <summary>
        /// Sets the WorkoutSearch object with the stored session search variables if they exist
        /// </summary>
        /// <param name="search">The WorkoutSearch object to set</param>
        /// <returns>The WorkoutSearch object set with the session search variables if the session exists, else the passed in WorkoutSearch object</returns>
        private WorkoutSearch setSearchFromSession(WorkoutSearch search)
        {
            if (Session != null)
            {
                search.name = Session["NameSearchParam"] as String;
                search.category = Session["CategorySearchParam"] as String;
                search.username = Session["UserSearchParam"] as String;
            }
            return search;
        }

        /// <summary>
        /// Sets the session search parameters based on the current search values
        /// </summary>
        /// <param name="search">The WorkoutSearch object containing the values to set in the session</param>
        private void setSessionFromSearch(WorkoutSearch search)
        {
            if (Session != null)
            {
                if (!String.IsNullOrEmpty(search.name)) Session["NameSearchParam"] = search.name;
                else Session["NameSearchParam"] = "";

                if (!String.IsNullOrEmpty(search.category)) Session["CategorySearchParam"] = search.category;
                else Session["CategorySearchParam"] = "";

                if (!String.IsNullOrEmpty(search.dateAdded))
                {
                    string[] dateArrayString = search.dateAdded.Split('-');
                    int year = Convert.ToInt16(dateArrayString[0]);
                    int month = Convert.ToInt16(dateArrayString[1]);
                    int day = Convert.ToInt16(dateArrayString[2]);
                }

                if (!String.IsNullOrEmpty(search.username)) Session["UsernameSearchParam"] = search.username;
                else Session["UsernameSearchParam"] = "";
            }
        }

        /// <summary>
        /// Sets the sortBy param to the session sort value if the session exists. 
        /// If the session does not exist the passed in sortBy param is returned. 
        /// </summary>
        /// <param name="sortBy">The current sort filter</param>
        /// <returns>The sort parameter set from the session else the original sort param</returns>
        private string setSortFromSession(string sortBy)
        {
            if (Session != null)
            {
                sortBy = Session["SortBy"] as String;
            }
            return sortBy;
        }

        /// <summary>
        /// Sets the session if it exists from the passed in sortBy string
        /// </summary>
        /// <param name="sortBy">The current sort filter</param>
        private void setSessionFromSort(string sortBy)
        {
            if (Session != null) Session["SortBy"] = sortBy;
        }

	}
}