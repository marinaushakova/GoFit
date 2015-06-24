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

namespace GoFit.Controllers
{
    public class MyAccountController : Controller
    {
        private masterEntities db;
        private ControllerHelpers helper;

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public MyAccountController()
        {
            db = new masterEntities();
        }

        /// <summary>
        /// Constructor to allow a passed in db context
        /// </summary>
        /// <param name="context">The context to use</param>
        public MyAccountController(masterEntities context)
        {
            db = context;
            helper = new ControllerHelpers(db);
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
                string hashedPassword = HashPassword(login.Username, login.Password);
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

            string hashedPassword = HashPassword(user.username, user.password);
            user.password = hashedPassword;
            user.is_admin = 0;
            user.timestamp = DateTime.Now;

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
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to register new account");
                }
            }
            else
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to register new account"));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured while trying to register new account");
            }
        }

        /// <summary>
        /// Hashes the given data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string HashData(string data)
        {
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
            return HashData(String.Format("{0}{1}", userName.Substring(0, 4), password));
        }
    }
}