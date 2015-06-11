using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class MyProfileController : Controller
    {
        //
        // GET: /MyProfile/
        [Authorize]
        public ActionResult Index()
        {
            ActionResult view = View("MyProfile");
            return view;
        }
	}
}