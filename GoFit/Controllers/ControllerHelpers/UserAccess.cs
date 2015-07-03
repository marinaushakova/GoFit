using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GoFit.Controllers.ControllerHelpers
{
    /// <summary>
    /// Class to provide common helper methods to the various controllers. 
    /// Also allows override of helpers as needed for testing purposes
    /// </summary>
    public class UserAccess
    {
        private masterEntities db;

        public UserAccess(masterEntities db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets the id of the current user else returns -1
        /// </summary>
        /// <param name="username">The Controller.User object to get the username from</param>
        /// <returns>The id of the current logged in user else -1</returns>
        public int getUserId(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                user user = db.users.Where(a => a.username.Equals(username)).FirstOrDefault();
                int userId = -1;
                if (user != null) userId = user.id;
                return userId;
            }
            return -1;
        }

        ///// <summary>
        ///// Helper to determine whether or not a user is an admin
        ///// or not
        ///// </summary>
        ///// <param name="username">The username as a string</param>
        ///// <returns>true if the user is an administrator</returns>
        //public bool userIsAdmin(string username)
        //{
        //    bool isAdmin = false;
        //    user user = db.users.Where(u => u.username.Equals(username)).FirstOrDefault();
        //    if (user.is_admin == 1)
        //    {
        //        isAdmin = true;
        //    }
        //    return isAdmin;
        //}
    }
}