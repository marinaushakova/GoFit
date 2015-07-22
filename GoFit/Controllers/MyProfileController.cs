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
using System.Data.Entity.Infrastructure;

namespace GoFit.Controllers
{
    public class MyProfileController : GoFitBaseController
    {
        private masterEntities db;
        private UserAccess userAccess;


        public MyProfileController() : base()
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        public MyProfileController(masterEntities context) : base(context)
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }

        /// <summary>
        /// Gets an instance of the UserWorkoutViewModel
        /// which contains information for a user and a list
        /// of workouts for the brag feed partial view.
        /// </summary>
        /// <returns>An ActionResult instance</returns>
        [Authorize]
        public ActionResult Index()
        {
            UserWorkoutViewModel vm = new UserWorkoutViewModel();
            vm.BragFeedWorkoutList = db.user_workout.OrderByDescending(w => w.date_finished).Where(w => w.date_finished.HasValue).Take(50).ToList();
            vm.TheUser = db.users.Where(u => u.username.Equals(User.Identity.Name)).FirstOrDefault();

            if (vm.TheUser == null || vm.TheUser.id != userAccess.getUserId(User.Identity.Name))
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user."));
            }

            return View(vm);
        }

        [Authorize]
        public ActionResult Edit()
        {
            var view = View(db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());
            if (view == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user profile."));
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
            }

            if (ModelState.IsValid)
            {
                try
                {   
                    string hashedPassword = Hasher.HashPassword(user.username, user.password);
                    user.password = hashedPassword;
                    var u = db.users.Find(user.id);

                    var entry = db.Entry(u);
                    var state = entry.State;
                    if (state == EntityState.Detached)
                    {
                        db.Entry(user).State = EntityState.Modified;
                    }
                    else
                    {
                        entry.OriginalValues["timestamp"] = user.timestamp;
                        entry.CurrentValues.SetValues(user);
                    }
                    db.SaveChanges();
                    if (Session.Contents != null && Session.SessionID != null) FormsAuthentication.SetAuthCookie(user.username, false);
                    return RedirectToAction("Index", "MyProfile");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user as another user/admin may have already update this user"));
                }
                catch (DbUpdateException ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user."));
                }
                catch (Exception ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user."));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid changes."));
            }

        }
    }
}