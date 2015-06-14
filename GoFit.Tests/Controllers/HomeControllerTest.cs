using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit;
using GoFit.Controllers;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests the Home Controller functionality
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        /// <summary>
        /// Tests the Home controller Index method
        /// </summary>
        [TestMethod]
        public void TestIndexViewRetrieval()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Workouts", result.ViewName);
            var workouts = (List<GoFit.Models.workout>) result.ViewData.Model;
        }
    }
}
