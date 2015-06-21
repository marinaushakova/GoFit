using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            string hashedPassword = HashPassword(user.username, user.password);
            user.password = hashedPassword;

            if (ModelState.IsValid)
            {
                user.timestamp = DateTime.Now;
                dbEntities.Entry(user).State = EntityState.Modified;

                dbEntities.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(dbEntities.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault());

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