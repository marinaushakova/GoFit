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
    /// Tests on the AdminCategoriesController class
    /// </summary>
    [TestClass]
    public class AdminCategoriesControllerTest
    {
        private AdminCategoriesController adminCon;
        private Mock<masterEntities> db;
        private CategorySearch search;

        /// <summary>
        /// Test setup code to be run prior to each
        /// test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new CategorySearch();

            db = contextHelpers.getDbContext();
            adminCon = new AdminCategoriesController(db.Object)
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

        /// <summary>
        /// Test the categories are sorted ascending upon passing
        /// sortBy "name" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminCategoriesNameAsc()
        {
            string sortBy = "name";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var categories = (PagedList<category>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(categories, "name", "asc");
            Assert.IsTrue(isSortedAsc);

        }

        /// <summary>
        /// Test that the categories are returned and sorted in descending
        /// order upon passing "name_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminCategoriesNameDesc()
        {
            string sortBy = "name_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var categories = (PagedList<category>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(categories, "name", "desc"));
        }

        /// <summary>
        /// Test that the categories are returned and sorted in descending
        /// order upon passing "description_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminCategoriesDescriptionDesc()
        {
            string sortBy = "description_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var categories = (PagedList<category>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(categories, "description", "desc"));
        }

        /// <summary>
        /// Test that the categories are returned and sorted in ascending
        /// order upon passing "description" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminCategoriesDescriptionAsc()
        {
            string sortBy = "description";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var categories = (PagedList<category>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(categories, "description", "asc"));
        }

        /// <summary>
        /// Test that the AdminCategories Index view returns data
        /// </summary>
        [TestMethod]
        public void TestAdminCategoriesIndexViewRender()
        {
            ViewResult result = adminCon.Index(null, null, null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var categories = (PagedList<category>)result.ViewData.Model;
            Assert.IsTrue(categories.Count > 0);
        }


        [TestMethod]
        public void TestAdminCategoriesDetailsWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Details(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No category to view was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesDetailsWithNotFoundCategory()
        {
            ViewResult result = adminCon.Details(6523) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The category could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesCreateThrowsException()
        {
            category t = new category();
            db.Setup(c => c.SaveChanges()).Throws(new Exception());
            ViewResult result = adminCon.Create(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to create the category.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesEditWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No category to edit was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesGetEditWithNotFoundCategory()
        {
            int? id = 6042;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The category could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostEditCategoriesNotFound()
        {
            category t = new category();
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The category does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostEditWithNullExercise()
        {
            category t = null;
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit the category.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostEditWithConcurrencyException()
        {
            category t = new category()
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
            Assert.AreEqual("Failed to edit category as another admin may have already updated this category", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostEditWithDbUpdateException()
        {
            category t = new category()
            {
                id = 1,
                name = "someCat",
                description = "Some desc",
                timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0}
            };
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit category.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesDeleteWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No category to delete was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesGetDeleteWithNotFoundCategory()
        {
            int? id = 6042;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The category could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostDeleteCategoryNotFound()
        {
            category t = new category();
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The category does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostDeleteWithNullCategory()
        {
            category t = null;
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the category.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostDeleteWithConcurrencyException()
        {
            category t = new category()
            {
                id = 1
            };
            db.Setup(c => c.categories.Find(t.id)).Returns(t);
            db.Setup(c => c.categories.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the category as another admin may have modified this category", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCategoriesPostDeleteWithDbUpdateException()
        {
            category t = new category()
            {
                id = 1
            };
            db.Setup(c => c.categories.Find(t.id)).Returns(t);
            db.Setup(c => c.categories.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the category as it may be referenced in the database.", model.StatusDescription);
        }

        /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if a category list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="categories">The category list to check</param>
        /// <param name="propName">The category property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<category> categories, string propName, string order)
        {
            int limit = (categories.Count > 10) ? 11 : categories.Count;
            for (int i = 1; i < limit; i++)
            {
                category currentCategory = categories[i];
                category prevCategory = categories[i - 1];
                int? res = null;
                if (propName == "name")
                {
                    res = String.Compare(prevCategory.name, currentCategory.name);
                }
                else if (propName == "description")
                {
                    res = String.Compare(prevCategory.description, currentCategory.description);
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
