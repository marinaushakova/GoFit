using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Models;
using Moq;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Tests.MockContexts;
using System.Web.Mvc;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class WorkoutRatingControllerTest
    {
        private WorkoutRatingController controller;
        private Mock<masterEntities> db;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();

            db = contextHelpers.getDbContext();
            controller = new WorkoutRatingController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
        }


        [TestMethod]
        public void TestWorkoutRatingAddRatingToNonExistingWorkout()
        {
            ViewResult result = controller.AddWorkoutRating(55, 8) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to rate the requested workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestWorkoutRatingAddRatingWithBadParameters()
        {
            ViewResult result = controller.AddWorkoutRating(55, null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No rating value was specified.", model.StatusDescription);
        }

        [TestMethod]
        public void TestWorkoutRatingAddRatingWithNoUserError()
        {
            controller = new WorkoutRatingController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("not_a_real_user")
            };
            ViewResult result = controller.AddWorkoutRating(28, 10) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("No user could be associated with the rating being added", model.StatusDescription);
        }

        [TestMethod]
        public void TestWorkoutRatingAddRating()
        {
            workout_rating rating = new workout_rating()
            {
                workout_id = 100,
                average_rating = 0,
                times_rated = 0
            };
            db.Setup(w => w.workout_rating.Add(rating)).Returns(rating);
            RedirectToRouteResult result = controller.AddWorkoutRating(100, 10) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"], "action was not Index");
            Assert.AreEqual("Home", result.RouteValues["controller"], "controller was not Home");
        }
    }
}
