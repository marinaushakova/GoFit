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
            Assert.IsTrue(workouts.Count == 12);
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
            Assert.AreEqual(9, workouts.Count);
            search.category = "endurance";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<workout>)result.ViewData.Model;
            Assert.AreEqual(15, workouts.Count);
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
            Assert.AreEqual(9, workouts.Count);
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
        public void TestHomeControllerGetCreateWorkoutView()
        {
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        /// <summary>
        /// Tests creation of new workout
        /// </summary>
        [TestMethod]
        public void TestCreateWorkout()
        {
            workout workout = new workout();
            workout.id = 100;
            workout.name = "TestWorkoutName";
            workout.description = "TestWorkoutDescription";
            workout.category_id = 1;
            workout.created_by_user_id = 1;
            db.Setup(c => c.workouts.Add(workout)).Returns(workout);
            RedirectToRouteResult result = controller.Create(workout) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.RouteValues["id"], "workoutId was not 100");
            Assert.AreEqual("AddExerciseToWorkout", result.RouteValues["action"], "action was not AddExerciseToWorkout");
            Assert.AreEqual("Home", result.RouteValues["controller"], "controller was not Home");
        }
       
        [TestMethod]
        public void TestAddExerciseToWorkout()
        {
            workout_exercise workoutExercise = new workout_exercise();
            workoutExercise.id = 4;
            workoutExercise.exercise_id = 1;
            workoutExercise.workout_id = 2;
            workoutExercise.position = 3;
            workoutExercise.duration = 5;
            controller.Session["workout_id"] = 2;
            ViewResult r = controller.AddExerciseToWorkout(2) as ViewResult;
            Assert.IsNotNull(r);
            db.Setup(c => c.workout_exercise.Add(workoutExercise)).Returns(workoutExercise);
            RedirectToRouteResult result = controller.AddExerciseToWorkout(workoutExercise) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.RouteValues["id"], "workoutId was not 2");
            Assert.AreEqual("AddExerciseToWorkout", result.RouteValues["action"], "action was not AddExerciseToWorkoutr");
            Assert.AreEqual("Home", result.RouteValues["controller"], "controller was not Home");
        }

        /// <summary>
        /// Test if ExerciseList method returns list of 2 exersise_workouts for workout with id = 2
        /// </summary>
        [TestMethod]
        public void TestExerciseList()
        {
            PartialViewResult result = controller.ExerciseList(2);
            Assert.IsNotNull(result);
            List<workout_exercise> exs = (List<workout_exercise>)result.ViewData.Model;
            Assert.AreEqual(2, exs.Count(), "workout_exercise count was not 2");
        }

        
        [TestMethod]
        public void TestHomeControllerAddCommentOnWorkout()
        {
            comment comment = new comment();
            comment.message = "Test comment";
            comment.date_created = DateTime.Now;
            comment.User_id = 1;
            comment.Workout_id = 1;
            db.Setup(c => c.comments.Add(comment)).Returns(comment);
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewData.Model);

        }

        [TestMethod]
        public void TestHomeControllerAddCommentWithNoUserError()
        {
            controller = new HomeController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("not_a_real_user")
            };
            var comment = new comment();
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Comment could not be added.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddCommentWithValidationErrs()
        {
            var comment = new comment();
            controller.ModelState.AddModelError("Fail", "Failed");
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Invalid comment.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddNoCommentPassedError()
        {
            ViewResult result = controller.AddComment(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No comment to add was specified.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddCommentWithBadCommentObj()
        {
            var comment = new comment();
            var ex = new Exception();
            db.Setup(c => c.comments.Add(comment)).Throws(ex);
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Comment could not be added.", model.StatusDescription);
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
        public void TestHomeControllerCreateNoWorkoutPassedError()
        {
            ViewResult result = controller.Create(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Workout could not be created. Please try again.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateWorkoutWithNoUserError()
        {
            controller = new HomeController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("not_a_real_user")
            };
            var workout = new workout();
            ViewResult result = controller.Create(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("No user could be associated with the workout being created", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateWorkoutWithBadWorkoutObj()
        {
            var workout = new workout();
            var ex = new Exception();
            db.Setup(c => c.workouts.Add(workout)).Throws(ex);
            ViewResult result = controller.Create(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to create the requested workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerCreateWorkoutWithValidationErrs()
        {
            var workout = new workout();
            controller.ModelState.AddModelError("Fail", "Failed");
            ViewResult result = controller.Create(workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Could not create the workout with the given values.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddExerciseToWorkoutWithNullId()
        {
            int? number = null;
            ViewResult result = controller.AddExerciseToWorkout(number) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No exercise to add was specified.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddExerciseToWorkoutCannotFindWorkoutToAddExTo()
        {
            ViewResult result = controller.AddExerciseToWorkout(6000) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("Workout to add exercise to could not be found.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddExerciseToWorkoutWithNullEx()
        {
            workout_exercise w_ex = null;
            ViewResult result = controller.AddExerciseToWorkout(w_ex) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No exercise to add was specified.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddExerciseToWorkoutWithInvalidModel()
        {
            var w_ex = new workout_exercise();
            controller.ModelState.AddModelError("Fail", "Failed");
            ViewResult result = controller.AddExerciseToWorkout(w_ex) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Invalid exercise.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddExerciseToWorkoutDbException()
        {
            var w_ex = new workout_exercise();
            w_ex.position = 1;
            var ex = new Exception();
            db.Setup(c => c.workout_exercise.Add(w_ex)).Throws(ex);
            ViewResult result = controller.AddExerciseToWorkout(w_ex) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Exercise could not be added to the workout.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerGetMeasureWithNoExId()
        {
            ViewResult result = controller.GetMeasure(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No exercise to get a measure for was specified.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerGetMeasureWithBadExId()
        {
            ViewResult result = controller.GetMeasure(6000) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("Exercise could not be found.", model.StatusDescription);
        }

    }
}
