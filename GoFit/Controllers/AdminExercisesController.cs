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
    public class AdminExercisesController : GoFitBaseController
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
        public AdminExercisesController() : base()
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
        }

        // GET: AdminExercises
        public ActionResult Index(string filterString, string sortBy, int? page, ExerciseSearch exerciseSearch)
        {
            var exercises = from e in db.exercises select e;
            exercises = this.doSearch(exercises, exerciseSearch, filterString, sortBy, page);
            exercises = this.doSort(exercises, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", exercises.ToPagedList(pageNumber, pageSize));
            return view;
        }

        // GET: AdminExercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercise exercise = db.exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: AdminExercises/Create
        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.types, "id", "name");
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: AdminExercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,type_id,created_by_user_id,created_at,link,description,timestamp,name")] exercise exercise)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    exercise.created_at = DateTime.Now;
                    exercise.created_by_user_id = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
                    db.exercises.Add(exercise);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.type_id = new SelectList(db.types, "id", "name", exercise.type_id);
                ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", exercise.created_by_user_id);
                return View(exercise);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminExercises", "Create");
                return View("_DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the exercise."));
            }

        }

        // GET: AdminExercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercise exercise = db.exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            ViewBag.type_id = new SelectList(db.types, "id", "name", exercise.type_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", exercise.created_by_user_id);
            return View(exercise);
        }

        // POST: AdminExercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,type_id,created_by_user_id,created_at,link,description,timestamp,name")] exercise exercise)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldExercise = db.exercises.Find(exercise.id);
                    if (oldExercise == null)
                    {
                        return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The exercise does not exist or has already been deleted"));
                    }
                    var entry = db.Entry(oldExercise);
                    var state = entry.State;
                    if (state == EntityState.Detached)
                    {
                        db.Entry(exercise).State = EntityState.Modified;
                    }
                    else
                    {
                        entry.OriginalValues["timestamp"] = exercise.timestamp;
                        entry.CurrentValues.SetValues(exercise);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.type_id = new SelectList(db.types, "id", "name", exercise.type_id);
                ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", exercise.created_by_user_id);
                return View(exercise);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit exercise as another admin may have already update this exercise"));
            }
            catch (DbUpdateException ex)
            {
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit exercise."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminExercises", "Edit");
                return View("_DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the exercise."));
            }

        }

        // GET: AdminExercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercise exercise = db.exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: AdminExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "id,timestamp")] exercise exercise)
        {
            try
            {
                exercise oldExercise = db.exercises.Find(exercise.id);
                if (oldExercise == null)
                {
                    return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The exercise does not exist or has already been deleted"));
                }
                var entry = db.Entry(oldExercise);
                var state = entry.State;
                entry.OriginalValues["timestamp"] = exercise.timestamp;
                db.exercises.Remove(oldExercise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the exercise as another admin may have modified it"));
            }
            catch (DbUpdateException ex)
            {
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the exercise."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminExercises", "DeleteConfirmed");
                return View("_DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the exercise as it may be referenced in the database."));
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

        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<exercise> doSearch(IQueryable<exercise> categories, ExerciseSearch search, String filterString, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy) || !String.IsNullOrEmpty(filterString))
            {
                search = setSearchFromSession(search);
            }
            else setSessionFromSearch(search);

            if (!String.IsNullOrEmpty(search.name)) categories = categories.Where(c => c.name.Contains(search.name));
            if (!String.IsNullOrEmpty(search.description)) categories = categories.Where(c => c.description.Contains(search.description));

            return categories;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<exercise> doSort(IQueryable<exercise> exercises, string sortBy)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                SessionVariableManager.setSessionFromSort(Session, sortBy);
            }
            else
            {
                sortBy = SessionVariableManager.setSortFromSession(Session, sortBy);
            }

            ViewBag.CreatedSortParam = (sortBy == "date") ? "date_desc" : "date";
            ViewBag.LinkSortParam = (sortBy == "link") ? "link_desc" : "link";
            ViewBag.DescriptionSortParam = (sortBy == "description") ? "description_desc" : "description";
            ViewBag.NameSortParam = (sortBy == "name") ? "name_desc" : "name";
            ViewBag.TypeSortParam = (sortBy == "type") ? "type_desc" : "type";
            ViewBag.UsernameSortParam = (sortBy == "username") ? "username_desc" : "username";

            switch (sortBy)
            {
                case "date":
                    exercises = exercises.OrderBy(e => e.created_at);
                    break;
                case "date_desc":
                    exercises = exercises.OrderByDescending(e => e.created_at);
                    break;
                case "link":
                    exercises = exercises.OrderBy(e => e.link);
                    break;
                case "link_desc":
                    exercises = exercises.OrderByDescending(e => e.link);
                    break;
                case "description":
                    exercises = exercises.OrderBy(e => e.description);
                    break;
                case "description_desc":
                    exercises = exercises.OrderByDescending(e => e.description);
                    break;
                case "name":
                    exercises = exercises.OrderBy(e => e.name);
                    break;
                case "name_desc":
                    exercises = exercises.OrderByDescending(e => e.name);
                    break;
                case "username":
                    exercises = exercises.OrderBy(e => e.user.username);
                    break;
                case "username_desc":
                    exercises = exercises.OrderByDescending(e => e.user.username);
                    break;
                case "type":
                    exercises = exercises.OrderBy(e => e.type.name);
                    break;
                case "type_desc":
                    exercises = exercises.OrderByDescending(e => e.type.name);
                    break;
                default:
                    exercises = exercises.OrderBy(e => e.name);
                    break;
            }

            return exercises;
        }

        /// <summary>
        /// Sets the WorkoutSearch object with the stored session search variables if they exist
        /// </summary>
        /// <param name="search">The WorkoutSearch object to set</param>
        /// <returns>The WorkoutSearch object set with the session search variables if the session exists, else the passed in WorkoutSearch object</returns>
        private ExerciseSearch setSearchFromSession(ExerciseSearch search)
        {
            if (Session != null)
            {
                search.name = Session["NameSearchParam"] as String;
                search.description = Session["DescriptionSearchParam"] as String;
            }
            return search;
        }

        /// <summary>
        /// Sets the session search parameters based on the current search values
        /// </summary>
        /// <param name="search">The WorkoutSearch object containing the values to set in the session</param>
        private void setSessionFromSearch(ExerciseSearch search)
        {
            if (Session != null)
            {
                if (!String.IsNullOrEmpty(search.name)) Session["NameSearchParam"] = search.name;
                else Session["NameSearchParam"] = "";

                if (!String.IsNullOrEmpty(search.description)) Session["DescriptionSearchParam"] = search.description;
                else Session["DescriptionSearchParam"] = "";


            }
        }
    }
}
