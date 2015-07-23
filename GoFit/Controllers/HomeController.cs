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
    /// 
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
            // Checks if workout is in Favorite list
            int userID = userAccess.getUserId(User.Identity.Name);
            user_favorite_workout fav_workout = db.user_favorite_workout
                                                    .Where(m => m.workout_id == (int)workoutId && 
                                                            m.user_id == userID).FirstOrDefault();
            ViewBag.IsFavorite = (fav_workout == null) ? false : true;

            return View(workout);
        }

        /// <summary>
        /// Passes a list of categories and list of exercises 
        /// to populate comboboxes on New workout page 
        /// </summary>
        /// <returns>New workout view</returns>
        [Authorize]
        public ActionResult New()
        {
            var workout = new workout();
            workout.CreateWorkoutExercise();

            //var query = db.exercises.Select(ex => new { ex.id, ex.name });
            var query = from ex in db.exercises select new { id = ex.id, name = ex.name + " - " + ex.type.measure };
            ViewBag.Exercises = new SelectList(query.AsEnumerable(), "id", "name");

            query = db.categories.Select(c => new { c.id, c.name });
            ViewBag.Categories = new SelectList(query.AsEnumerable(), "id", "name");

            return View(workout);
        }

        /// <summary>
        /// Adds new workout with workout_exercises to the database
        /// </summary>
        /// <param name="workout">Workout being added to the database with list of workout_exercises</param>
        /// <returns>Workout Details view if success, error view if not</returns>
        [Authorize]
        [HttpPost]
        public ActionResult New(workout workout)
        {
            if (workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Workout could not be created with given parameters."));
            }

            workout.created_at = DateTime.Now;
            var user = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault();
            if (user == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "No user could be associated with the workout being created"));
            }
            else workout.created_by_user_id = user.id;
            var position = 1;
            
            if (ModelState.IsValid)
            {
                try 
                { 
                    foreach (workout_exercise w_ex in workout.workout_exercise.ToList())
                    {
                        w_ex.position = position;
                        position++;
                    }
                    db.workouts.Add(workout);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Home", new { workoutId = workout.id });
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
        
    }
}