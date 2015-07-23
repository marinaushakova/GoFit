using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    /// <summary>
    /// Handles various application errors displaying the appropriate error message
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Overrides the Controller.HandleUnknownAction method to redirect to the
        /// NotFoundError action of the Error controller
        /// </summary>
        /// <param name="actionName">The unknown action name</param>
        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("NotFoundError").ExecuteResult(this.ControllerContext);
        }
        
        // GET: /Error/
        /// <summary>
        /// Default non descript error
        /// </summary>
        /// <returns>The plain error view</returns>
        public ActionResult Index()
        {
            return View("Error");
        }

        /// <summary>
        /// Displays a not found error
        /// </summary>
        /// <returns>Not found view</returns>
        public ActionResult NotFoundError()
        {
            return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The requested resource does not exist or could not be found")); 
        }

        /// <summary>
        /// Returns not found error page with the provided message
        /// </summary>
        /// <param name="message">The message to display in the error page</param>
        /// <returns>The DetailedError page with the given message</returns>
        public ActionResult NotFoundErrorWithMessage(string message)
        {
            return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, message)); 
        }

        /// <summary>
        /// Displays a generic catch all error page
        /// </summary>
        /// <returns>The generic catch all error view</returns>
        public ActionResult CatchAllError()
        {
            return View("CatchAllError"); 
        }
	}
}