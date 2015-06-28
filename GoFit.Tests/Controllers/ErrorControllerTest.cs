using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using System.Web.Mvc;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests the Error controller functionality
    /// </summary>
    [TestClass]
    public class ErrorControllerTest
    {
        private ErrorController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new ErrorController();
        }

        [TestMethod]
        public void TestErrorControllerIndex()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void TestErrorControllerNotFoundError()
        {
            ViewResult result = controller.NotFoundError() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The requested resource does not exist or could not be found", model.StatusDescription);
        }
    }
}
