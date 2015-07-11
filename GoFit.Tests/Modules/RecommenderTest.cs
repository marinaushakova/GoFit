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

        }
    }
}
