using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GoFit.Controllers
{
    /// <summary>
    /// Class to provide common helper methods to the various controllers. 
    /// Also allows override of helpers as needing for testing purposes
    /// </summary>
    public class ControllerHelpers
    {
        /// <summary>
        /// Gets the id of the current user else returns -1
        /// </summary>
        /// <param name="db">The db instance</param>
        /// <param name="User">The Controller.User object to get the username from</param>
        /// <returns>The id of the current logged in user else -1</returns>
        public static int getUserId(masterEntities db, IPrincipal User)
        {
            if (User != null)
            {
                user user = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault();
                int userId = -1;
                if (user != null) userId = user.id;
                return userId;
            }
            return -1;
        }
    }
}