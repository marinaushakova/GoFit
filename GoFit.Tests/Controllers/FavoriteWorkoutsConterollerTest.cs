using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Models;
using Moq;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Tests.MockContexts;
using System.Web.Mvc;
using System.Collections.Generic;
using PagedList;

namespace GoFit.Tests.Controllers
{
    /// <summary>
    /// Tests the FavoriteWorkouts controller functionality
    /// </summary>
    [TestClass]
    public class FavoriteWorkoutsConterollerTest
    {
        
        private FavoriteWorkoutsController controller;
        private Mock<masterEntities> db;
        private WorkoutSearch search;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new WorkoutSearch();

            db = contextHelpers.getDbContext();
            controller = new FavoriteWorkoutsController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
            controller.pageSize = 10;
        }

        /// <summary>
        /// Test if FavoriteList method returns list of 2 user_favorite_workouts
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerFavoriteList()
        {
            PartialViewResult result = controller.FavoriteList();
            Assert.IsNotNull(result);
            List<user_favorite_workout> faves = (List<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(4, faves.Count, "user_favorite_workout count was not 4");
        }

        /// <summary>
        /// Tests the FavoriteWorkouts controller Index method
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexViewRetrieval()
        {
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.IsTrue(faves.Count == 4);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by name in ascending order
        /// with an empty string for the sortBy parameter
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByNameAsc()
        {
            string sortBy = "name";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(faves, "name", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by name in descending order
        /// with 'name_desc' for the sortBy parameter
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(faves, "name", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by category in asc order
        /// with 'category' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByCategoryAsc()
        {
            string sortBy = "category";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(faves, "category", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by category in desc order
        /// with 'category_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByCategoryDesc()
        {
            string sortBy = "category_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(faves, "category", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by date created in asc order
        /// with 'date' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByDateCreatedAsc()
        {
            string sortBy = "date";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(faves, "dateCreated", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by date created in asc order
        /// with 'date_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByDateCreatedDesc()
        {
            string sortBy = "date_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(faves, "dateCreated", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by username in asc order
        /// with 'user' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByUserAsc()
        {
            string sortBy = "user";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(faves, "username", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests that the controller properly sorts favorite workouts by username in desc order
        /// with 'user_desc' as the sortBy param
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSortByUserDesc()
        {
            string sortBy = "user_desc";
            ViewResult result = controller.Index(sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            var isSortedDesc = this.isSorted(faves, "username", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that searching for "1" returns all favorite workouts with "1" in their name 
        /// and the same for search string "2"
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSearchByWorkoutName()
        {
            search.name = "2";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.IsTrue(faves.Count == 2);
            search.name = "1";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.IsTrue(faves.Count == 3);
        }

        /// <summary>
        /// Tests that searching for category "strength" returns all favorite workouts "Stength" as category 
        /// and the same for search string "endurance"
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSearchByCategoryName()
        {
            search.category = "strength";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(0, faves.Count);
            search.category = "endurance";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(4, faves.Count);
        }

        /// <summary>
        /// Tests that searching for workouts where date it was added is "2015-06-15" 
        /// returns all my workouts with "2015-06-15" in their date 
        /// and the same for search with date "2015-06-17"
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSearchByDateAdded()
        {
            search.dateAdded = "2015-06-15";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(1, faves.Count);
            search.dateAdded = "2015-06-13";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(1, faves.Count);
        }

        /// <summary>
        /// Tests that searching for workouts where username fo user who added it
        /// is "admin" returns all my workouts added by "bob" 
        /// and the same for search with username "jjones"
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSearchByUsername()
        {
            search.username = "bob";
            ViewResult result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(2, faves.Count);
            search.username = "jjones";
            result = controller.Index("", null, search) as ViewResult;
            Assert.IsNotNull(result);
            faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(2, faves.Count);
        }

        /// <summary>
        /// Tests that searching favorites forkouts by username "bob" and 
        /// sorting them by name desc return all favorite workouts added by bob
        /// sorted by name in descending order
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSearchByUsernameAndSortByNameDesc()
        {
            controller.pageSize = 20;
            search.username = "bob";
            ViewResult result = controller.Index("name_desc", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.AreEqual(2, faves.Count);
            var isSortedDesc = this.isSorted(faves, "name", "desc");
            Assert.IsTrue(isSortedDesc);
        }

        /// <summary>
        /// Tests that searching favorites forkouts by name containing "1" and 
        /// sorting them by date asc return all favorite workouts with "1" in name
        /// sorted by dateCreated in ascending order
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerIndexSearchByNameSortByDateAddedAsc()
        {
            controller.pageSize = 20;
            search.name = "1";
            controller.Session["NameSearchParam"] = "1";
            ViewResult result = controller.Index("date", null, search) as ViewResult;
            Assert.IsNotNull(result);
            var faves = (PagedList<user_favorite_workout>)result.ViewData.Model;
            Assert.IsTrue(faves.Count == 3);
            var isSortedAsc = this.isSorted(faves, "dateCreated", "asc");
            Assert.IsTrue(isSortedAsc);
        }

        /// <summary>
        /// Tests AddWorkoutToFavorites method of FavoriteWorkouts controller
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerAddWorkoutToFavorites()
        {
            user_favorite_workout favWorkout = new user_favorite_workout();
            db.Setup(c => c.user_favorite_workout.Add(favWorkout)).Returns(favWorkout);
            RedirectToRouteResult result = controller.AddWorkoutToFavorites(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"], "action was not Index");
            Assert.AreEqual("FavoriteWorkouts", result.RouteValues["controller"], "controller was not FavoriteWorkouts");
        }
        /*
        /// <summary>
        /// Tests RemoveWorkoutFromFavorites method of FavoriteWorkouts controller
        /// </summary>
        [TestMethod]
        public void TestFavoriteWorkoutsControllerRemoveWorkoutFromFavorites()
        {
            user_favorite_workout favWorkout = new user_favorite_workout();
            favWorkout.id = 1;
            favWorkout.user = new user
            {
                id = 2
            };
            favWorkout.workout = new workout()
            {
                id = 1
            };
            byte[] timestamp = new byte[8];
            for (var i = 0; i < timestamp.Length; i++)
            {
                timestamp[i] = 0;
            }
            favWorkout.timestamp = timestamp;
            //db.Setup(c => c.user_favorite_workout.Find(favWorkout.id)).Returns(favWorkout);
            db.Setup(c => c.user_favorite_workout.Remove(favWorkout)).Returns(favWorkout);
            RedirectToRouteResult result = controller.RemoveWorkoutFromFavorites(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"], "action was not Index");
            Assert.AreEqual("FavoriteWorkouts", result.RouteValues["controller"], "controller was not FavoriteWorkouts");
        }
        */

        /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if my workouts list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="workouts">The workout list to check</param>
        /// <param name="propName">The workout property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<user_favorite_workout> workouts, string propName, string order)
        {
            int limit = (workouts.Count > 10) ? 11 : workouts.Count;
            for (int i = 1; i < limit; i++)
            {
                user_favorite_workout currentWorkout = workouts[i];
                user_favorite_workout prevWorkout = workouts[i - 1];
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
