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
using System.Data.Entity.Infrastructure;

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
            adminCon = new AdminTypesController(db.Object)
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }
        
        /// <summary>
        /// Test creating a type record
        /// </summary>
        [TestMethod]
        public void TestAdminCreateType()
        {
            type type = new type();
            type.id = 2;
            type.name = "TEST_DLJ";
            type.measure = "TEST_DLJ";
            //type.timestamp = DateTime.Now;
            db.Setup(c => c.types.Add(type)).Returns(type);
            RedirectToRouteResult result = adminCon.Create(type) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"], "redirect was not to Index");
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


        [TestMethod]
        public void TestAdminTypesDetailsWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Details(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No type to view was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesDetailsWithNotFoundType()
        {
            ViewResult result = adminCon.Details(6523) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The type could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesCreateThrowsException()
        {
            type t = new type();
            db.Setup(c => c.SaveChanges()).Throws(new Exception());
            ViewResult result = adminCon.Create(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to create the type. Please try again.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesEditWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No type to edit was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesGetEditWithNotFoundType()
        {
            int? id = 6042;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The type could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostEditTypeNotFound()
        {
            type t = new type();
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The type does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostEditWithNullType()
        {
            type t = null;
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit the type.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostEditWithConcurrencyException()
        {
            type t = new type()
            {
                id = 1
            };
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit type as another admin may have already updated this type", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostEditWithDbUpdateException()
        {
            type t = new type()
            {
                id = 1,
                name = "xfs",
                measure = "measure"
            };
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit type.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesDeleteWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No type to delete was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesGetDeleteWithNotFoundType()
        {
            int? id = 6042;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The type could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostDeleteTypeNotFound()
        {
            type t = new type();
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The type does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostDeleteWithNullType()
        {
            type t = null;
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the type.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostDeleteWithConcurrencyException()
        {
            type t = new type()
            {
                id = 1
            };
            db.Setup(c => c.types.Find(t.id)).Returns(t);
            db.Setup(c => c.types.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the type as another admin may have modified this type", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminTypesPostDeleteWithDbUpdateException()
        {
            type t = new type()
            {
                id = 1,
                name = "xfs",
                measure = "measure"
            };
            db.Setup(c => c.types.Find(t.id)).Returns(t);
            db.Setup(c => c.types.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the type as it may be referenced by another item.", model.StatusDescription);
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

