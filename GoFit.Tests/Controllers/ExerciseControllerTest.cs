using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using Moq;
using GoFit.Models;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Tests.MockContexts;
using System.Web.Mvc;
using System.Collections.Generic;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests the Exercise Controller functionality
    /// </summary>
    [TestClass]
    public class ExerciseControllerTest
    {
        private ExerciseController controller;
        private Mock<masterEntities> db;
        private WorkoutSearch search;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new WorkoutSearch();

            db = contextHelpers.getDbContext();
            controller = new ExerciseController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
        }

        /// <summary>
        /// Tests the Exercise controller Index method
        /// </summary>
        [TestMethod]
        public void TestExerciseControllerIndexViewRetrieval()
        {
            ViewResult result = controller.Index(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var exercises = (List<exercise>) result.ViewData.Model;
            Assert.IsTrue(exercises.Count == 3);
        }

        /// <summary>
        /// Tests the Exercise controller ExerciseDetails method
        /// </summary>
        [TestMethod]
        public void TestExerciseControllerDetailsForExercise1and3()
        {
            PartialViewResult result = controller.ExerciseDetails(1) as PartialViewResult;
            Assert.IsNotNull(result);
            exercise exercise1 = (exercise)result.ViewData.Model;
            Assert.AreEqual("ex1", exercise1.name, "Name was not 'ex1'");
            result = controller.ExerciseDetails(3) as PartialViewResult;
            Assert.IsNotNull(result);
            exercise exercise3 = (exercise)result.ViewData.Model;
            Assert.AreEqual("ex3", exercise3.name, "Name was not 'ex3'");
        }

        /// <summary>
        /// Tests the Exercise controller ExerciseDetails method
        /// with null passed as id parameter
        /// </summary>
        [TestMethod]
        public void TestExerciseControllerExerciseDetailsNoIdError()
        {
            PartialViewResult result = controller.ExerciseDetails(null) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Exercise could not be retrieved with given parameters.", model.StatusDescription);
        }

        [TestMethod]
        public void TestExerciseControllerExerciseDetailsNotFoundIdError()
        {
            PartialViewResult result = controller.ExerciseDetails(4000) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("Could not find the specified exercise.", model.StatusDescription);
        }
    }
}
