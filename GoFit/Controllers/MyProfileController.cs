using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [Authorize]
        public ActionResult Edit()
        {
            masterEntities dbEntities = new masterEntities();
            var view = View(dbEntities.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());
            return view;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(user user)
        {
            masterEntities dbEntities = new masterEntities();
            if (ModelState.IsValid)
            {
                user.timestamp = DateTime.Now;
                dbEntities.Entry(user).State = EntityState.Modified;

                dbEntities.SaveChanges();
                
                return RedirectToAction("Index");

            }

            return View(dbEntities.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());

        }
	}
}