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
        public ActionResult Index(string filterString)
        {
            currUserId = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
            var user_workouts = from w in db.user_workout where w.user_id == currUserId select w;
            
            switch (filterString)
            {
                case "in_progress":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started != null && w.date_finished == null
                                    select w;
                    break;
                case "not_started":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started == null && w.date_finished == null
                                    select w;
                    break;
                case "completed":
                    user_workouts = from w in db.user_workout
                                    where w.user_id == currUserId && w.date_started != null && w.date_finished != null
                                    select w;
                    break;
                default:
                    break;
            }

            ActionResult view = View("Index", user_workouts.ToList());
            return view;
        }
	}
}