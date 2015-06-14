using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines the home page functionality
    /// </summary>
    public class HomeController : Controller
    {
        private masterEntities db = new masterEntities();

        /// <summary>
        /// Returns a list of workouts from the DB
        /// GET: /home/
        /// </summary>
        /// <param name="sortBy">String parameter telling the controller how to sore the workouts</param>
        /// <returns>A list of workouts from the DB</returns>
        [AllowAnonymous]
        public ActionResult Index(string sortBy)
        {
            var workouts = this.sortResults(sortBy);
            var view = View("Index", workouts.ToList());
            return view;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<workout> sortResults(string sortBy)
        {
            var workouts = from w in db.workouts select w;

            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortBy)) ? "name_desc" : "";
            ViewBag.DateSortParam = (sortBy == "date") ? "date_desc" : "date";
            ViewBag.CategorySortParam = (sortBy == "category") ? "category_desc" : "category";
            ViewBag.UserSortParam = (sortBy == "user") ? "user_desc" : "user";

            switch (sortBy)
            {
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
                    workouts = workouts.OrderBy(w => w.name);
                    break;
            }

            return workouts;
        }
    }
}