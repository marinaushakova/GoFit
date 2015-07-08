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
    public class ExerciseController : GoFitBaseController
    {

        private masterEntities db;
        private UserAccess userAccess;

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public ExerciseController() : base()
        {
            db = this.getDB();
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
            if (ex_id != null)
            {
                ViewBag.Exercise_id = ex_id;
            }
            
            var exList = db.exercises.OrderBy(m => m.name).ToList();
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
                return PartialView("_ErrorPartial", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exercise could not be retrieved with given parameters."));
            }
            exercise = db.exercises.Find(ex_id);
            if (exercise == null)
            {
                return PartialView("_ErrorPartial", new HttpStatusCodeResult(HttpStatusCode.NotFound, "Could not find the specified exercise."));
            }
            return PartialView(exercise);
        }
	}
}