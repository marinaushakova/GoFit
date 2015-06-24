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
            workouts = this.doSearch(workouts, workoutSearch, filterString, sortBy, page);
            workouts = this.doSort(workouts, sortBy);

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


        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<workout> doSearch(IQueryable<workout> workouts, WorkoutSearch search, String filterString, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy) || !String.IsNullOrEmpty(filterString))
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
            ViewBag.DescriptionSortParam = (sortBy == "description") ? "description_desc" : "description";
            ViewBag.DateSortParam = (sortBy == "date") ? "date_desc" : "date";
            ViewBag.TimestampSortParam = (sortBy == "time") ? "time_desc" : "time";
            ViewBag.CategorySortParam = (sortBy == "category") ? "category_desc" : "category";
            ViewBag.UsernameSortParam = (sortBy == "user") ? "user_desc" : "user";

            switch (sortBy)
            {
                case "name_desc":
                    workouts = workouts.OrderByDescending(w => w.name);
                    break;
                case "description":
                    workouts = workouts.OrderBy(w => w.description);
                    break;
                case "description_desc":
                    workouts = workouts.OrderByDescending(w => w.description);
                    break;
                case "date":
                    workouts = workouts.OrderBy(w => w.created_at);
                    break;
                case "date_desc":
                    workouts = workouts.OrderByDescending(w => w.created_at);
                    break;
                case "time":
                    workouts = workouts.OrderBy(w => w.timestamp);
                    break;
                case "time_desc":
                    workouts = workouts.OrderByDescending(w => w.timestamp);
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
                    workouts = workouts.OrderBy(w => w.name);
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
