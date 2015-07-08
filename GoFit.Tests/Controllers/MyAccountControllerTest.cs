using GoFit.Controllers;
using GoFit.Models;
using GoFit.Tests.MockContexts;
using GoFit.Tests.MockSetupHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests the MyAccount Controller functionality
    /// </summary>
    [TestClass]
    public class MyAccountControllerTest
    {
        private MyAccountController myAccountCon;
        private Mock<masterEntities> db;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();

            db = contextHelpers.getDbContext();

            myAccountCon = new MyAccountController(db.Object);
            //{
             //   ControllerContext = MockContext.AuthenticationContext("jjones")
            //};

        }

        [TestMethod]
        public void TestUserDuplicate()
        {
            user u = new user()
            {
                username = "jjones",
                password = "jjones"
            };
            ViewResult result = myAccountCon.Register(u) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(myAccountCon.ModelState.IsValid, false);
            Assert.AreEqual(true, myAccountCon.ModelState.ContainsKey("username"));
        }


        /*
        /// <summary>
        /// Tests the MyAccont controller Login method to login as Admin
        /// </summary>
        [TestMethod]
        public void TestMyAccountLoginAdmin()
        {
            Login login = new Login
            {
                Username = "admin",
                Password = "admin"
            };
            ActionResult result = myAccountCon.Login(login);
            Assert.IsNotNull(result);
        }
        */
        /*
        [TestMethod]
        public void TestMyAccountRegister()
        {
            user user = new user();
            user.username = "user1";
            user.password = this.HashPassword("user1","user1");
            user.is_admin = 0;
            user.id = 15;
            db.Setup(c => c.users.Add(user)).Returns(user);
            user.password = "user1";
            RedirectToRouteResult result = myAccountCon.Register(user) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
            Assert.AreEqual("MyAccount", result.RouteValues["controller"]);
        }
        */

        // ------- Private helpers -------

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
