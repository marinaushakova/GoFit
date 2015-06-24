using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines the home page functionality
    /// </summary>
    public class HomeController : Controller
    {
        private masterEntities db;
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
        /// Constructor to create the default db context
        /// </summary>
        public HomeController()
        {
            db = new masterEntities();
            pageSize = PAGE_SIZE;
            helper = new ControllerHelpers(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public HomeController(masterEntities context)
        {
            db = context;
            pageSize = PAGE_SIZE;
            helper = new ControllerHelpers(db);
        }

        /// <summary>
        /// Getter/setter for the pageSize instance variable.
        /// Should only be changed for testing purposes
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Returns a list of workouts from the DB
        /// GET: /home/
        /// </summary>
        /// <param name="sortBy">String parameter telling the controller how to sore the workouts</param>
        /// <param name="page">The request page in the list of workouts</param>
        /// <returns>A list of workouts from the DB</returns>
        [AllowAnonymous]
        public ActionResult Index(string sortBy, int? page, WorkoutSearch workoutSearch)
        {
            var workouts = from w in db.workouts select w;

            workouts = this.doSearch(workouts, workoutSearch, sortBy, page);
            workouts = this.doSort(workouts, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", workouts.ToPagedList(pageNumber, pageSize));
            return view;
        }

        /// <summary>
        /// Gets the view for a single workout, showing its exercise content and allowing user
        /// to mark their progress on the workout if they have added it to their workouts
        /// </summary>
        /// <param name="workoutId">The workout id</param>
        /// <returns>The workout's view</returns>
        [AllowAnonymous]
        public ActionResult Details(int? workoutId)
        {
            workout workout;
            if (workoutId == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Workout could not be retrieved with given parameters."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout = db.workouts.Find(workoutId);
            if (workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Could not find the specified workout."));
                //return HttpNotFound("Workout not found");
            }
            return View(workout);
        }

        /// <summary>
        /// Passes a list of categories to populate combobox on Create new workout page to the view 
        /// </summary>
        /// <returns>Create workout view</returns>
        [Authorize]
        public ActionResult Create()
        {
            var query = db.categories.Select(c => new { c.id, c.name });
            ViewBag.Categories = new SelectList(query.AsEnumerable(), "id", "name");

            return View();
        }

        /// <summary>
        /// Adds new workout to the database
        /// </summary>
        /// <param name="workout">Workout being added to the database</param>
        /// <returns>AddExerciseToWorkout view if success, create workout view if not</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Create(workout workout)
        {
            if (workout == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Workout could not be created. Please try again."));
            }
            
            workout.timestamp = DateTime.Now;
            workout.created_at = DateTime.Now;
            workout.created_by_user_id = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
            if (workout.created_by_user_id == -1)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Workout could not be found."));
                //return HttpNotFound("created_by_user_id not found");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.workouts.Add(workout);
                    db.SaveChanges();
                    return RedirectToAction("AddExerciseToWorkout", "Home", new { id = workout.id });
                }
                catch (Exception ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the requested workout."));
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to create new workout");
                }
            }
            else
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to create new workout");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not create the workout with the given values."));
            }
            

        }

        /// <summary>
        /// Returns an add exercise to current workout view
        /// </summary>
        /// <param name="id">workout id</param>
        /// <returns>AddExerciseToWorkout view </returns>
        [HttpGet]
        [Authorize]
        public ActionResult AddExerciseToWorkout(int? id)
        {
            if (id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No exercise to add was specified."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ViewBag.Workout = db.workouts.Find(id);
                if (ViewBag.Workout == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Workout could not be found."));
                    //return HttpNotFound("Workout not found");
                }
                // Workout id is stored in session to be accessed from AddExerciseToWorkout post method
                Session["workout_id"] = id;
                // ViewBag.Exercises stores a list of exercises to populate combobox
                var query = db.exercises.Select(ex => new { ex.id, ex.name });
                ViewBag.Exercises = new SelectList(query.AsEnumerable(), "id", "name");
            }
            
            return View();
        }

        /// <summary>
        /// Adds exercise to current workout
        /// </summary>
        /// <param name="w_ex">workout_exercise object being added to db</param>
        /// <returns>AddExerciseToWorkout</returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddExerciseToWorkout(workout_exercise w_ex)
        {
            if (w_ex == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No exercise to add was specified."));
            }

            w_ex.workout_id = (int)Session["workout_id"];
            w_ex.timestamp = DateTime.Now;
            w_ex.position = db.workout_exercise.Where(m => m.workout_id == w_ex.workout_id).Count() + 1;
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.workout_exercise.Add(w_ex);
                    db.SaveChanges();
                    return RedirectToAction("AddExerciseToWorkout", "Home", new { id = w_ex.workout_id });
                }
                catch
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to add an exercise to workout");
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Exercise could not be added to the workout."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid exercise."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to add an exercise to workout");
            }

        }

        /// <summ/ary>
        /// Gets measure for given exercise
        /// </summary>
        /// <param name="ex_id">Exercise id</param>
        /// <returns>Measure name to be used in javascript on AddExerciseToWorkout page</returns>
        [HttpGet]
        [Authorize]
        public ActionResult GetMeasure(int? ex_id)
        {
            if (ex_id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No exercise to get a measure for was specified."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var measure = db.exercises.Find(ex_id).type.measure;
            if (ViewBag.Workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Exercise could not be found."));
                //return HttpNotFound("Measure not found");
            }
            return Json(measure, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Gets list of exercises that are in given workout
        /// </summary>
        /// <param name="id">workout id</param>
        /// <returns>List of exercises of workout with passed id</returns>
        [Authorize]
        public PartialViewResult ExerciseList(int? id)
        {
            var exerciseList = db.workout_exercise.Where(m => m.workout_id == id).ToList();
            return PartialView(exerciseList);
        }

        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<workout> doSearch(IQueryable<workout> workouts, WorkoutSearch search, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy))
            {
                search = setSearchFromSession(search);
            }
            else setSessionFromSearch(search);

            if (!String.IsNullOrEmpty(search.name)) workouts = workouts.Where(w => w.name.Contains(search.name));
            if (!String.IsNullOrEmpty(search.category)) workouts = workouts.Where(w => w.category.name.Contains(search.category));
            if (!String.IsNullOrEmpty(search.username)) workouts = workouts.Where(w => w.user.username.Contains(search.username));
            if (!String.IsNullOrEmpty(search.dateAdded))
            {
                string[] dateArrayString = search.dateAdded.Split('-');
                int year = Convert.ToInt16(dateArrayString[0]);
                int month = Convert.ToInt16(dateArrayString[1]);
                int day = Convert.ToInt16(dateArrayString[2]);

                workouts = workouts.Where(w =>
                    w.created_at.Year == year &&
                    w.created_at.Month == month &&
                    w.created_at.Day == day);
            }
            return workouts;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<workout> doSort(IQueryable<workout> workouts, string sortBy)
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
                case "name":
                    workouts = workouts.OrderBy(w => w.name);
                    break;
                case "name_desc":
                    workouts = workouts.OrderByDescending(w => w.name);
                    break;
                case "date":
                    workouts = workouts.OrderBy(w => w.created_at);
                    break;
                case "date_desc":
                    workouts = workouts.OrderByDescending(w => w.created_at);
                    break;
                case "category":
                    workouts = workouts.OrderBy(w => w.category.name);
                    break;
                case "category_desc":
                    workouts = workouts.OrderByDescending(w => w.category.name);
                    break;
                case "user":
                    workouts = workouts.OrderBy(w => w.user.username);
                    break;
                case "user_desc":
                    workouts = workouts.OrderByDescending(w => w.user.username);
                    break;
                default:
                    workouts = workouts.OrderByDescending(w => w.name);
                    break;
            }

            return workouts;
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
                if (!String.IsNullOrEmpty(Session["NameSearchParam"] as String)) search.name = Session["NameSearchParam"] as String;
                if (!String.IsNullOrEmpty(Session["CategorySearchParam"] as String)) search.category = Session["CategorySearchParam"] as String;
                if (!String.IsNullOrEmpty(Session["UserSearchParam"] as String)) search.username = Session["UserSearchParam"] as String;
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