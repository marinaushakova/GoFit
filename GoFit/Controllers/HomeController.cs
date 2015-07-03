using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using PagedList;
using System.Net;
using System.Data.Entity;
using GoFit.Controllers.ControllerHelpers;
using System.Data.Entity.Infrastructure;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines the home page functionality
    /// </summary>
    public class HomeController : GoFitBaseController
    {
        private masterEntities db;
        private const int PAGE_SIZE = 10;
        private UserAccess userAccess;

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public HomeController() : base()
        {
            pageSize = PAGE_SIZE;
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public HomeController(masterEntities context) : base(context)
        {
            pageSize = PAGE_SIZE;
            db = this.getDB();
            userAccess = new UserAccess(db);
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

            workouts = WorkoutSortSearch.doSearch(workouts, workoutSearch, sortBy, page, Session, ViewBag);
            workouts = WorkoutSortSearch.doSort(workouts, sortBy, Session, ViewBag);

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
            }
            workout = db.workouts.Find(workoutId);
            if (workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Could not find the specified workout."));
            }
            //session is used in AddComment method
            Session["workout_id"] = workoutId;
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

            return View("Create");
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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Workout could not be created. Please try again."));
            }
            
            workout.created_at = DateTime.Now;
            var user = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault();
            if (user == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "No user could be associated with the workout being created"));
            }
            else workout.created_by_user_id = user.id;

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
                }
            }
            else
            {
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
            }
            else
            {
                ViewBag.Workout = db.workouts.Find(id);
                if (ViewBag.Workout == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Workout to add exercise to could not be found."));
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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No exercise to add was specified."));
            }

            if (Session["workout_id"] != null) w_ex.workout_id = (int)Session["workout_id"];

            if (w_ex.position == 0)
            {
                var exercisesInWorkout = db.workout_exercise.Where(m => m.workout_id == w_ex.workout_id);
                int exerciseCount = exercisesInWorkout.Count();
                w_ex.position = exerciseCount + 1;
            }
            
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
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Exercise could not be added to the workout."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid exercise."));
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
            }
            var exercise = db.exercises.Find(ex_id);
            if (exercise == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Exercise could not be found."));
            }
            var measure = exercise.type.measure;
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
        /// Calls add comment partial view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddComment()
        {
            return PartialView();
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
                    return new PartialViewResult();
                }
                catch
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Comment could not be added."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Invalid comment."));
            }     
        }
    }
}