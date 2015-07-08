using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using GoFit.Controllers.ControllerHelpers;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security;

namespace GoFit.Controllers
{
    public class MyAccountController : Controller
    {
        private masterEntities db;

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public MyAccountController()
        {
            db = new masterEntities();
        }

        /// <summary>
        /// Parameterized constructor that takes a db context as the parameter
        /// </summary>
        /// <param name="context">The db context to use</param>
        public MyAccountController(masterEntities context)
        {
            db = context;
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string ReturnUrl = "")
        {
            using (db)
            {
                string hashedPassword = Hasher.HashPassword(login.Username, login.Password);
                var user = db.users.Where(a => a.username.Equals(login.Username) && a.password.Equals(hashedPassword)).FirstOrDefault();
                
                ModelState.Remove("Password");
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.username, login.RememberMe);

                    if (user.is_admin == 1)
                    {
                        return RedirectToAction("Index", "AdminHome");
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "MyProfile");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user user)
        {
            Login login = new Login();
            login.Username = user.username;
            login.Password = user.password;

            string hashedPassword = Hasher.HashPassword(user.username, user.password);
            user.password = hashedPassword;
            user.is_admin = 0;

            //checks if username already exists in db
            var isDuplicate = db.users.Where(u => u.username == user.username).FirstOrDefault();
            if (isDuplicate != null)
            {
                ModelState.AddModelError("username", "This username already exists. Please choose another one.");
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.users.Add(user);
                    db.SaveChanges();

                    return Login(login);
                }
                catch (Exception ex)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "An error occured while trying to register new account"));
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to register new account"));
            }
        }
    }
}