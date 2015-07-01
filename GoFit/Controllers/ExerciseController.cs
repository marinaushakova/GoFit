using GoFit.Controllers.ControllerHelpers;
using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// Returns exercises view for chosen exercise
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(int? ex_id)
        {
            if (ex_id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exercise could not be retrieved with given parameters."));
            }
            ViewBag.Exercise_id = ex_id;
            var exList = db.exercises.ToList();
            return View(exList);
        }

        /// <summary>
        /// Gets the view for a single exercise, showing its description and link to multimedia
        /// </summary>
        /// <param name="workoutId">The exercise id</param>
        /// <returns>The exercise's view</returns>
        [AllowAnonymous]
        public PartialViewResult ExerciseDetails(int? ex_id)
        {
            exercise exercise;
            if (ex_id == null)
            {
                return PartialView("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exercise could not be retrieved with given parameters."));
            }
            exercise = db.exercises.Find(ex_id);
            if (exercise == null)
            {
                return PartialView("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Could not find the specified exercise."));
            }
            return PartialView(exercise);
        }
	}
}