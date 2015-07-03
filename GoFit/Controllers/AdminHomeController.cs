using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using System.Net;
using GoFit.Controllers.ControllerHelpers;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminHomeController : GoFitBaseController
    {
        private masterEntities db;
        private UserAccess userAccess;


        public AdminHomeController() : base()
        {
            db = this.getDB();
            userAccess = new UserAccess(db);
        }


        // GET: AdminHome
        public ActionResult Index()
        {
            var view = View(db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());
            if (view == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user profile."));
            }
            



            int hour = DateTime.Now.Hour;
            string time = "Evening";
            if (hour < 12)
                time = "Morning";
            else if (hour >= 12 && hour < 18)
                time = "Afternoon";

            ViewBag.greeting = "Good " + time + ", " + User.Identity.Name + "!";
            //return View();
            return view;
        }

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
                    return RedirectToAction("Index", "MyProfile");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user as another user/admin may have already update this user"));
                }
                catch (DbUpdateException)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user."));
                }
                catch (Exception)
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