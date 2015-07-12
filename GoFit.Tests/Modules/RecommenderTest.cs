using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFit.Models;
using Moq;
using GoFit.Tests.MockSetupHelpers;
using GoFit.Controllers.ControllerHelpers;

namespace GoFit.Tests.Modules
{
    [TestClass]
    public class RecommenderTest
    {
        private Mock<masterEntities> db;
        private Recommender recommender;

        [TestInitialize]
        public void Initialize()
        {
            DbContextHelpers contextHelpers = new DbContextHelpers();
            db = contextHelpers.getDbContext();
            recommender = new Recommender(db.Object);
        }

        [TestMethod]
        public void TestRecommendReturnsCorrectWorkouts()
        {
            for (var i = 0; i < 5; i++)
            {
                var workout = recommender.Recommend(4);
                Assert.IsNotNull(workout);
                bool isWorkout29or31 = (workout.id == 29 || workout.id == 31) ? true : false;
                Assert.AreEqual(true, isWorkout29or31, "Workout id was not 29 or 31");
            }
        }

        [TestMethod]
        public void TestRecommendReturnsRandomWorkout() 
        {
            var workout = recommender.Recommend(1);
            Assert.IsNotNull(workout);
        }
    }
}
