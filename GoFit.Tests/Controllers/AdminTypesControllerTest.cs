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
    /// Tests on the AdminTypesController class
    /// </summary>
    [TestClass]
    public class AdminTypesControllerTest
    {
        private AdminTypesController adminCon;
        private Mock<masterEntities> db;
        private TypeSearch search;

        /// <summary>
        /// Test setup code to be run prior to each
        /// test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new TypeSearch();

            db = contextHelpers.getDbContext();
            adminCon = new AdminTypesController()
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

        /// <summary>
        /// Test the types are sorted ascending upon passing
        /// sortBy "name" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminTypesNameAsc()
        {
            string sortBy = "name";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var types = (PagedList<type>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(types, "name", "asc");
            Assert.IsTrue(isSortedAsc);

        }

        /// <summary>
        /// Test that the types are returned and sorted in descending
        /// order upon passing "name_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminTypesNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var types = (PagedList<type>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(types, "name", "desc"));
        }

        /// <summary>
        /// Test the types are sorted ascending upon passing
        /// sortBy "measure" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminTypesMeasureAsc()
        {
            string sortBy = "measure";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var types = (PagedList<type>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(types, "measure", "asc");
            Assert.IsTrue(isSortedAsc);

        }

        /// <summary>
        /// Test that the types are returned and sorted in descending
        /// order upon passing "measure_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminTypesMeasureDesc()
        {
            string sortBy = "measure_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var types = (PagedList<type>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(types, "measure", "desc"));
        }

        /// <summary>
        /// Test that the types are returned and sorted in descending
        /// order upon passing "time_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminTypesTimeDesc()
        {
            string sortBy = "time_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var types = (PagedList<type>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(types, "time", "desc"));
        }

        /// <summary>
        /// Test that the types are returned and sorted in ascending
        /// order upon passing "time" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminTypesTimeAsc()
        {
            string sortBy = "time";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var types = (PagedList<type>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(types, "time", "asc"));
        }

        /// <summary>
        /// Test that the AdminTypes Index view returns data
        /// </summary>
        [TestMethod]
        public void TestAdminTypesIndexViewRender()
        {
            ViewResult result = adminCon.Index(null, null, null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var types = (PagedList<type>)result.ViewData.Model;
            Assert.IsTrue(types.Count > 0);
        }

        /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if a type list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="types">The type list to check</param>
        /// <param name="propName">The type property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<type> types, string propName, string order)
        {
            int limit = (types.Count > 10) ? 11 : types.Count;
            for (int i = 1; i < limit; i++)
            {
                type currentType = types[i];
                type prevType = types[i - 1];
                int? res = null;
                if (propName == "name")
                {
                    res = String.Compare(prevType.name, currentType.name);
                }
                else if (propName == "measure")
                {
                    res = String.Compare(prevType.measure, currentType.measure);
                }
                else if (propName == "time")
                {
                    res = DateTime.Compare(prevType.timestamp, currentType.timestamp);
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
