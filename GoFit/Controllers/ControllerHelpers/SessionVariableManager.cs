using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Controllers.ControllerHelpers
{
    /// <summary>
    /// Static helper class to handle setting and getting various 
    /// session variables
    /// </summary>
    public static class SessionVariableManager
    {
        /// <summary>
        /// Sets the WorkoutSearch object with the stored session search variables if they exist
        /// </summary>
        /// <param name="search">The WorkoutSearch object to set</param>
        /// <returns>The WorkoutSearch object set with the session search variables if the session exists, else the passed in WorkoutSearch object</returns>
        public static WorkoutSearch setSearchFromSession(HttpSessionStateBase Session, WorkoutSearch search)
        {
            if (Session != null)
            {
                if (!String.IsNullOrEmpty(Session["NameSearchParam"] as String)) search.name = Session["NameSearchParam"] as String;
                if (!String.IsNullOrEmpty(Session["CategorySearchParam"] as String)) search.category = Session["CategorySearchParam"] as String;
                if (!String.IsNullOrEmpty(Session["UserSearchParam"] as String)) search.username = Session["UserSearchParam"] as String;
            }
            return search;
        }

        /// <summary>
        /// Sets the session search parameters based on the current search values
        /// </summary>
        /// <param name="search">The WorkoutSearch object containing the values to set in the session</param>
        public static void setSessionFromSearch(HttpSessionStateBase Session, WorkoutSearch search)
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

                if (!String.IsNullOrEmpty(search.username)) Session["UserSearchParam"] = search.username;
                else Session["UserSearchParam"] = "";
            }
        }

        /// <summary>
        /// Sets the sortBy param to the session sort value if the session exists. 
        /// If the session does not exist the passed in sortBy param is returned. 
        /// </summary>
        /// <param name="sortBy">The current sort filter</param>
        /// <returns>The sort parameter set from the session else the original sort param</returns>
        public static string setSortFromSession(HttpSessionStateBase Session, string sortBy)
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
        public static void setSessionFromSort(HttpSessionStateBase Session, string sortBy)
        {
            if (Session != null) Session["SortBy"] = sortBy;
        }
    }
}