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

namespace GoFit.Controllers
{
    public class MyProfileController : Controller
    {
        private masterEntities db;
        private ControllerHelpers helper;

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
            helper = new ControllerHelpers(db);
        }

        public MyProfileController(masterEntities context)
        {
            db = context;
            helper = new ControllerHelpers(db);
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
            
            if (user == null || user.id != helper.getUserId(User.Identity.Name))
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Could not get user."));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            masterEntities db = new masterEntities();
            user.timestamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    string hashedPassword = HashPassword(user.username, user.password);
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

        /// <summary>
        /// Hashes the given data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string HashData(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                throw new Exception("HashData can't be empty string or null");
            }
            
            SHA256 hasher = SHA256Managed.Create();
            byte[] hashedData = hasher.ComputeHash(Encoding.Unicode.GetBytes(data));

            StringBuilder sb = new StringBuilder(hashedData.Length * 2);
            foreach (byte b in hashedData)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Hashes the user login credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password can't be empty string or null");
            }
            return HashData(String.Format("{0}{1}", userName.Substring(0, 4), password));
        }
    }
}