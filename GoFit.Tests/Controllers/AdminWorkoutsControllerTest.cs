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
            adminCon = new AdminWorkoutsController()
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

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
            var isSortedAsc = this.isSorted(workouts, "name", "asc");
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
            Assert.IsTrue(this.isSorted(workouts, "name", "desc"));
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
            Assert.IsTrue(this.isSorted(workouts, "description", "desc"));
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
            Assert.IsTrue(this.isSorted(workouts, "description", "asc"));
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
            Assert.IsTrue(this.isSorted(workouts, "date", "asc"));
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
            Assert.IsTrue(this.isSorted(workouts, "date", "desc"));
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
            Assert.IsTrue(this.isSorted(workouts, "category", "asc"));
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
            Assert.IsTrue(this.isSorted(workouts, "category", "desc"));
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
            Assert.IsTrue(this.isSorted(workouts, "user", "desc"));
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
            Assert.IsTrue(this.isSorted(workouts, "user", "asc"));
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

        /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if a workout list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="workouts">The workout list to check</param>
        /// <param name="propName">The workout property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<workout> workouts, string propName, string order)
        {
            int limit = (workouts.Count > 10) ? 11 : workouts.Count;
            for (int i = 1; i < limit; i++)
            {
                workout currentWorkout = workouts[i];
                workout prevWorkout = workouts[i - 1];
                int? res = null;
                if (propName == "name")
                {
                    res = String.Compare(prevWorkout.name, currentWorkout.name);
                }
                else if (propName == "description")
                {
                    res = String.Compare(prevWorkout.description, currentWorkout.description);
                }
                else if (propName == "category")
                {
                    res = String.Compare(prevWorkout.category.name, currentWorkout.category.name);
                }
                else if (propName == "date")
                {
                    res = DateTime.Compare(prevWorkout.created_at, currentWorkout.created_at);
                }
                else if (propName == "time")
                {
                    res = DateTime.Compare(prevWorkout.timestamp, currentWorkout.timestamp);
                }
                else if (propName == "user")
                {
                    res = String.Compare(prevWorkout.user.username, currentWorkout.user.username);
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
