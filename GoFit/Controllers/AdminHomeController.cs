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
        private masterEntities db = new masterEntities();

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;
            }

            // Redirect non-administrative users to the home page upon authorization
            if (isAdmin != 1)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }

        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}