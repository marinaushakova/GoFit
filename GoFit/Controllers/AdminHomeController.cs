using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminHomeController : GoFitBaseController
    {
        private masterEntities db;

        public AdminHomeController() : base() 
        {
            db = this.getDB();
        }

        // GET: AdminHome
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            string time = "Evening";
            if (hour < 12)
                time = "Morning";
            else if (hour >= 12 && hour < 18)
                time = "Afternoon";
            else
                time = "Evening";

            ViewBag.greeting = "Good " + time + ", " + User.Identity.Name + "!";
            return View();
        }
    }
}