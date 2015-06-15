using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoFit.Controllers
{
    public class MyProfileController : Controller
    {
        //
        // GET: /MyProfile/
        [Authorize]
        public ActionResult Index()
        {
            masterEntities dbEntities = new masterEntities();
            var view = View(dbEntities.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());
            return view;
        }
	}
}