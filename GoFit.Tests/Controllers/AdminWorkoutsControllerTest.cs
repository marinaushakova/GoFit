using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit;
using GoFit.Controllers;
using GoFit.Models;
using PagedList;
using Moq;
using System.Data.Entity;
using GoFit.Tests.MockContexts;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Tests.Controllers.TestHelpers;
using System.Data.Entity.Infrastructure;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests on the AdminWorkoutsController class
    /// </summary>
    [TestClass]
    public class AdminWorkoutsControllerTest
    {
        private AdminWorkoutsController adminCon;
        private Mock<masterEntities> db;
        private WorkoutSearch search;

        /// <summary>
        /// Test setup code to be run prior to each
        /// test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new WorkoutSearch();

            db = contextHelpers.getDbContext();
            adminCon = new AdminWorkoutsController(db.Object)
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

        #region David's Tests

        /// <summary>
        /// Test that AdminWorkouts Details view returns ViewData-
        /// a record from the Workout model
        /// </summary>
        [TestMethod]
        public void TestAdminWorkoutsDetailsViewReturnsData()
        {
            var result = adminCon.Details(1) as ViewResult;
            var workout = (workout)result.ViewData.Model;
            Assert.IsNotNull(result);
            Assert.AreEqual("workout1", workout.name);
        }

        /// <summary>
        /// Test that AdminWorkouts Create view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminWorkoutsNewViewReturnsData()
        {
            var result = adminCon.New() as ViewResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test that AdminWorkouts Edit view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminWorkoutsEditViewReturnsData()
        {
            var result = adminCon.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test that AdminWorkouts Delete view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminWorkoutsDeleteViewReturnsData()
        {
            var result = adminCon.Delete(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        #endregion

        /// <summary>
        /// Test the workouts are sorted ascending upon passing
        /// sortBy "name" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsNameAsc()
        {
            string sortBy = "name";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedAsc = CheckSort.isSorted(workouts, "name", "asc");
            Assert.IsTrue(isSortedAsc);
            
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in descending
        /// order upon passing "name_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "name", "desc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in descending
        /// order upon passing "description_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsDescriptionDesc()
        {
            string sortBy = "description_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "description", "desc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in ascending
        /// order upon passing "description" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsDescriptionAsc()
        {
            string sortBy = "description";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "description", "asc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in ascending
        /// order upon passing "date" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsDateAsc()
        {
            string sortBy = "date";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "dateCreated", "asc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in descending
        /// order upon passing "date_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsDateDesc()
        {
            string sortBy = "date_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "dateCreated", "desc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in ascending
        /// order upon passing "category" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsCategoryAsc()
        {
            string sortBy = "category";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "category", "asc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in descending
        /// order upon passing "category_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsCategoryDesc()
        {
            string sortBy = "category_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "category", "desc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in descending
        /// order upon passing "user_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsUserDesc()
        {
            string sortBy = "user_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "username", "desc"));
        }

        /// <summary>
        /// Test that the workouts are returned and sorted in ascending
        /// order upon passing "user" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminWorkoutsUserAsc()
        {
            string sortBy = "user";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(CheckSort.isSorted(workouts, "username", "asc"));
        }

        /// <summary>
        /// Test that the AdminWorkouts Index view returns data
        /// </summary>
        [TestMethod]
        public void TestAdminWorkoutsIndexViewRender()
        {
            ViewResult result = adminCon.Index(null, null, null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count > 0);
        }
             
        
        [TestMethod]
        public void TestAdminWorkoutsDetailsWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Details(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("A workout to view was not specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsDetailsWithNotFoundWorkout()
        {
            ViewResult result = adminCon.Details(6523) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("That workout could not be found or does not exist", model.StatusDescription);
        }
        
        [TestMethod]
        public void TestAdminWorkoutsNewThrowsException()
        {
            workout w = new workout();
            db.Setup(c => c.SaveChanges()).Throws(new Exception());
            ViewResult result = adminCon.New(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to create the requested workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateNewNoWorkoutPassedError()
        {
            ViewResult result = adminCon.New(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Workout could not be created with given parameters.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateNewWorkoutWithNoUserError()
        {
            adminCon = new AdminWorkoutsController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("not_a_real_user")
            };
            var workout = new workout();
            var workoutExercise = new workout_exercise();
            workout.workout_exercise.Add(workoutExercise);
            ViewResult result = adminCon.New(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("No user could be associated with the workout being created", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateNewWorkoutWithValidationErrs()
        {
            var workout = new workout();
            var workoutExercise = new workout_exercise();
            workout.workout_exercise.Add(workoutExercise);
            adminCon.ModelState.AddModelError("Fail", "Failed");
            ViewResult result = adminCon.New(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Could not create the workout with the given values.", model.StatusDescription);
        }
        
        
        [TestMethod]
        public void TestAdminWorkoutsEditWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("A workout to edit was not specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsGetEditWithNotFoundWorkout()
        {
            int? id = 6042;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("That workout could not be found or does not exist", model.StatusDescription);
        } 

        [TestMethod]
        public void TestAdminWorkoutsPostEditWorkoutNotFound()
        {
            workout w = new workout();
            ViewResult result = adminCon.Edit(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The workout does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostEditWithNullWorkout()
        {
            workout w = null;
            ViewResult result = adminCon.Edit(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit the workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostEditWithConcurrencyException()
        {
            workout w = new workout()
            {
                id = 1
            };
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.Edit(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit workout as another admin may have already update this workout", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostEditWithDbUpdateException()
        {
            workout w = new workout()
            {
                id = 1,
                category_id = 1,
                name = "xfs",
                description = "sadsa",
                created_by_user_id = 1
            };
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.Edit(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsDeleteWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("A workout to delete was not specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsGetDeleteWithNotFoundWorkout()
        {
            int? id = 6042;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("That workout could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostDeleteWorkoutNotFound()
        {
            workout w = new workout();
            ViewResult result = adminCon.DeleteConfirmed(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The workout does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostDeleteWithNullWorkout()
        {
            workout w = null;
            ViewResult result = adminCon.DeleteConfirmed(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostDeleteWithConcurrencyException()
        {
            workout w = new workout()
            {
                id = 1
            };
            db.Setup(c => c.workouts.Find(w.id)).Returns(w);
            db.Setup(c => c.workouts.Remove(w)).Returns(w);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.DeleteConfirmed(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the workout as another admin may have modified this workout", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminWorkoutsPostDeleteWithDbUpdateException()
        {
            workout w = new workout()
            {
                id = 1,
                category_id = 1,
                name = "xfs",
                description = "sadsa",
                created_by_user_id = 1
            };
            db.Setup(c => c.workouts.Find(w.id)).Returns(w);
            db.Setup(c => c.workouts.Remove(w)).Returns(w);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.DeleteConfirmed(w) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the workout as it may be referenced by another item.", model.StatusDescription);
        }
    }
}
