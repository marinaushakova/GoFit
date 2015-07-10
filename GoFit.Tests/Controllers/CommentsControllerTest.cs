using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Controllers;
using GoFit.Models;
using Moq;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Tests.MockContexts;
using System.Web.Mvc;

namespace GoFit.Tests.Controllers
{
    [TestClass]
    public class CommentsControllerTest
    {
        private CommentsController controller;
        private Mock<masterEntities> db;
        private WorkoutSearch search;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            search = new WorkoutSearch();

            db = contextHelpers.getDbContext();
            controller = new CommentsController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("jjones")
            };
        }


        [TestMethod]
        public void TestHomeControllerAddCommentOnWorkout()
        {
            comment comment = new comment();
            comment.message = "Test comment";
            comment.date_created = DateTime.Now;
            comment.User_id = 1;
            comment.Workout_id = 1;
            db.Setup(c => c.comments.Add(comment)).Returns(comment);
            RedirectToRouteResult result = controller.AddComment(comment) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"], "action was not Index");
            Assert.AreEqual("Home", result.RouteValues["controller"], "controller was not FavoriteWorkouts");

        }

        [TestMethod]
        public void TestHomeControllerAddCommentWithNoUserError()
        {
            controller = new CommentsController(db.Object)
            {
                ControllerContext = MockContext.AuthenticationContext("not_a_real_user")
            };
            var comment = new comment();
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Comment could not be added.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddCommentWithValidationErrs()
        {
            var comment = new comment();
            controller.ModelState.AddModelError("Fail", "Failed");
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Invalid comment.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddNoCommentPassedError()
        {
            ViewResult result = controller.AddComment(null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No comment to add was specified.", model.StatusDescription);
        }

        [TestMethod]
        public void TestHomeControllerAddCommentWithBadCommentObj()
        {
            var comment = new comment();
            var ex = new Exception();
            db.Setup(c => c.comments.Add(comment)).Throws(ex);
            PartialViewResult result = controller.AddComment(comment) as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("_ErrorPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("Comment could not be added.", model.StatusDescription);
        }
    }
}
