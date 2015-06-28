using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using GoFit.Controllers.ControllerHelpers;
using PagedList;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminCommentsController : Controller
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
        public AdminCommentsController()
        {
            db = new masterEntities();
            pageSize = PAGE_SIZE;
            
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            try
            {
                var isAdmin = 0;
                if (User.Identity.IsAuthenticated)
                    isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;

                if (isAdmin != 1)
                {
                    ViewBag.UserIsAdmin = false;
                    // Redirect non-administrative users to the home page upon authorization
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
                else
                    ViewBag.UserIsAdmin = true;
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminCategories", "Create");
                RedirectToRoute("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error on authorization. Please contact site administrator."));
            }
        }
        // GET: AdminComments
        public ActionResult Index(string filterString, string sortBy, int? page, CommentSearch commentSearch)
        {
            //var comments = db.comments.Include(c => c.user).Include(c => c.workout);
            //return View(comments.ToList());
            var comments = from c in db.comments select c;
            comments = this.doSearch(comments, commentSearch, filterString, sortBy, page);
            comments = this.doSort(comments, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", comments.ToPagedList(pageNumber, pageSize));
            return view;
        }

        // GET: AdminComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: AdminComments/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.users, "id", "username");
            ViewBag.Workout_id = new SelectList(db.workouts, "id", "name");
            return View();
        }

        // POST: AdminComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,message,timestamp,User_id,Workout_id,date_cteated")] comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.User_id = new SelectList(db.users, "id", "username", comment.User_id);
                ViewBag.Workout_id = new SelectList(db.workouts, "id", "name", comment.Workout_id);
                return View(comment);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminComments", "Create");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the comment."));
            }
            
        }

        // GET: AdminComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.users, "id", "username", comment.User_id);
            ViewBag.Workout_id = new SelectList(db.workouts, "id", "name", comment.Workout_id);
            return View(comment);
        }

        // POST: AdminComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,message,timestamp,User_id,Workout_id,date_cteated")] comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.User_id = new SelectList(db.users, "id", "username", comment.User_id);
                ViewBag.Workout_id = new SelectList(db.workouts, "id", "name", comment.Workout_id);
                return View(comment);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminComments", "Edit");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the comment."));
            }
        }

        // GET: AdminComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                comment comment = db.comments.Find(id);
                db.comments.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminComments", "DeleteConfirmed");
                return View("~/Views/Shared/_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the comment as it may be referenced in the database."));
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
        private IQueryable<comment> doSearch(IQueryable<comment> comments, CommentSearch search, String filterString, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy) || !String.IsNullOrEmpty(filterString))
            {
                search = this.setSearchFromSession(search);
            }
            else setSessionFromSearch(search);

            if (!String.IsNullOrEmpty(search.message)) comments = comments.Where(c => c.message.Contains(search.message));

            return comments;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<comment> doSort(IQueryable<comment> comments, string sortBy)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                SessionVariableManager.setSessionFromSort(Session, sortBy);
            }
            else
            {
                sortBy = SessionVariableManager.setSortFromSession(Session, sortBy);
            }

            ViewBag.UsernameSortParam = (sortBy == "username") ? "username_desc" : "username";
            ViewBag.MessageSortParam = (sortBy == "message") ? "message_desc" : "message";
            ViewBag.WorkoutSortParam = (sortBy == "workout") ? "workout_desc" : "workout";
            ViewBag.DateSortParam = (sortBy == "date") ? "date_desc" : "date";

            switch (sortBy)
            {
                case "username_desc":
                    comments = comments.OrderByDescending(c => c.user.username);
                    break;
                case "workout":
                    comments = comments.OrderBy(c => c.workout.name);
                    break;
                case "workout_desc":
                    comments = comments.OrderByDescending(c => c.workout.name);
                    break;
                case "message":
                    comments = comments.OrderBy(c => c.message);
                    break;
                case "message_desc":
                    comments = comments.OrderByDescending(c => c.message);
                    break;
                case "date":
                    comments = comments.OrderBy(c => c.date_cteated);
                    break;
                case "date_desc":
                    comments = comments.OrderByDescending(c => c.date_cteated);
                    break;
                default:
                    comments = comments.OrderBy(c => c.user.username);
                    break;
            }

            return comments;
        }

        /// <summary>
        /// Sets the WorkoutSearch object with the stored session search variables if they exist
        /// </summary>
        /// <param name="search">The WorkoutSearch object to set</param>
        /// <returns>The WorkoutSearch object set with the session search variables if the session exists, else the passed in WorkoutSearch object</returns>
        private CommentSearch setSearchFromSession(CommentSearch search)
        {
            if (Session != null)
            {
                search.message = Session["MessageSearchParam"] as String;
            }
            return search;
        }

        /// <summary>
        /// Sets the session search parameters based on the current search values
        /// </summary>
        /// <param name="search">The WorkoutSearch object containing the values to set in the session</param>
        private void setSessionFromSearch(CommentSearch search)
        {
            if (Session != null)
            {

                if (!String.IsNullOrEmpty(search.message)) Session["MessageSearchParam"] = search.message;
                else Session["MessageSearchParam"] = "";

            }
        }
    }
}
