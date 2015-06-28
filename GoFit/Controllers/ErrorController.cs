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
	}
}