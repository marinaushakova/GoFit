using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class MyWorkoutsController : Controller
    {

        private masterEntities db = new masterEntities();
        private int currUserId;

        //
        // GET: /MyWorkouts/
        [Authorize]
        public ActionResult Index()
        {
            currUserId = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
            var user_workouts = from w in db.user_workout where w.user_id == currUserId select w;

            ActionResult view = View("Index", user_workouts.ToList());
            return view;
        }
	}
}