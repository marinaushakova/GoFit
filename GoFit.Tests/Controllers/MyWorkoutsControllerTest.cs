using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Models;
using GoFit.Tests.MockContexts;
using Moq;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Collections.Generic;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class MyWorkoutsControllerTest
    {
        private MyWorkoutsController myWorkoutsCon;
        private Mock<masterEntities> db;
        private WorkoutSearch search;
        private user_workout uWorkout;
        private byte[] ts; 

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

            ts = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            uWorkout = new user_workout()
            {
                id = 1,
                number_of_ex_completed = 0,
                workout_id = 1,
                user_id = 2,
                timestamp = ts,
                workout = new workout()
                {
                    workout_exercise = new List<workout_exercise>
                    {
                        {new workout_exercise()},
                        {new workout_exercise()},
                        {new workout_exercise()},
                        {new workout_exercise()},
                    }
                }
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
            string timestamp = "0 0 0 0 0 0 0 0";
            db.Setup(c => c.user_workout.Find(1)).Returns(uWorkout);
            JsonResult result = myWorkoutsCon.MarkExercise(1, 1, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["success"]);
            Assert.AreEqual("false", dict["error"]);
        }

        [TestMethod]
        public void TestMarkExerciseClickingSecondCheckbox()
        {
            string timestamp = "0 0 0 0 0 0 0 0";
            db.Setup(c => c.user_workout.Find(1)).Returns(uWorkout);
            JsonResult result = myWorkoutsCon.MarkExercise(1, 2, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["success"]);
            Assert.AreEqual("false", dict["error"]);
        }

        [TestMethod]
        public void TestMarkExerciseClickingLastBoxFirst()
        {
            string timestamp = "0 0 0 0 0 0 0 0";
            db.Setup(c => c.user_workout.Find(1)).Returns(uWorkout);
            JsonResult result = myWorkoutsCon.MarkExercise(1, 4, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("true", dict["success"]);
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
            Assert.AreEqual(3, workouts.Count);
            search.dateAdded = "2015-06-13";
            result = myWorkoutsCon.Index("", "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(4, workouts.Count);
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

        /// <summary>
        /// Tests that filtering MyWorkouts by its completion status being
        /// "not_started", "in_progress" and "completed" and checks if correct number
        /// of workouts is returned
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexFilterByCompletionStatus()
        {
            myWorkoutsCon.pageSize = 10;
            string filterString = "not_started";
            ViewResult result = myWorkoutsCon.Index(filterString, "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(7, workouts.Count);

            filterString = "in_progress";
            result = myWorkoutsCon.Index(filterString, "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(2, workouts.Count);

            filterString = "completed";
            result = myWorkoutsCon.Index(filterString, "", null, search) as ViewResult;
            Assert.IsNotNull(result);
            workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.AreEqual(1, workouts.Count);
        }

        /// <summary>
        /// Tests the MyWorkouts are fitlered by "not_started", 
        /// created by user with username "jjones" and 
        /// sorted by workout name in descending order
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsIndexFilterByNotStartedSearchUsernameAndSortWorkoutNameDesc()
        {
            myWorkoutsCon.pageSize = 10;
            search.username = "jjones";
            ViewResult result = myWorkoutsCon.Index("not_started", "name_desc", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var workouts = (PagedList<user_workout>)result.ViewData.Model;
            Assert.IsTrue(workouts.Count == 7);
            var isSortedDesc = this.isSorted(workouts, "name", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Checks if details view is returned for workout1 when
        /// user_workout with id=1 is chosen
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsDetailsForWorkout1()
        {
            user_workout u_workout = new user_workout();
            u_workout.id = 1;
            byte[] timestamp = new byte[8];
            for (var i = 0; i < timestamp.Length; i++)
            {
                timestamp[i] = 0;
            }
            u_workout.timestamp = timestamp;
            u_workout.workout = new workout
            {
                name = "workout1"
            };
            db.Setup(c => c.user_workout.Find(u_workout.id)).Returns(u_workout);
            ViewResult result = myWorkoutsCon.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            workout workout1 = (workout)result.ViewData.Model;
            Assert.AreEqual("workout1", workout1.name, "Name was not 'workout1'");
        }

        /// <summary>
        /// Checks if details view is returned for workout2 when
        /// user_workout with id=10 is chosen
        /// </summary>
        [TestMethod]
        public void TestMyWorkoutsDetailsForWorkout24()
        {
            user_workout u_workout = new user_workout();
            u_workout.id = 10;
            byte[] timestamp = new byte[8];
            for (var i = 0; i < timestamp.Length; i++)
            {
                timestamp[i] = 0;
            }
            u_workout.timestamp = timestamp;
            u_workout.workout = new workout
            {
                description = "desc2"
            };
            db.Setup(c => c.user_workout.Find(u_workout.id)).Returns(u_workout);
            ViewResult result = myWorkoutsCon.Details(10) as ViewResult;
            Assert.IsNotNull(result);
            workout workout10 = (workout)result.ViewData.Model;
            Assert.AreEqual("desc2", workout10.description, "description was not 'desc24'");
        }

        [TestMethod]
        public void TestMyWorkoutsDeleteMyWorkout()
        {
            user_workout u_workout = new user_workout();
            u_workout.id = 2;
            u_workout.workout = new workout()
            {
                id = 2
            };
            byte[] timestamp = new byte[8];
            for (var i = 0; i < timestamp.Length; i++)
            {
                timestamp[i] = 0;
            }
            u_workout.timestamp = timestamp;
            db.Setup(c => c.user_workout.Find(u_workout.id)).Returns(u_workout);
            db.Setup(c => c.user_workout.Remove(u_workout)).Returns(u_workout);
            RedirectToRouteResult result = myWorkoutsCon.DeleteFromMyWorkouts(u_workout) as RedirectToRouteResult;
            RedirectToRouteResult v_result = myWorkoutsCon.Details(2) as RedirectToRouteResult;
            Assert.IsNull(v_result);
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"], "action was not Index");
            Assert.AreEqual("MyWorkouts", result.RouteValues["controller"], "controller was not MyWorkouts");
        }

        [TestMethod]
        public void TestMyWorkoutsDetailsNoIdPassed()
        {
            ViewResult result = myWorkoutsCon.Details(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Workout could not be retrieved with given parameters.", model.StatusDescription);
        }

        [TestMethod]
        public void TestMyWorkoutsDetailsBadIdPassed()
        {
            ViewResult result = myWorkoutsCon.Details(50404) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("Your workout could not be found.", model.StatusDescription);
        }

        [TestMethod]
        public void TestMyWorkoutsMarkWithBadId()
        {
            string timestamp = "0 0 0 0 0 0 0 0";
            JsonResult result = myWorkoutsCon.MarkExercise(50405, 4, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("4", dict["position"]);
            Assert.AreEqual("true", dict["error"]);
            Assert.AreEqual("Failed to mark progress as the workout does not exist or may have been deleted", dict["message"]);
            Assert.AreEqual("500", dict["code"]);
        }

        [TestMethod]
        public void TestMyWorkoutsMarkWithConcurrencyException()
        {
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            string timestamp = "0 0 0 0 0 0 0 0";
            JsonResult result = myWorkoutsCon.MarkExercise(1, 4, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("4", dict["position"]);
            Assert.AreEqual("true", dict["error"]);
            Assert.AreEqual("Failed to mark progress as the workout may have already been updated", dict["message"]);
            Assert.AreEqual("500", dict["code"]);
        }

        [TestMethod]
        public void TestMyWorkoutsMarkWithUpdateException()
        {
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            string timestamp = "0 0 0 0 0 0 0 0";
            JsonResult result = myWorkoutsCon.MarkExercise(1, 4, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("4", dict["position"]);
            Assert.AreEqual("true", dict["error"]);
            Assert.AreEqual("Failed to mark progress as the workout may have already been updated", dict["message"]);
            Assert.AreEqual("500", dict["code"]);
        }

        [TestMethod]
        public void TestMyWorkoutsMarkWithOtherException()
        {
            db.Setup(c => c.SaveChanges()).Throws(new Exception());
            string timestamp = "0 0 0 0 0 0 0 0";
            JsonResult result = myWorkoutsCon.MarkExercise(1, 4, timestamp) as JsonResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("Dictionary`2", result.Data.GetType().Name);
            var dict = result.Data as System.Collections.Generic.Dictionary<string, string>;
            Assert.AreEqual("4", dict["position"]);
            Assert.AreEqual("true", dict["error"]);
            Assert.AreEqual("Failed to mark workout progress", dict["message"]);
            Assert.AreEqual("500", dict["code"]);
        }

        [TestMethod]
        public void TestMyWorkoutsAddWorkoutWithBadWorkout()
        {
            user_workout u_workout = new user_workout();
            ViewResult result = myWorkoutsCon.AddToMyWorkouts(u_workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to add the requested workout to user workouts.", model.StatusDescription);
        }

        [TestMethod]
        public void TestMyWorkoutsDeleteWorkoutNotFound()
        {
            user_workout u_workout = new user_workout();
            u_workout.id = 50406;
            ViewResult result = myWorkoutsCon.DeleteFromMyWorkouts(u_workout) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The workout does not exist or has already been deleted", model.StatusDescription);
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
