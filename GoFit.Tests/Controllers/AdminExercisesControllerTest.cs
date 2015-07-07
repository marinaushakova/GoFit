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
            adminCon = new AdminExercisesController(db.Object)
            {
                // sign in as admin
                ControllerContext = MockContext.AuthenticationContext("admin")
            };
        }

        #region David's Tests

        /// <summary>
        /// Test that AdminExercises Details view returns ViewData-
        /// a record from the Exercise model
        /// </summary>
        [TestMethod]
        public void TestAdminExercisesDetailsViewReturnsData()
        {
            var result = adminCon.Details(1) as ViewResult;
            var exercise = (exercise)result.ViewData.Model;
            Assert.IsNotNull(result);
            Assert.AreEqual("ex1", exercise.name);
        }

        /// <summary>
        /// Test that AdminExercises Create view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminExercisesCreateViewReturnsData()
        {
            var result = adminCon.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test that AdminExercises Edit view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminExercisesEditViewReturnsData()
        {
            var result = adminCon.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test that AdminExercises Editing a exercise redirects to Index
        /// </summary>
        [TestMethod]
        public void TestAdminExercisesEditExercise()
        {
            var result = adminCon.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            var testExercise = (exercise)result.ViewData.Model;
            testExercise.description = "TEST_DLJ";
            var result2 = (RedirectToRouteResult)adminCon.Edit(testExercise);
            Assert.AreEqual("Index", result2.RouteValues["action"]);
        }

        /// <summary>
        /// Test that AdminExercises Delete view returns a ViewResult
        /// </summary>
        [TestMethod]
        public void TestAdminExercisesDeleteViewReturnsData()
        {
            var result = adminCon.Delete(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        #endregion

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
        /// Test the exercises are sorted ascending upon passing
        /// sortBy "username" to the index view
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesUserNameAsc()
        {
            string sortBy = "username";
            // controller.action(args) as ViewResult
            //  -gives a resulting view object
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            var isSortedAsc = this.isSorted(exercises, "username", "asc");
            Assert.IsTrue(isSortedAsc);

        }

        /// <summary>
        /// Test that the exercises are returned and sorted in descending
        /// order upon passing "username_desc" to the Index
        /// </summary>
        [TestMethod]
        public void TestSortAdminExercisesUserNameDesc()
        {
            string sortBy = "username_desc";
            ViewResult result = adminCon.Index(null, sortBy, null, search) as ViewResult;
            Assert.IsNotNull(result);
            var exercises = (PagedList<exercise>)result.ViewData.Model;
            Assert.IsTrue(this.isSorted(exercises, "username", "desc"));
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


        [TestMethod]
        public void TestAdminExercisesDetailsWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Details(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No exercise to view was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesDetailsWithNotFoundExercise()
        {
            ViewResult result = adminCon.Details(6523) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The exercise could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesCreateThrowsException()
        {
            exercise t = new exercise();
            db.Setup(c => c.SaveChanges()).Throws(new Exception());
            ViewResult result = adminCon.Create(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to create the exercise.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesEditWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No exercise to edit was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesGetEditWithNotFoundExercise()
        {
            int? id = 6042;
            ViewResult result = adminCon.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The exercise could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostEditExerciseNotFound()
        {
            exercise t = new exercise();
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The exercise does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostEditWithNullExercise()
        {
            exercise t = null;
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit the exercise.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostEditWithConcurrencyException()
        {
            exercise t = new exercise()
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
            Assert.AreEqual("Failed to edit exercise as another admin may have already updated this exercise", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostEditWithDbUpdateException()
        {
            exercise t = new exercise()
            {
                id = 1
            };
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.Edit(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to edit exercise.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesDeleteWithNullId()
        {
            int? id = null;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(400, model.StatusCode);
            Assert.AreEqual("No exercise to delete was specified", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesGetDeleteWithNotFoundExercise()
        {
            int? id = 6042;
            ViewResult result = adminCon.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(404, model.StatusCode);
            Assert.AreEqual("The exercise could not be found or does not exist", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostDeleteExerciseNotFound()
        {
            exercise t = new exercise();
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("The exercise does not exist or has already been deleted", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostDeleteWithNullExercise()
        {
            exercise t = null;
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the exercise.", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostDeleteWithConcurrencyException()
        {
            exercise t = new exercise()
            {
                id = 1
            };
            db.Setup(c => c.exercises.Find(t.id)).Returns(t);
            db.Setup(c => c.exercises.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the exercise as another admin may have modified it", model.StatusDescription);
        }

        [TestMethod]
        public void TestAdminExercisesPostDeleteWithDbUpdateException()
        {
            exercise t = new exercise()
            {
                id = 1
            };
            db.Setup(c => c.exercises.Find(t.id)).Returns(t);
            db.Setup(c => c.exercises.Remove(t)).Returns(t);
            db.Setup(c => c.SaveChanges()).Throws(new DbUpdateException());
            ViewResult result = adminCon.DeleteConfirmed(t) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailedError", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(HttpStatusCodeResult));
            var model = result.Model as HttpStatusCodeResult;
            Assert.AreEqual(500, model.StatusCode);
            Assert.AreEqual("Failed to delete the exercise as it may be referenced by another item.", model.StatusDescription);
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
                else if (propName == "username")
                {
                    res = String.Compare(prevExercise.user.username, currentExercise.user.username);
                }
                else if (propName == "link")
                {
                    res = String.Compare(prevExercise.link, currentExercise.link);
                }
                else if (propName == "description")
                {
                    res = String.Compare(prevExercise.description, currentExercise.description);
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
