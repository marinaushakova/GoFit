using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines the basic common functionality of all controller in theis application, 
    /// specifically the HandleUnknownAction and OnAuthorization overrides
    /// </summary>
    public abstract class GoFitBaseController : Controller
    {
        private masterEntities db { get; set; }

        private readonly string[] ADMIN_CONTROLLERS = {
            "GoFit.Controllers.AdminCategoriesController", 
            "GoFit.Controllers.AdminCommentsController",
            "GoFit.Controllers.AdminExercisesController",
            "GoFit.Controllers.AdminHomeController",
            "GoFit.Controllers.AdminTypesController",
            "GoFit.Controllers.AdminWorkoutsController",
            "GoFit.Controllers.ErrorController"
        };

        private readonly string[] USER_CONTROLLERS = {
            "GoFit.Controllers.HomeController", 
            "GoFit.Controllers.MyAccountController",
            "GoFit.Controllers.MyProfileController",
            "GoFit.Controllers.MyWorkoutsController",
            "GoFit.Controllers.FavoriteWorkoutsController",
            "GoFit.Controllers.CommentsController",
            "GoFit.Controllers.ExerciseController",
            "GoFit.Controllers.ErrorController"
        };

        /// <summary>
        /// Sets the db to the given context
        /// For testing
        /// </summary>
        /// <param name="context">The context to set the db to</param>
        public GoFitBaseController(masterEntities context)
        {
            db = context;
        }

        /// <summary>
        /// Constructs a new db instance
        /// </summary>
        public GoFitBaseController()
        {
            db = new masterEntities();
        }

        /// <summary>
        /// Getter for the db instance
        /// </summary>
        /// <returns>The db instance</returns>
        public masterEntities getDB()
        {
            return db;
        }

        /// <summary>
        /// Overrides the Controller.HandleUnknownAction method to redirect to the
        /// NotFoundError action of the Error controller
        /// </summary>
        /// <param name="actionName">The unknown action name</param>
        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("NotFoundError", "Error").ExecuteResult(this.ControllerContext);
        }

        /// <summary>
        /// Redirects the logged in user to the admin page if they are an admin
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault();
                if (user != null) isAdmin = user.is_admin;
            }

            string destinationController = filterContext.Controller.ToString();
            if (isAdmin == 1)
            {
                ViewBag.UserIsAdmin = true;
                var isAdminController = ADMIN_CONTROLLERS.Contains(destinationController);
                if (!isAdminController) filterContext.Result = new RedirectResult("/AdminHome/Index");
            }
            else
            {
                ViewBag.UserIsAdmin = false;
                var isUserController = USER_CONTROLLERS.Contains(destinationController);
                if (!isUserController) filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}