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
            if (workoutId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = helper.getUserId(User.Identity.Name);
            user_workout myworkout = db.user_workout.Where(w =>
                w.workout_id == workoutId &&
                w.user_id == userId).FirstOrDefault();

            if (myworkout == null) workout = db.workouts.Find(workoutId);
            else
            {
                workout = myworkout.workout;
                ViewBag.myWorkoutId = myworkout.id;
                ViewBag.numExercisesCompleted = myworkout.number_of_ex_completed;
                ViewBag.isMyWorkout = true;
            }
            if (workout == null) return HttpNotFound();
            return View(workout);
        }

        /// <summary>
        /// Returns a list of categories to populate combobox on Create new workout page
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
            if (ModelState.IsValid)
            {
                workout.timestamp = DateTime.Now;
                workout.created_at = DateTime.Now;
                workout.created_by_user_id = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
                db.workouts.Add(workout);
                db.SaveChanges();

                return RedirectToAction("AddExerciseToWorkout", new { id = workout.id });
            }

            return View();

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

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.Workout = db.workouts.Find(id);
            Session["workout_id"] = id;
            var query = db.exercises.Select(ex => new { ex.id, ex.name });
            ViewBag.Exercises = new SelectList(query.AsEnumerable(), "id", "name");

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

            if (ModelState.IsValid)
            {
                w_ex.workout_id = (int)Session["workout_id"];
                w_ex.timestamp = DateTime.Now;
                w_ex.position = db.workout_exercise.Where(m => m.workout_id == w_ex.workout_id).Count() + 1;
                db.workout_exercise.Add(w_ex);
                db.SaveChanges();

                return RedirectToAction("AddExerciseToWorkout", new { id = w_ex.workout_id });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Gets measure for given exercise
        /// </summary>
        /// <param name="ex_id">Exercise id</param>
        /// <returns>Measure name to be used in javascript on AddExerciseToWorkout page</returns>
        [HttpGet]
        [Authorize]
        public ActionResult GetMeasure(int ex_id)
        {
            var measure = db.exercises.Find(ex_id).type.measure;
            return Json(measure, JsonRequestBehavior.AllowGet);
        }

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
                    return Json(true);
                }
                else if (position == myWorkout.workout.workout_exercise.Count)
                {
                    myWorkout.number_of_ex_completed = position;
                    myWorkout.date_finished = DateTime.Now;
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    myWorkout.number_of_ex_completed = position;
                    db.SaveChanges();
                    return Json(true);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        /// <summary>
        /// Gets list of exercises that are in given workout
        /// </summary>
        /// <param name="id">workout id</param>
        /// <returns>List of exercises of workout with passed id</returns>
        [Authorize]
        public PartialViewResult ExerciseList(int id)
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