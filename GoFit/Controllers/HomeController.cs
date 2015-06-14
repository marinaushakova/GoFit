using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;

namespace GoFit.Controllers
{
    /// <summary>
    /// Defines the home page functionality
    /// </summary>
    public class HomeController : Controller
    {
        private masterEntities db = new masterEntities();

        /// <summary>
        /// Returns a list of workouts from the DB
        /// GET: /home/
        /// </summary>
        /// <returns>A list of workouts from the DB</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            var workouts = from w in db.workouts select w;
            var view = View("Workouts", workouts.ToList());
            return view;
        }
    }
}