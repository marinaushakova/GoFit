using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using PagedList;
using GoFit.Controllers.ControllerHelpers;
using System.Data.Entity.Infrastructure;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminWorkoutsController : GoFitBaseController
    {
        private masterEntities db;
        private const int PAGE_SIZE = 10;

        /// <summary>
        /// Getter/setter for the pageSize instance variable
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public AdminWorkoutsController() : base()
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
        }

        ///// <summary>
        ///// Returns an add exercise to current workout view
        ///// </summary>
        ///// <param name="id">workout id</param>
        ///// <returns>AddExerciseToWorkout view </returns>
        //[HttpGet]
        //public ActionResult AddExerciseToWorkout(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No exercise to add was specified."));
        //    }
        //    else
        //    {
        //        ViewBag.Workout = db.workouts.Find(id);
        //        if (ViewBag.Workout == null)
        //        {
        //            return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Workout to add exercise to could not be found."));
        //        }
        //        // Workout id is stored in session to be accessed from AddExerciseToWorkout post method
        //        Session["workout_id"] = id;
        //        // ViewBag.Exercises stores a list of exercises to populate combobox
        //        var query = db.exercises.Select(ex => new { ex.id, ex.name });
        //        ViewBag.Exercises = new SelectList(query.AsEnumerable(), "id", "name");
        //    }

        //    return View();
        //}

        ///// <summary>
        ///// Adds exercise to current workout
        ///// </summary>
        ///// <param name="w_ex">workout_exercise object being added to db</param>
        ///// <returns>AddExerciseToWorkout</returns>
        //[HttpPost]
        //public ActionResult AddExerciseToWorkout(workout_exercise w_ex)
        //{
        //    if (w_ex == null)
        //    {
        //        return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No exercise to add was specified."));
        //    }

        //    if (Session["workout_id"] != null) w_ex.workout_id = (int)Session["workout_id"];

        //    if (w_ex.position == 0)
        //    {
        //        var exercisesInWorkout = db.workout_exercise.Where(m => m.workout_id == w_ex.workout_id);
        //        int exerciseCount = exercisesInWorkout.Count();
        //        w_ex.position = exerciseCount + 1;
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.workout_exercise.Add(w_ex);
        //            db.SaveChanges();
        //            return RedirectToAction("AddExerciseToWorkout", "Home", new { id = w_ex.workout_id });
        //        }
        //        catch
        //        {
        //            return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Exercise could not be added to the workout."));
        //        }
        //    }
        //    else
        //    {
        //        return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid exercise."));
        //    }

        //}

        // GET: AdminWorkouts
        public ActionResult Index(string filterString, string sortBy, int? page, WorkoutSearch workoutSearch)
        {
            var workouts = from w in db.workouts select w;

            workouts = WorkoutSortSearch.doSearch(workouts, workoutSearch, sortBy, page, Session, ViewBag);
            workouts = WorkoutSortSearch.doSort(workouts, sortBy, Session, ViewBag);

            int pageNumber = (page ?? 1);
            var view = View("Index", workouts.ToPagedList(pageNumber, pageSize));
            return view;
        }

        // GET: AdminWorkouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: AdminWorkouts/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "id", "name");
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: AdminWorkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,category_id,created_by_user_id,created_at,timestamp")] workout workout)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    workout.created_at = DateTime.Now;
                    workout.created_by_user_id = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
                    db.workouts.Add(workout);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
                ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
                return View(workout);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminWorkouts", "Create");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the workout."));
            }

        }

        // GET: AdminWorkouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
            return View(workout);
        }

        // POST: AdminWorkouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,category_id,created_by_user_id,created_at,timestamp")] workout workout)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldWorkout = db.workouts.Find(workout.id);
                    if (oldWorkout == null)
                    {
                        return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The workout does not exist or has already been deleted"));
                    }
                    var entry = db.Entry(oldWorkout);
                    var state = entry.State;
                    if (state == EntityState.Detached)
                    {
                        db.Entry(workout).State = EntityState.Modified;
                    }
                    else
                    {
                        entry.OriginalValues["timestamp"] = workout.timestamp;
                        entry.CurrentValues.SetValues(workout);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
                ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
                return View(workout);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit workout as another admin may have already update this workout"));
            }
            catch (DbUpdateException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit workout."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminWorkouts", "Edit");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the workout."));
            }

        }

        // GET: AdminWorkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: AdminWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "id,timestamp")] workout workout)
        {
            try
            {
                workout oldWorkout = db.workouts.Find(workout.id);
                if (oldWorkout == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The workout does not exist or has already been deleted"));
                }
                var entry = db.Entry(oldWorkout);
                var state = entry.State;
                entry.OriginalValues["timestamp"] = workout.timestamp;
                db.workouts.Remove(oldWorkout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout as another admin may have modified this workout"));
            }
            catch (DbUpdateException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminWorkouts", "DeleteConfirmed");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout as it may be referenced in the database."));
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
