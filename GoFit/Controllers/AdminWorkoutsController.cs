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

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminWorkoutsController : Controller
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
        public AdminWorkoutsController()
        {
            db = new masterEntities();
            pageSize = PAGE_SIZE;
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;
            }

            // Redirect non-administrative users to the home page upon authorization
            if (isAdmin != 1)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }

        // GET: AdminWorkouts
        public ActionResult Index(string filterString, string sortBy, int? page, WorkoutSearch workoutSearch)
        {
            //currUserId = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
            //var workouts = db.workouts.Include(w => w.category).Include(w => w.user);
            var workouts = from w in db.workouts select w;

            //workouts = this.doFilter(workouts, filterString);
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
                    workout.timestamp = DateTime.Now;
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
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the workout."));
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
                    db.Entry(workout).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
                ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
                return View(workout);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminWorkouts", "Edit");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the workout."));
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
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                workout workout = db.workouts.Find(id);
                db.workouts.Remove(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminWorkouts", "DeleteConfirmed");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the workout as it may be referenced in the database."));
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
