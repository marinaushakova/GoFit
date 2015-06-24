using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Models;
using Moq;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Tests.MockContexts;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class MyProfileControllerTest
    {
        private MyProfileController myProfileCon;
        private Mock<masterEntities> db;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();

            db = contextHelpers.getDbContext();
            myProfileCon = new MyProfileController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
        }

        /// <summary>
        /// Tests the MyProfile controller Index method
        /// </summary>
        [TestMethod]
        public void TestMyProfileIndexViewRetrieval()
        {
            ViewResult result = myProfileCon.Index() as ViewResult;
            Assert.IsNotNull(result);
            user user = (user)result.ViewData.Model;
            Assert.AreEqual("jjones", user.username, "Userame was not 'jjones'");
        }


        /// <summary>
        /// Tests the MyProfile controller Edit method
        /// </summary>
        [TestMethod]
        public void TestMyProfileEditUser()
        {
            ViewResult result = myProfileCon.Edit() as ViewResult;
            Assert.IsNotNull(result);
            user user = (user)result.ViewData.Model;
            Assert.AreEqual("jjones", user.username, "Userame was not 'jjones'");
            string plainTextPassword = "jjones";
            user.fname = "Jane";
            user.lname = "Jones";
            user.password = plainTextPassword;
            user.is_male = 0;
            user.weight = 130;
            RedirectToRouteResult editResult = myProfileCon.Edit(user) as RedirectToRouteResult;
            Assert.IsNotNull(editResult);
            Assert.AreEqual("Index", editResult.RouteValues["action"], "Action was not Index");
            Assert.AreEqual("MyProfile", editResult.RouteValues["controller"], "Controller was not MyProfile" );
            Assert.IsTrue(user.fname == "Jane");
            Assert.IsTrue(user.lname == "Jones");
            Assert.IsTrue(user.is_male == 0);
            Assert.IsTrue(user.weight == 130);
            string expectedPassword = this.HashPassword(user.username, plainTextPassword);
            Assert.AreEqual(expectedPassword, user.password);
        }




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
