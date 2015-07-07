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
    /// Tests on the AdminCommentsController class
    /// </summary>
    [TestClass]
    public class AdminCommentsControllerTest
    {
        private AdminCommentsController adminCon;
        private Mock<masterEntities> db;
        private CommentSearch search;

        /// <summary>
        /// Test setup code to be run prior to each
        /// test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new CommentSearch();

            db = contextHelpers.getDbContext();
            adminCon = new AdminCommentsController(db.Object)
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

        #region David's Tests

        /// <summary>
        /// Test that AdminComments Details view returns ViewData-
        /// a record from the Comment model
        /// </summary>
        [TestMethod]
        public void TestAdminCommentsDetailsViewReturnsData()
        {
            var result = adminCon.Details(1) as ViewResult;
            var comment = (comment)result.ViewData.Model;
            Assert.IsNotNull(result);
            Assert.AreEqual("Comment1", comment.message);
        }

        /// <summary>
        /// Test that AdminComments Delete view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminCommentsDeleteViewReturnsData()
        {
            var result = adminCon.Delete(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        #endregion

        /// <summary>
        /// Test the comments are sorted ascending upon passing
        /// sortBy "username" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminCommentsUserNameAsc()
        {
            string sortBy = "username";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var comments = (PagedList<comment>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(comments, "username", "asc");
            Assert.IsTrue(isSortedAsc);

        }

        /// <summary>
        /// Test that the comments are returned and sorted in descending
        /// order upon passing "username_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminCommentsUserNameDesc()
        {
            string sortBy = "username_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var comments = (PagedList<comment>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(comments, "username", "desc"));
        }

        /// <summary>
        /// Test that the comments are returned and sorted in descending
        /// order upon passing "message_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminCommentsMessageDesc()
        {
            string sortBy = "message_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var comments = (PagedList<comment>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(comments, "message", "desc"));
        }

        /// <summary>
        /// Test that the comments are returned and sorted in ascending
        /// order upon passing "message" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminCommentsMessageAsc()
        {
            string sortBy = "message";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var comments = (PagedList<comment>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(comments, "message", "asc"));
        }

        /// <summary>
        /// Test that the AdminComments Index view returns data
        /// </summary>
        [TestMethod]
        public void TestAdminCommentsIndexViewRender()
        {
            ViewResult result = adminCon.Index(null, null, null, search) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var comments = (PagedList<comment>)result.ViewData.Model;
            Assert.IsTrue(comments.Count > 0);
        }

        [TestMethod]
        public void TestAdminCommentsDetailsWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Details(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No comment to view was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsDetailsWithNotFoundComment()
        {
            ViewResult result = adminCon.Details(6523) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The comment could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsDeleteWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No comment to delete was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsGetDeleteWithNotFoundComment()
        {
            int? id = 6042;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The comment could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsPostDeleteCommentNotFound()
        {
            comment t = new comment();
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The comment does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsPostDeleteWithNullComment()
        {
            comment t = null;
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the comment.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsPostDeleteWithConcurrencyException()
        {
            comment t = new comment()
            {
                id = 1
            };
            db.Setup(c => c.comments.Find(t.id)).Returns(t);
            db.Setup(c => c.comments.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the comment as another admin may have modified it", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminCommentsPostDeleteWithDbUpdateException()
        {
            comment t = new comment()
            {
                id = 1
            };
            db.Setup(c => c.comments.Find(t.id)).Returns(t);
            db.Setup(c => c.comments.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the comment as it may be referenced in the database.", model.StatusDescription);
        }

        /* Private Test Helpers */

        /// <summary>
        /// Helper method to determin if a comment list is sorted in a certain order on a certain property
        /// </summary>
        /// <param name="comments">The comment list to check</param>
        /// <param name="propName">The comment property that the list should be sorted by</param>
        /// <param name="order">One of "asc" or "desc". Tells the method to check if the list is in ascending or descending order</param>
        /// <returns>True if the list is sorted on the given property in the given order, false otherwise</returns>
        private bool isSorted(PagedList<comment> comments, string propName, string order)
        {
            int limit = (comments.Count > 10) ? 11 : comments.Count;
            for (int i = 1; i < limit; i++)
            {
                comment currentComment = comments[i];
                comment prevComment = comments[i - 1];
                int? res = null;
                if (propName == "username")
                {
                    res = String.Compare(prevComment.user.username, currentComment.user.username);
                }
                else if (propName == "message")
                {
                    res = String.Compare(prevComment.message, currentComment.message);
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

