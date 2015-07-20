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
using System.Web;
using GoFit.Tests.Controllers.TestHelpers;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests the Home Controller functionality
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private Mock<masterEntities> db;
        private WorkoutSearch search;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new WorkoutSearch();

            db = contextHelpers.getDbContext();
            controller = new HomeController(db.Object) {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
            controller.pageSize = 10;
        }

        /// <summary>
        /// Tests the Home controller Index method
        /// </summary>
        [TestMethod]
        public void TestIndexViewRetrieval()
        {
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var workouts = (PagedList<workout>) result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 10);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by name in ascending order
        /// with an empty string for the sortBy parameter
        /// </summary>
        [TestMethod]
        public void TestIndexSortByNameAsc()
        {
            string sortBy = "name";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedAsc = CheckSort.isSorted(workouts, "name", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by name in descending order
        /// with 'name_desc' for the sortBy parameter
        /// </summary>
        [TestMethod]
        public void TestIndexSortByNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedDesc = CheckSort.isSorted(workouts, "name", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by category in asc order
        /// with 'category' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestIndexSortByCategoryAsc()
        {
            string sortBy = "category";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedAsc = CheckSort.isSorted(workouts, "category", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by category in desc order
        /// with 'category_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestIndexSortByCategoryDesc()
        {
            string sortBy = "category_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedDesc = CheckSort.isSorted(workouts, "category", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by date created in asc order
        /// with 'date' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestIndexSortByDateCreatedAsc()
        {
            string sortBy = "date";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedAsc = CheckSort.isSorted(workouts, "dateCreated", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by date created in asc order
        /// with 'date_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestIndexSortByDateCreatedDesc()
        {
            string sortBy = "date_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedDesc = CheckSort.isSorted(workouts, "dateCreated", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by username in asc order
        /// with 'user' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestIndexSortByUserAsc()
        {
            string sortBy = "user";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedAsc = CheckSort.isSorted(workouts, "username", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by username in desc order
        /// with 'user_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestIndexSortByUserDesc()
        {
            string sortBy = "user_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            var isSortedDesc = CheckSort.isSorted(workouts, "username", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that searching for "1" returns all workouts with "1" in their name 
        /// and the same for search string "2"
        /// </summary>
        [TestMethod]
        public void TestIndexSearchByWorkoutName()
        {
            controller.pageSize = 50;
            search.name = "1";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 14);
            search.name = "2";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 7);
        }

        [TestMethod]
        public void TestIndexSearchByCategoryName()
        {
            controller.pageSize = 50;
            search.category = "strength";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.AreEqual(12, workouts.Count);
            search.category = "endurance";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.AreEqual(19, workouts.Count);
        }

        [TestMethod]
        public void TestIndexSearchByDateAdded()
        {
            controller.pageSize = 50;
            search.dateAdded = "2015-06-15";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 6);
            search.dateAdded = "2015-06-14";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 6);
        }

        [TestMethod]
        public void TestIndexSearchByUsername()
        {
            controller.pageSize = 50;
            search.username = "admin";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.AreEqual(9, workouts.Count);
            search.username = "bob";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.AreEqual(12, workouts.Count);
        }

        [TestMethod]
        public void TestIndexSearchCategoryAndSortUserDesc()
        {
            controller.pageSize = 20;
            search.category = "strength";
            ViewResult result = controller.Index("user_desc", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.AreEqual(12, workouts.Count);
            var isSortedDesc = CheckSort.isSorted(workouts, "username", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        [TestMethod]
        public void TestIndexSearchByNameSortByDateAddedAsc()
        {
            controller.pageSize = 20;
            search.name = "2";
            controller.Session["NameSearchParam"] = "2";
            ViewResult result = controller.Index("date", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 7);
            var isSortedAsc = CheckSort.isSorted(workouts, "dateCreated", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        [TestMethod]
        public void TestDetailsForWorkout1()
        {
            ViewResult result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            workout workout1 = (workout)result.ViewData.Model;
            Assert.AreEqual("workout1", workout1.name, "Name was not 'workout1'");
        }

        [TestMethod]
        public void TestDetailsForWorkout24()
        {
            ViewResult result = controller.Details(24) as ViewResult;
            Assert.IsNotNull(result);
            workout workout24 = (workout)result.ViewData.Model;
            Assert.AreEqual("desc24", workout24.description, "description was not 'desc24'");
        }
        
        [TestMethod]
        public void TestHomeControllerGetNewWorkoutView()
        {
            ViewResult result = controller.New() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
        
        /// <summary>
        /// Tests creation of new workout
        /// </summary>
        [TestMethod]
        public void TestCreateNewWorkout()
        {
            workout workout = new workout();
            workout.id = 100;
            workout.name = "TestWorkoutName";
            workout.description = "TestWorkoutDescription";
            workout.category_id = 1;
            workout.created_by_user_id = 1;
            workout_exercise workoutExercise = new workout_exercise();
            workoutExercise.id = 4;
            workoutExercise.exercise_id = 1;
            workoutExercise.workout_id = 100;
            workoutExercise.position = 1;
            workoutExercise.duration = 5;
            workout.workout_exercise.Add(workoutExercise);
            db.Setup(c => c.workouts.Add(workout)).Returns(workout);
            RedirectToRouteResult result = controller.New(workout) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.RouteValues["workoutId"], "workoutId was not 100");
            Assert.AreEqual("Details", result.RouteValues["action"], "action was not Details");
            Assert.AreEqual("Home", result.RouteValues["controller"], "controller was not Home");
        }

        [TestMethod]
        public void TestHomeControllerDetailsNoIdError()
        {
            ViewResult result = controller.Details(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Workout could not be retrieved with given parameters.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerDetailsNotFoundIdError()
        {
            ViewResult result = controller.Details(4000) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("Could not find the specified workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateNewNoWorkoutPassedError()
        {
            ViewResult result = controller.New(null) as ViewResult;
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
            controller = new HomeController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("not_a_real_user")
            };
            var workout = new workout();
            var workoutExercise = new workout_exercise();
            workout.workout_exercise.Add(workoutExercise);
            ViewResult result = controller.New(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("No user could be associated with the workout being created", model.StatusDescription);
        }
        
        [TestMethod]
        public void TestHomeControllerCreateNewWorkoutWithBadPassedObj()
        {
            var workout = new workout();
            var workoutExercise = new workout_exercise();
            workout.workout_exercise.Add(workoutExercise);
            var ex = new Exception();
            db.Setup(c => c.workouts.Add(workout)).Throws(ex);
            ViewResult result = controller.New(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to create the requested workout.", model.StatusDescription);
        }
        
        [TestMethod]
        public void TestHomeControllerCreateNewWorkoutWithValidationErrs()
        {
            var workout = new workout();
            var workoutExercise = new workout_exercise();
            workout.workout_exercise.Add(workoutExercise);
            controller.ModelState.AddModelError("Fail", "Failed");
            ViewResult result = controller.New(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Could not create the workout with the given values.", model.StatusDescription);
        }
        
    }
}
