using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using PagedList;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines the home page functionality
    /// </summary>
    public class HomeController : Controller
    {
        private masterEntities db;
        private const int PAGE_SIZE = 10;

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public HomeController()
        {
            db = new masterEntities();
            pageSize = PAGE_SIZE;
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public HomeController(masterEntities context)
        {
            db = context;
            pageSize = PAGE_SIZE;
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