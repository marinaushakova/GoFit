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
        private masterEntities db = new masterEntities();
        private const int PAGE_SIZE = 2;


        /// <summary>
        /// Returns a list of workouts from the DB
        /// GET: /home/
        /// </summary>
        /// <param name="sortBy">String parameter telling the controller how to sore the workouts</param>
        /// <param name="page">The request page in the list of workouts</param>
        /// <returns>A list of workouts from the DB</returns>
        [AllowAnonymous]
        public ActionResult Index(string sortBy, int? page)
        {
            var workouts = from w in db.workouts select w;

            workouts = this.doSearch(workouts, page);
            workouts = this.sortResults(workouts, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", workouts.ToPagedList(pageNumber, PAGE_SIZE));
            return view;
        }

        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<workout> doSearch(IQueryable<workout> workouts, int? page)
        {
            string nameSearch = null;
            string categorySearch = null;
            string dateAddedSearch = null;
            string usernameSearch = null;

            if (Request != null && Request.Form != null)
            {
                var searchParams = Request.Form;
                nameSearch = searchParams["Name"];
                categorySearch = searchParams["Category"];
                dateAddedSearch = searchParams["DateAdded"];
                usernameSearch = searchParams["Username"];
            } 

            if (page != null)
            {
                nameSearch = Session["NameSearchParam"] as String;
                categorySearch = Session["CategorySearchParam"] as String;
                usernameSearch = Session["UserSearchParam"] as String;
            }

            if (!String.IsNullOrEmpty(nameSearch))
            {
                workouts = workouts.Where(w => w.name.Contains(nameSearch));
                Session["NameSearchParam"] = nameSearch;

            }
            else Session["NameSearchParam"] = "";

            if (!String.IsNullOrEmpty(categorySearch))
            {
                workouts = workouts.Where(w => w.category.name.Contains(categorySearch));
                Session["CategorySearchParam"] = categorySearch;
            }
            else Session["CategorySearchParam"] = "";

            if (!String.IsNullOrEmpty(dateAddedSearch))
            {
                string[] dateArrayString = dateAddedSearch.Split('-');
                int year = Convert.ToInt16(dateArrayString[0]);
                int month = Convert.ToInt16(dateArrayString[1]);
                int day = Convert.ToInt16(dateArrayString[2]);

                workouts = workouts.Where(w =>
                    w.created_at.Year == year &&
                    w.created_at.Month == month &&
                    w.created_at.Day == day);
            }


            if (!String.IsNullOrEmpty(usernameSearch))
            {
                workouts = workouts.Where(w => w.user.username.Contains(usernameSearch));
                Session["UsernameSearchParam"] = usernameSearch;
            }
            else Session["UsernameSearchParam"] = "";

            return workouts;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<workout> sortResults(IQueryable<workout> workouts, string sortBy)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                Session["SortBy"] = sortBy;
            }
            else
            {
                sortBy = Session["SortBy"] as String;
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
    }
}