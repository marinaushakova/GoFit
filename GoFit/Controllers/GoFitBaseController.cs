using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// <summary>
        /// Overrides the Controller.HandleUnknownAction method to redirect to the
        /// NotFoundError action of the Error controller
        /// </summary>
        /// <param name="actionName">The unknown action name</param>
        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("NotFoundError", "Error").ExecuteResult(this.ControllerContext);
        }
    }
}