﻿using System;
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
    /// Tests on the AdminExercisesController class
    /// </summary>
    [TestClass]
    public class AdminExercisesControllerTest
    {
        private AdminExercisesController adminCon;
        private Mock<masterEntities> db;
        private ExerciseSearch search;

        /// <summary>
        /// Test setup code to be run prior to each
        /// test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new ExerciseSearch();

            db = contextHelpers.getDbContext();
            adminCon = new AdminExercisesController()
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

        /// <summary>
        /// Test the exercises are sorted ascending upon passing
        /// sortBy "name" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesNameAsc()
        {
            string sortBy = "name";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(exercises, "name", "asc");
            Assert.IsTrue(isSortedAsc);

        }

        /// <summary>
        /// Test that the exercises are returned and sorted in descending
        /// order upon passing "name_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(exercises, "name", "desc"));
        }

        /// <summary>
        /// Test that the exercises are returned and sorted in descending
        /// order upon passing "description_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesDescriptionDesc()
        {
            string sortBy = "description_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(exercises, "description", "desc"));
        }

        /// <summary>
        /// Test that the exercises are returned and sorted in ascending
        /// order upon passing "description" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesDescriptionAsc()
        {
            string sortBy = "description";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(exercises, "description", "asc"));
        }

        /// <summary>
        /// Test that the exercises are returned and sorted in descending
        /// order upon passing "time_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesTimeDesc()
        {
            string sortBy = "time_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(exercises, "time", "desc"));
        }

        /// <summary>
        /// Test that the exercises are returned and sorted in ascending
        /// order upon passing "time" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesTimeAsc()
        {
            string sortBy = "time";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(exercises, "time", "asc"));
        }

        /// <summary>
        /// Test that the AdminExercises Index view returns data
        /// </summary>
        [TestMethod]
        public void TestAdminExercisesIndexViewRender()
        {
            ViewResult result = adminCon.Index(null, null, null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(exercises.Count > 0);
        }

        /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if a exercise list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="exercises">The exercise list to check</param>
        /// <param name="propName">The exercise property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<exercise> exercises, string propName, string order)
        {
            int limit = (exercises.Count > 10) ? 11 : exercises.Count;
            for (int i = 1; i < limit; i++)
            {
                exercise currentExercise = exercises[i];
                exercise prevExercise = exercises[i - 1];
                int? res = null;
                if (propName == "name")
                {
                    res = String.Compare(prevExercise.name, currentExercise.name);
                }
                else if (propName == "date")
                {
                    res = DateTime.Compare(prevExercise.created_at, currentExercise.created_at);
                }
                else if (propName == "link")
                {
                    res = String.Compare(prevExercise.link, currentExercise.link);
                }
                else if (propName == "description")
                {
                    res = String.Compare(prevExercise.description, currentExercise.description);
                }
                else if (propName == "time")
                {
                    res = DateTime.Compare(prevExercise.timestamp, currentExercise.timestamp);
                }
                else if (propName == "type")
                {
                    res = String.Compare(prevExercise.type.name, currentExercise.type.name);
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