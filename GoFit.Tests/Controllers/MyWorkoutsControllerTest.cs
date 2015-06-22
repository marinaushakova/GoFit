using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Models;
using GoFit.Tests.MockContexts;
using Moq;
using System.Web.Mvc;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class MyWorkoutsControllerTest
    {
        private MyWorkoutsController myWorkoutsCon;
        private Mock<masterEntities> db;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();

            db = contextHelpers.getDbContext();
            myWorkoutsCon = new MyWorkoutsController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
        }

        [TestMethod]
        public void TestAddWorkoutToMyWorkouts()
        {
            user_workout userWorkout = new user_workout();
            userWorkout.workout_id = 1;
            db.Setup(c => c.user_workout.Add(userWorkout)).Returns(userWorkout);
            RedirectToRouteResult result = myWorkoutsCon.AddToMyWorkouts(userWorkout) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.RouteValues["user_workout_id"], "workoutId was not 1");
            Assert.AreEqual("Details", result.RouteValues["action"], "action was not Controller");
            Assert.AreEqual("MyWorkouts", result.RouteValues["controller"], "controller was not Home");
        }

        [TestMethod]
        public void TestMarkExerciseClickingFirstCheckbox()
        {
            JsonResult result = myWorkoutsCon.MarkExercise(1, 1) as JsonResult;

            Assert.IsNotNull(result);
        }
    }
}
