using GoFit.Controllers.ControllerHelpers;
using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class ExerciseController : Controller
    {

        private masterEntities db;
        private UserAccess userAccess;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;
            }

            // Redirect admins to admin home page upon authorization
            if (isAdmin == 1)
            {
                filterContext.Result = new RedirectResult("/AdminHome/Index");
            }
        }

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public ExerciseController()
        {
            db = new masterEntities();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public ExerciseController(masterEntities context)
        {
            db = context;
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Returns exercises view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var exList = db.exercises.ToList();
            return View(exList);
        }
	}
}