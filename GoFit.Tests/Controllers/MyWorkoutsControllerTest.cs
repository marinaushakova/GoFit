using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Models;
using GoFit.Tests.MockContexts;
using Moq;
using System.Web.Mvc;
using PagedList;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class MyWorkoutsControllerTest
    {
        private MyWorkoutsController myWorkoutsCon;
        private Mock<masterEntities> db;
        private WorkoutSearch search;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();

            db = contextHelpers.getDbContext();
            search = new WorkoutSearch();
            myWorkoutsCon = new MyWorkoutsController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
            myWorkoutsCon.pageSize = 10;
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
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);
        }

        [TestMethod]
        public void TestMarkExerciseClickingSecondCheckbox()
        {
            JsonResult result = myWorkoutsCon.MarkExercise(1, 2) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);
        }

        [TestMethod]
        public void TestMarkExerciseClickingSecondCheckboxAfterFirst()
        {
            JsonResult result = myWorkoutsCon.MarkExercise(1, 1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);

            result = myWorkoutsCon.MarkExercise(1, 2) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);
        }

        [TestMethod]
        public void TestMarkExerciseFinishingExercise()
        {
            JsonResult result = myWorkoutsCon.MarkExercise(1, 1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);

            result = myWorkoutsCon.MarkExercise(1, 2) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);

            result = myWorkoutsCon.MarkExercise(1, 3) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["result"]);
            Assert.AreEqual("false", dict["error"]);
        }

        /// <summary>
        /// Tests the MyWorkouts controller Index method
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexViewRetrieval()
        {
            ViewResult result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(10, workouts.Count);
        }

        /// <summary>
        /// Tests that the controller properly sorts my workouts by name in ascending order
        /// with an empty string for the sortBy parameter
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByNameAsc()
        {
            string sortBy = "name";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(workouts, "name", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts my workouts by name in descending order
        /// with 'name_desc' for the sortBy parameter
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(workouts, "name", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by category in asc order
        /// with 'category' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByCategoryAsc()
        {
            string sortBy = "category";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(workouts, "category", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by category in desc order
        /// with 'category_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByCategoryDesc()
        {
            string sortBy = "category_desc";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(workouts, "category", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts my workouts by date created in asc order
        /// with 'date' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByDateCreatedAsc()
        {
            string sortBy = "date";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(workouts, "dateCreated", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts MY workouts by date created in asc order
        /// with 'date_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByDateCreatedDesc()
        {
            string sortBy = "date_desc";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(workouts, "dateCreated", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by username in asc order
        /// with 'user' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByUserAsc()
        {
            string sortBy = "user";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(workouts, "username", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts the workouts by username in desc order
        /// with 'user_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSortByUserDesc()
        {
            string sortBy = "user_desc";
            ViewResult result = myWorkoutsCon.Index("", sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(workouts, "username", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that searching for "1" returns all my workouts with "1" in their name 
        /// and the same for search string "2"
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSearchByWorkoutName()
        {
            myWorkoutsCon.pageSize = 10;
            search.name = "1";
            ViewResult result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 3);
            search.name = "2";
            result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 3);
        }

        /// <summary>
        /// Tests that searching for category "strength" returns all my workouts "Stength" as category 
        /// and the same for search string "endurance"
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSearchByCategoryName()
        {
            myWorkoutsCon.pageSize = 10;
            search.category = "strength";
            ViewResult result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(0, workouts.Count);
            search.category = "endurance";
            result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(10, workouts.Count);
        }

        /// <summary>
        /// Tests that searching for workouts where date it was added is "2015-06-15" 
        /// returns all my workouts with "2015-06-15" in their date 
        /// and the same for search with date "2015-06-17"
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSearchByDateAdded()
        {
            myWorkoutsCon.pageSize = 10;
            search.dateAdded = "2015-06-15";
            ViewResult result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 3);
            search.dateAdded = "2015-06-17";
            result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 4);
        }

        /// <summary>
        /// Tests that searching for workouts where username fo user who added it
        /// is "admin" returns all my workouts added by "admin" 
        /// and the same for search with username "jjones"
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexSearchByUsername()
        {
            myWorkoutsCon.pageSize = 10;
            search.username = "admin";
            ViewResult result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(0, workouts.Count);
            search.username = "jjones";
            result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(10, workouts.Count);
        }

    /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if my workouts list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="workouts">The workout list to check</param>
        /// <param name="propName">The workout property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<user_workout> workouts, string propName, string order)
        {
            int limit = (workouts.Count > 10) ? 11 : workouts.Count;
            for (int i = 1; i < limit; i++)
            {
                user_workout currentWorkout = workouts[i];
                user_workout prevWorkout = workouts[i - 1];
                int? res = null;
                if (propName == "name")
                {
                    res = String.Compare(prevWorkout.workout.name, currentWorkout.workout.name);
                }
                else if (propName == "category")
                {
                    res = String.Compare(prevWorkout.workout.category.name, currentWorkout.workout.category.name);
                }
                else if (propName == "dateCreated")
                {
                    res = DateTime.Compare(prevWorkout.workout.created_at, currentWorkout.workout.created_at);
                }
                else if (propName == "username")
                {
                    res = String.Compare(prevWorkout.workout.user.username, currentWorkout.workout.user.username);
                }
                else return false;

                if (order == "asc")
                {
                    if (res > 0) return false;
                }
                else if (order == "desc")
                {
                    if (res < 0) return false;
                }
            }
            return true;
        }
    }
}
