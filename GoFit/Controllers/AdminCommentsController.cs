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
using System.Data.Entity.Infrastructure;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminCommentsController : GoFitBaseController
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
        public AdminCommentsController() : base()
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
            
        }

        /// <summary>
        /// Override to allow passing the desired db to the controller
        /// </summary>
        /// <param name="context">The db to use</param>
        public AdminCommentsController(masterEntities context)
            : base(context)
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No comment to view was specified"));
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The comment could not be found or does not exist"));
            }
            return View(comment);
        }

        // GET: AdminComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No comment to delete was specified"));
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The comment could not be found or does not exist"));
            }
            return View(comment);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "id,timestamp")] comment comment)
        {
            try
            {
                comment oldComment = db.comments.Find(comment.id);
                if (oldComment == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The comment does not exist or has already been deleted"));
                }
                var entry = db.Entry(oldComment);
                var state = entry.State;
                entry.OriginalValues["timestamp"] = comment.timestamp;
                db.comments.Remove(oldComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the comment as another admin may have modified it"));
            }
            catch (DbUpdateException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the comment."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminComments", "DeleteConfirmed");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the comment as it may be referenced in the database."));
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
            if (!String.IsNullOrEmpty(search.username)) comments = comments.Where(c => c.user.username.Contains(search.username));

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
                    comments = comments.OrderBy(c => c.date_created);
                    break;
                case "date_desc":
                    comments = comments.OrderByDescending(c => c.date_created);
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
                search.username = Session["UsernameSearchParam"] as String;
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

                if (!String.IsNullOrEmpty(search.username)) Session["UsernameSearchParam"] = search.username;
                else Session["UsernameSearchParam"] = "";

            }
        }
    }
}
