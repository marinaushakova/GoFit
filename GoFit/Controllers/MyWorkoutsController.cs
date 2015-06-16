using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class MyWorkoutsController : Controller
    {

        private masterEntities db = new masterEntities();
        private int currUserId;

        //
        // GET: /MyWorkouts/
        /// <summary>
        /// Returns the list of My Workouts for currently logged in user
        /// </summary>
        /// <param name="filterString">Parameter is used to filter my workouts list</param>
        /// <returns>A list of users workouts from DB</returns>
        [Authorize]
        public ActionResult Index(string filterString, string sortBy, string nameSearch, string categorySearch, DateTime? dateAddedSeach, string usernameSearch)
        {
            currUserId = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
            var user_workouts = from w in db.user_workout where w.user_id == currUserId select w;

            if (!String.IsNullOrEmpty(filterString))
            {
                ViewBag.FilterParam = filterString;
            }
            else ViewBag.FilterParam = "";

            switch (filterString)
            {
                case "in_progress":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started != null && w.date_finished == null
                                    select w;
                    break;
                case "not_started":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started == null && w.date_finished == null
                                    select w;
                    break;
                case "completed":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started != null && w.date_finished != null
                                    select w;
                    break;
                default:
                    break;
            }
            
            if (!String.IsNullOrEmpty(nameSearch))
            {
                user_workouts = user_workouts.Where(w => w.workout.name.Contains(nameSearch));
                ViewBag.NameSearchParam = nameSearch;
            }
            else if (!String.IsNullOrEmpty(ViewBag.NameSearchParam))
            {
                string nameSearchParam = ViewBag.NameSearchParam;
                user_workouts = user_workouts.Where(w => w.workout.name.Contains(nameSearchParam));
            }
            else ViewBag.NameSearchParam = null;

            if (!String.IsNullOrEmpty(categorySearch)) user_workouts = user_workouts.Where(w => w.workout.category.name.Contains(categorySearch));
            if (dateAddedSeach != null) user_workouts = user_workouts.Where(w => w.workout.created_at.Equals(dateAddedSeach));
            if (!String.IsNullOrEmpty(usernameSearch)) user_workouts = user_workouts.Where(w => w.workout.user.username.Contains(usernameSearch));

            user_workouts = this.sortResults(user_workouts, sortBy);

            ActionResult view = View("Index", user_workouts.ToList());
            return view;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<user_workout> sortResults(IQueryable<user_workout> user_workouts, string sortBy)
        {
            ViewBag.NameSortParam = (String.IsNullOrEmpty(sortBy)) ? "name_desc" : "";
            ViewBag.DateSortParam = (sortBy == "date") ? "date_desc" : "date";
            ViewBag.CategorySortParam = (sortBy == "category") ? "category_desc" : "category";
            ViewBag.UserSortParam = (sortBy == "user") ? "user_desc" : "user";

            switch (sortBy)
            {
                case "name_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.name);
                    break;
                case "date":
                    user_workouts = user_workouts.OrderBy(w => w.workout.created_at);
                    break;
                case "date_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.created_at);
                    break;
                case "category":
                    user_workouts = user_workouts.OrderBy(w => w.workout.category.name);
                    break;
                case "category_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.category.name);
                    break;
                case "user":
                    user_workouts = user_workouts.OrderBy(w => w.workout.user.username);
                    break;
                case "user_desc":
                    user_workouts = user_workouts.OrderByDescending(w => w.workout.user.username);
                    break;
                default:
                    user_workouts = user_workouts.OrderBy(w => w.workout.name);
                    break;
            }

            return user_workouts;
        }
	}
}