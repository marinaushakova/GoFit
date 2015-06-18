using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Workouts()
        {
            return View();
        }

        public ActionResult Exercises()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Types()
        {
            return View();
        }
    }
}