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
    }
}
