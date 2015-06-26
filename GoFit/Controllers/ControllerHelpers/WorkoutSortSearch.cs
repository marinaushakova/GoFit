using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Controllers.ControllerHelpers
{
    /// <summary>
    /// A static helper class to allow both the admin and user workouts controllers
    /// to use the same search and sort functions
    /// </summary>
    public static class WorkoutSortSearch
    {
        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="search">The WorkoutSearch object containing the parameters to search</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <param name="session">The Session object to get or set variables from/to</param>
        /// <param name="viewBag">The viewBag object to pass the set variables back to the view with</param>
        /// <returns>The searched workouts</returns>
        public static IQueryable<workout> doSearch(IQueryable<workout> workouts, WorkoutSearch search, string sortBy, int? page, HttpSessionStateBase session, dynamic viewBag)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy))
            {
                search = SessionVariableManager.setSearchFromSession(session, search);
            }
            else SessionVariableManager.setSessionFromSearch(session, search);

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
        /// <param name="session">The Session object to get or set variables from/to</param>
        /// <param name="viewBag">The viewBag object to pass the set variables back to the view with</param>
        /// <returns>The sorted workouts</returns>
        public static IQueryable<workout> doSort(IQueryable<workout> workouts, string sortBy, HttpSessionStateBase session, dynamic viewBag)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                SessionVariableManager.setSessionFromSort(session, sortBy);
            }
            else
            {
                sortBy = SessionVariableManager.setSortFromSession(session, sortBy);
            }

            viewBag.NameSortParam = (sortBy == "name") ? "name_desc" : "name";
            viewBag.DateSortParam = (sortBy == "date") ? "date_desc" : "date";
            viewBag.CategorySortParam = (sortBy == "category") ? "category_desc" : "category";
            viewBag.UserSortParam = (sortBy == "user") ? "user_desc" : "user";
            viewBag.DescriptionSortParam = (sortBy == "description") ? "description_desc" : "description";

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
                case "description":
                    workouts = workouts.OrderBy(w => w.description);
                    break;
                case "description_desc":
                    workouts = workouts.OrderByDescending(w => w.description);
                    break;
                default:
                    workouts = workouts.OrderByDescending(w => w.name);
                    break;
            }

            return workouts;
        }
    }
}