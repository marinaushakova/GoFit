using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoFit.Controllers
{
    public class MyAccountController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string ReturnUrl = "")
        {
            using (masterEntities dbEntities = new masterEntities())
            {
                var user = dbEntities.users.Where(a => a.username.Equals(login.Username) && a.password.Equals(login.Password)).FirstOrDefault();
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
                ModelState.Remove("Password");
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
	}
}