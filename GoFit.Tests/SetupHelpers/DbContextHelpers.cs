using GoFit.Models;
using GoFit.Tests.MockContexts;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GoFit.Tests.MockSetupHelpers
{
    /// <summary>
    /// Class to provide the needed contexts to the various controller unit tests
    /// </summary>
    public class DbContextHelpers
    {
        /// <summary>
        /// Sets up the mock context and gives it test data
        /// </summary>
        /// <returns>The mock context</returns>
        public Mock<masterEntities> getDbContext()
        {
            var exercises = new List<workout_exercise> {
                new workout_exercise {},
                new workout_exercise {},
                new workout_exercise {}
            };

            user user1 = new user
            {
                id = 1,
                username = "admin"
            };
            user user2 = new user
            {
                id = 2,
                username = "bob"
            };
            user user3 = new user
            {
                id = 3,
                username = "jjones"
            };

            var users = new List<user> { user1, user2, user3 }.AsQueryable();
            

            
            category category1 = new category
            {
                id = 1,
                name = "endurance",
                description = "Endurance workouts help"
            };
            category category2 = new category
            {
                id = 2,
                name = "strength",
                description = "Strength workouts build"
            };
            category category3 = new category
            {
                id = 3,
                name = "flexibility",
                description = "Flexibility workouts stretch"
            };

            var testWorkout1 = new workout
            {
                id = 1,
                name = "workout1",
                description = "desc1",
                category = category1,
                created_at = Convert.ToDateTime("2015-06-15"),
                created_by_user_id = 1,
                workout_exercise = exercises,
                user = user3
            };
            var testWorkout2 = new workout
            {
                id = 2,
                name = "workout2",
                description = "desc2",
                category = category1,
                created_at = Convert.ToDateTime("2015-06-16"),
                created_by_user_id = 3,
                workout_exercise = exercises,
                user = user3
            };
            var testWorkout3 = new workout
            {
                id = 3,
                name = "workout3",
                description = "desc3",
                category = category1,
                created_at = Convert.ToDateTime("2015-06-17"),
                created_by_user_id = 3,
                workout_exercise = exercises,
                user = user3
            };

            var user_workouts = new List<user_workout>
            {
                new user_workout { 
                    user_id = 2,
                    workout_id = 1,
                    id = 1,
                    workout = testWorkout1
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 2,
                    id = 2,
                    workout = testWorkout2
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 3,
                    workout = testWorkout3
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 4,
                    workout = testWorkout3
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 2,
                    id = 5,
                    workout = testWorkout2
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 1,
                    id = 6,
                    workout = testWorkout1
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 1,
                    id = 7,
                    workout = testWorkout1
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 8,
                    workout = testWorkout3
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 9,
                    workout = testWorkout3
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 2,
                    id = 10,
                    workout = testWorkout2
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 1,
                    id = 11,
                    workout = testWorkout1
                }

            }.AsQueryable();

            var workouts = getSeedWorkouts();
            //var user_workouts = getSeedUserWorkouts();

            var workoutMockset = new Mock<DbSetOverrideWorkoutsFind<workout>>() { CallBase = true };
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.Provider).Returns(workouts.Provider);
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.Expression).Returns(workouts.Expression);
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.ElementType).Returns(workouts.ElementType);
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.GetEnumerator()).Returns(workouts.GetEnumerator);

            var userWorkoutMockset = new Mock<DbSetOverrideUserWorkoutsFind<user_workout>>() { CallBase = true };
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.Provider).Returns(user_workouts.Provider);
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.Expression).Returns(user_workouts.Expression);
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.ElementType).Returns(user_workouts.ElementType);
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.GetEnumerator()).Returns(user_workouts.GetEnumerator);

            var userMockset = new Mock<DbSet<user>>() { CallBase = true };
            userMockset.As<IQueryable<user>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockset.As<IQueryable<user>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockset.As<IQueryable<user>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockset.As<IQueryable<user>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator);

            var mockContext = new Mock<masterEntities>();
            mockContext.Setup(c => c.workouts).Returns(workoutMockset.Object);
            mockContext.Setup(c => c.user_workout).Returns(userWorkoutMockset.Object);
            mockContext.Setup(c => c.users).Returns(userMockset.Object);
            return mockContext;
        }

        /// <summary>
        /// Gets the fake db workouts collection with its associated 
        /// entities declared inline
        /// </summary>
        /// <returns>An queryable list of workouts</returns>
        private IQueryable<workout> getSeedWorkouts()
        {
            category category1 = new category
            {
                id = 1,
                name = "endurance",
                description = "Endurance workouts help"
            };
            category category2 = new category
            {
                id = 2,
                name = "strength",
                description = "Strength workouts build"
            };
            category category3 = new category
            {
                id = 3,
                name = "flexibility",
                description = "Flexibility workouts stretch"
            };

            user user1 = new user
            {
                id = 1,
                username = "admin"
            };
            user user2 = new user
            {
                id = 2,
                username = "bob"
            };

            var workouts = new List<workout>
            {
                new workout { 
                    id = 1,
                    name = "workout1",
                    description = "desc1",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 2,
                    name = "workout2",
                    description = "desc2",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 3,
                    name = "workout3",
                    description = "desc3",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 4,
                    name = "workout4",
                    description = "desc4",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 5,
                    name = "workout5",
                    description = "desc5",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 6,
                    name = "workout6",
                    description = "desc6",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 7,
                    name = "workout7",
                    description = "desc7",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 8,
                    name = "workout8",
                    description = "desc8",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 9,
                    name = "workout9",
                    description = "desc9",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 10,
                    name = "workout10",
                    description = "desc10",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 11,
                    name = "workout11",
                    description = "desc11",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 12,
                    name = "workout12",
                    description = "desc12",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 13,
                    name = "workout13",
                    description = "desc13",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = category1,
                    user = user1
                },
                new workout { 
                    id = 14,
                    name = "workout14",
                    description = "desc14",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = category1,
                    user = user1
                },
                new workout { 
                    id = 15,
                    name = "workout15",
                    description = "desc15",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = category1,
                    user = user1
                },
                new workout { 
                    id = 16,
                    name = "workout16",
                    description = "desc16",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = category2,
                    user = user2
                },
                new workout { 
                    id = 17,
                    name = "workout17",
                    description = "desc17",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = category2,
                    user = user2
                },
                new workout { 
                    id = 18,
                    name = "workout18",
                    description = "desc18",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = category2,
                    user = user2
                },
                new workout { 
                    id = 19,
                    name = "workout19",
                    description = "desc19",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 20,
                    name = "workout20",
                    description = "desc20",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 21,
                    name = "workout21",
                    description = "desc21",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = category1,
                    user = user2
                },
                new workout { 
                    id = 22,
                    name = "workout22",
                    description = "desc22",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 23,
                    name = "workout23",
                    description = "desc23",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = category2,
                    user = user1
                },
                new workout { 
                    id = 24,
                    name = "workout24",
                    description = "desc24",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = category2,
                    user = user1
                }
            }.AsQueryable();

            return workouts;
        }

    }
}
