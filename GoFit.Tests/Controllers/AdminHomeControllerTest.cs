using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Models;
using PagedList;
using GoFit.Tests.MockContexts;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class AdminHomeControllerTest
    {

        private AdminHomeController adminCon;

        [TestInitialize]
        public void Initialize()
        {
            this.adminCon = new AdminHomeController()
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
            
        }

        /// <summary>
        /// Test that the AdminTypes Index view returns data
        /// </summary>
        [TestMethod]
        public void TestAdminHomeIndexViewRender()
        {
            ViewResult result = adminCon.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
