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

        /// <summary>
        /// Constructor that takes the db as a parmeter and calls the base contructor
        /// with it. 
        /// </summary>
        /// <param name="context">The db to use</param>
        public AdminWorkoutsController(masterEntities context)
            : base(context)
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
        }
        

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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "A workout to view was not specified"));
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "That workout could not be found or does not exist"));
            }
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
        public ActionResult New([Bind(Include = "id,name,description,category_id,created_by_user_id,created_at,timestamp,workout_exercise")] workout workout)
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
                    return RedirectToAction("Details", "AdminWorkouts", new { id = workout.id });
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


        // GET: AdminWorkouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "A workout to edit was not specified"));
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "That workout could not be found or does not exist"));
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
            catch (DbUpdateConcurrencyException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit workout as another admin may have already update this workout"));
            }
            catch (DbUpdateException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit workout."));
            }
            catch (Exception)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the workout."));
            }

        }

        // GET: AdminWorkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "A workout to delete was not specified"));
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "That workout could not be found or does not exist"));
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
                if (state == EntityState.Detached)
                {
                    db.workouts.Remove(workout);
                }
                else
                {
                    entry.OriginalValues["timestamp"] = workout.timestamp;
                    db.workouts.Remove(oldWorkout);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout as another admin may have modified this workout"));
            }
            catch (DbUpdateException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout as it may be referenced by another item."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminWorkouts", "DeleteConfirmed");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout."));
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
