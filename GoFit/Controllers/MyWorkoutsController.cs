using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class MyWorkoutsController : Controller
    {
        //
        // GET: /MyWorkouts/
        [Authorize]
        public ActionResult Index()
        {
            ActionResult view = View("MyWorkouts");
            return view;
        }
	}
}