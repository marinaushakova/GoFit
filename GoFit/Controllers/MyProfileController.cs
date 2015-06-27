using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoFit.Controllers.ControllerHelpers;

namespace GoFit.Controllers
{
    public class MyProfileController : Controller
    {
        private masterEntities db;
        private UserAccess userAccess;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;
            }

            // Redirect admins to admin home page upon authorization
            if (isAdmin == 1)
            {
                filterContext.Result = new RedirectResult("/AdminHome/Index");
            }
        }


        public MyProfileController()
        {
            db = new masterEntities();
            userAccess = new UserAccess(db);
        }

        public MyProfileController(masterEntities context)
        {
            db = context;
            userAccess = new UserAccess(db);
        }

        //
        // GET: /MyProfile/
        [Authorize]
        public ActionResult Index()
        {
            var view = View(db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());
            if (view == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user profile."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return view;
        }

        [Authorize]
        public ActionResult Edit()
        {
            var view = View(db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());
            if (view == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user profile."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return view;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(user user)
        {

            if (user == null || user.id != userAccess.getUserId(User.Identity.Name))
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //user.timestamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {   
                    string hashedPassword = Hasher.HashPassword(user.username, user.password);
                    user.password = hashedPassword;
                    
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "MyProfile");
                }
                catch (Exception ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user."));
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to save changes");
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid changes."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to save changes");
            }

        }
    }
}