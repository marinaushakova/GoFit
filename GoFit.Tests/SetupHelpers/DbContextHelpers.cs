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
            List<category> categories = getSeedCategories();
            List<user> users = getSeedUsers();
            List<exercise> exercises = getSeedExercises();
            List<workout> workouts = getSeedWorkouts();
            List<workout_exercise> workoutExercises = getSeedWorkoutExercises();
            List<user_workout> userWorkouts = getSeedUserWorkouts();
            List<comment> comments = getSeedComments();
            List<user_favorite_workout> userFaves = getSeedUserFavWorkouts();
            List<workout_rating> workoutRating = getSeedWorkoutRatings();
            List<type> types = getSeedTypes();

            IQueryable<category> categoriesQ = categories.AsQueryable();
            IQueryable<user> usersQ = users.AsQueryable();
            IQueryable<exercise> exercisesQ = exercises.AsQueryable();
            IQueryable<workout> workoutsQ = workouts.AsQueryable();
            IQueryable<workout_exercise> workoutExercisesQ = workoutExercises.AsQueryable();
            IQueryable<user_workout> userWorkoutsQ = userWorkouts.AsQueryable();
            IQueryable<comment> commentsQ = comments.AsQueryable();
            IQueryable<user_favorite_workout> userFavesQ = userFaves.AsQueryable();
            IQueryable<workout_rating> workoutRatingQ = workoutRating.AsQueryable();
            IQueryable<type> typesQ = types.AsQueryable();

            var workoutMockset = new Mock<DbSetOverrideWorkoutsFind<workout>>() { CallBase = true };
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.Provider).Returns(workoutsQ.Provider);
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.Expression).Returns(workoutsQ.Expression);
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.ElementType).Returns(workoutsQ.ElementType);
            workoutMockset.As<IQueryable<workout>>().Setup(m => m.GetEnumerator()).Returns(workoutsQ.GetEnumerator);

            var userWorkoutMockset = new Mock<DbSetOverrideUserWorkoutsFind<user_workout>>() { CallBase = true };
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.Provider).Returns(userWorkoutsQ.Provider);
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.Expression).Returns(userWorkoutsQ.Expression);
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.ElementType).Returns(userWorkoutsQ.ElementType);
            userWorkoutMockset.As<IQueryable<user_workout>>().Setup(m => m.GetEnumerator()).Returns(userWorkoutsQ.GetEnumerator);

            var userMockset = new Mock<DbSetOverrideUserFind<user>>() { CallBase = true };
            userMockset.As<IQueryable<user>>().Setup(m => m.Provider).Returns(usersQ.Provider);
            userMockset.As<IQueryable<user>>().Setup(m => m.Expression).Returns(usersQ.Expression);
            userMockset.As<IQueryable<user>>().Setup(m => m.ElementType).Returns(usersQ.ElementType);
            userMockset.As<IQueryable<user>>().Setup(m => m.GetEnumerator()).Returns(usersQ.GetEnumerator);

            var categoryMockset = new Mock<DbSetOverrideCategoryFind<category>>() { CallBase = true };
            categoryMockset.As<IQueryable<category>>().Setup(m => m.Provider).Returns(categoriesQ.Provider);
            categoryMockset.As<IQueryable<category>>().Setup(m => m.Expression).Returns(categoriesQ.Expression);
            categoryMockset.As<IQueryable<category>>().Setup(m => m.ElementType).Returns(categoriesQ.ElementType);
            categoryMockset.As<IQueryable<category>>().Setup(m => m.GetEnumerator()).Returns(categoriesQ.GetEnumerator);

            var workoutExerciseMockset = new Mock<DbSet<workout_exercise>>() { CallBase = true };
            workoutExerciseMockset.As<IQueryable<workout_exercise>>().Setup(m => m.Provider).Returns(workoutExercisesQ.Provider);
            workoutExerciseMockset.As<IQueryable<workout_exercise>>().Setup(m => m.Expression).Returns(workoutExercisesQ.Expression);
            workoutExerciseMockset.As<IQueryable<workout_exercise>>().Setup(m => m.ElementType).Returns(workoutExercisesQ.ElementType);
            workoutExerciseMockset.As<IQueryable<workout_exercise>>().Setup(m => m.GetEnumerator()).Returns(workoutExercisesQ.GetEnumerator);

            var exerciseMockset = new Mock<DbSetOverrideExerciseFind<exercise>>() { CallBase = true };
            exerciseMockset.As<IQueryable<exercise>>().Setup(m => m.Provider).Returns(exercisesQ.Provider);
            exerciseMockset.As<IQueryable<exercise>>().Setup(m => m.Expression).Returns(exercisesQ.Expression);
            exerciseMockset.As<IQueryable<exercise>>().Setup(m => m.ElementType).Returns(exercisesQ.ElementType);
            exerciseMockset.As<IQueryable<exercise>>().Setup(m => m.GetEnumerator()).Returns(exercisesQ.GetEnumerator);

            var commentMockset = new Mock<DbSetOverrideCommentFind<comment>>() { CallBase = true };
            commentMockset.As<IQueryable<comment>>().Setup(m => m.Provider).Returns(commentsQ.Provider);
            commentMockset.As<IQueryable<comment>>().Setup(m => m.Expression).Returns(commentsQ.Expression);
            commentMockset.As<IQueryable<comment>>().Setup(m => m.ElementType).Returns(commentsQ.ElementType);
            commentMockset.As<IQueryable<comment>>().Setup(m => m.GetEnumerator()).Returns(commentsQ.GetEnumerator);

            var userFavMockset = new Mock<DbSetOverrideUserFavoriteWorkoutsFind<user_favorite_workout>>() { CallBase = true };
            userFavMockset.As<IQueryable<user_favorite_workout>>().Setup(m => m.Provider).Returns(userFavesQ.Provider);
            userFavMockset.As<IQueryable<user_favorite_workout>>().Setup(m => m.Expression).Returns(userFavesQ.Expression);
            userFavMockset.As<IQueryable<user_favorite_workout>>().Setup(m => m.ElementType).Returns(userFavesQ.ElementType);
            userFavMockset.As<IQueryable<user_favorite_workout>>().Setup(m => m.GetEnumerator()).Returns(userFavesQ.GetEnumerator);

            var workoutRatingMockset = new Mock<DbSetOverrideWorkoutRatingFind<workout_rating>>() { CallBase = true };
            workoutRatingMockset.As<IQueryable<workout_rating>>().Setup(m => m.Provider).Returns(workoutRatingQ.Provider);
            workoutRatingMockset.As<IQueryable<workout_rating>>().Setup(m => m.Expression).Returns(workoutRatingQ.Expression);
            workoutRatingMockset.As<IQueryable<workout_rating>>().Setup(m => m.ElementType).Returns(workoutRatingQ.ElementType);
            workoutRatingMockset.As<IQueryable<workout_rating>>().Setup(m => m.GetEnumerator()).Returns(workoutRatingQ.GetEnumerator);

            var typesMockset = new Mock<DbSetOverrideTypeFind<type>>() { CallBase = true };
            typesMockset.As<IQueryable<type>>().Setup(m => m.Provider).Returns(typesQ.Provider);
            typesMockset.As<IQueryable<type>>().Setup(m => m.Expression).Returns(typesQ.Expression);
            typesMockset.As<IQueryable<type>>().Setup(m => m.ElementType).Returns(typesQ.ElementType);
            typesMockset.As<IQueryable<type>>().Setup(m => m.GetEnumerator()).Returns(typesQ.GetEnumerator);

            var mockContext = new Mock<masterEntities>();
            mockContext.Setup(c => c.workouts).Returns(workoutMockset.Object);
            mockContext.Setup(c => c.user_workout).Returns(userWorkoutMockset.Object);
            mockContext.Setup(c => c.users).Returns(userMockset.Object);
            mockContext.Setup(c => c.categories).Returns(categoryMockset.Object);
            mockContext.Setup(c => c.workout_exercise).Returns(workoutExerciseMockset.Object);
            mockContext.Setup(c => c.exercises).Returns(exerciseMockset.Object);
            mockContext.Setup(c => c.comments).Returns(commentMockset.Object);
            mockContext.Setup(c => c.user_favorite_workout).Returns(userFavMockset.Object);
            mockContext.Setup(c => c.workout_rating).Returns(workoutRatingMockset.Object);
            mockContext.Setup(c => c.types).Returns(typesMockset.Object);
            return mockContext;
        }

        private List<type> getSeedTypes()
        {
            type type1 = new type
            {
                id = 1,
                measure = "measure",
                name = "type1",
                timestamp = new byte[] {0, 0, 0, 0, 0, 0, 0, 0}
            };

            var types = new List<type> { type1 };
            return types;
        }

        /// <summary>
        /// Gets the fake db workouts collection with its associated 
        /// entities declared inline
        /// </summary>
        /// <returns>An queryable list of workouts</returns>
        private List<workout> getSeedWorkouts()
        {
            List<category> categories = getSeedCategories();
            List<user> users = getSeedUsers();
            List<workout_rating> ratings = getSeedWorkoutRatings();

            var workouts = new List<workout>
            {
                new workout { 
                    id = 1,
                    name = "workout1",
                    description = "desc1",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = categories[0],
                    user = users[2]
                },
                new workout { 
                    id = 2,
                    name = "workout2",
                    description = "desc2",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = categories[0],
                    user = users[2]
                },
                new workout { 
                    id = 3,
                    name = "workout3",
                    description = "desc3",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = categories[0],
                    user = users[2]
                },
                new workout { 
                    id = 4,
                    name = "workout4",
                    description = "desc4",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = categories[1],
                    user = users[0]
                },
                new workout { 
                    id = 5,
                    name = "workout5",
                    description = "desc5",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = categories[1],
                    user = users[0]
                },
                new workout { 
                    id = 6,
                    name = "workout6",
                    description = "desc6",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = categories[1],
                    user = users[0]
                },
                new workout { 
                    id = 7,
                    name = "workout7",
                    description = "desc7",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 8,
                    name = "workout8",
                    description = "desc8",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 9,
                    name = "workout9",
                    description = "desc9",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 10,
                    name = "workout10",
                    description = "desc10",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 11,
                    name = "workout11",
                    description = "desc11",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 12,
                    name = "workout12",
                    description = "desc12",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 13,
                    name = "workout13",
                    description = "desc13",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = categories[0],
                    user = users[0]
                },
                new workout { 
                    id = 14,
                    name = "workout14",
                    description = "desc14",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = categories[0],
                    user = users[0]
                },
                new workout { 
                    id = 15,
                    name = "workout15",
                    description = "desc15",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = categories[0],
                    user = users[0]
                },
                new workout { 
                    id = 16,
                    name = "workout16",
                    description = "desc16",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = categories[1],
                    user = users[1]
                },
                new workout { 
                    id = 17,
                    name = "workout17",
                    description = "desc17",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = categories[1],
                    user = users[1]
                },
                new workout { 
                    id = 18,
                    name = "workout18",
                    description = "desc18",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = categories[1],
                    user = users[1]
                },
                new workout { 
                    id = 19,
                    name = "workout19",
                    description = "desc19",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 20,
                    name = "workout20",
                    description = "desc20",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 21,
                    name = "workout21",
                    description = "desc21",
                    created_at = Convert.ToDateTime("2015-06-15"),
                    category = categories[0],
                    user = users[1]
                },
                new workout { 
                    id = 22,
                    name = "workout22",
                    description = "desc22",
                    created_at = Convert.ToDateTime("2015-06-14"),
                    category = categories[1],
                    user = users[0]
                },
                new workout { 
                    id = 23,
                    name = "workout23",
                    description = "desc23",
                    created_at = Convert.ToDateTime("2015-06-13"),
                    category = categories[1],
                    user = users[0]
                },
                new workout { 
                    id = 24,
                    name = "workout24",
                    description = "desc24",
                    created_at = Convert.ToDateTime("2015-06-12"),
                    category = categories[1],
                    user = users[0]
                },
                new workout {
                    id = 25,
                    name = "Ab Workout",
                    description = "desc25",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[1],
                    user = users[3], 
                    workout_rating = ratings[4]
                },
                new workout {
                    id = 26,
                    name = "Endurance Endurance Endurance",
                    description = "desc26",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[0],
                    user = users[3], 
                    workout_rating = ratings[3]
                },
                new workout {
                    id = 27,
                    name = "Leg Workout 1",
                    description = "desc27",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[1],
                    user = users[3], 
                    workout_rating = ratings[1]
                },
                new workout {
                    id = 28,
                    name = "Running Core Workout",
                    description = "desc28",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[0],
                    user = users[3], 
                    workout_rating = ratings[2]
                },
                new workout {
                    id = 29,
                    name = "Upper Body Builder 1",
                    description = "desc29",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[1],
                    user = users[3], 
                    workout_rating = ratings[0]
                },
                new workout {
                    id = 30,
                    name = "Sprints",
                    description = "desc30",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[0],
                    user = users[3], 
                    workout_rating = ratings[5]
                },
                new workout {
                    id = 31,
                    name = "Running Upper Body Workout",
                    description = "desc31",
                    created_at = Convert.ToDateTime("2015-08-12"),
                    category = categories[0],
                    user = users[3], 
                    workout_rating = ratings[6]
                }
            };

            return workouts;
        }

        /// <summary>
        /// Gets fake workout ratings
        /// </summary>
        /// <returns>A list of workout ratings</returns>
        private List<workout_rating> getSeedWorkoutRatings()
        {
            var workoutRatings = new List<workout_rating>
            {
                new workout_rating
                { 
                    workout_id = 29,
                    average_rating = 8.0M,
                    times_rated = 1
                },
                new workout_rating
                { 
                    workout_id = 27,
                    average_rating = 7.0M,
                    times_rated = 10
                },
                new workout_rating
                { 
                    workout_id = 28,
                    average_rating = 6.0M,
                    times_rated = 3
                },
                new workout_rating
                { 
                    workout_id = 26,
                    average_rating = 8.0M,
                    times_rated = 3
                },
                new workout_rating
                { 
                    workout_id = 25,
                    average_rating = 4.0M,
                    times_rated = 9
                },
                new workout_rating
                { 
                    workout_id = 30,
                    average_rating = 9.0M,
                    times_rated = 16
                },
                new workout_rating
                { 
                    workout_id = 31,
                    average_rating = 10.0M,
                    times_rated = 2
                }
            };
            return workoutRatings;
        }

        /// <summary>
        /// Gets the fake categories
        /// </summary>
        /// <returns>A queryable list of fake categories</returns>
        private List<category> getSeedCategories()
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
            var categories = new List<category> { category1, category2, category3 };
            return categories;
        }

        /// <summary>
        /// Gets the fake users
        /// </summary>
        /// <returns>A queryable list of fake users</returns>
        private List<user> getSeedUsers()
        {
            user user1 = new user
            {
                id = 1,
                username = "admin",
                password = "3c1c88f0b0fec9b5f539c3d6b0577bd138bd157d604125a53e60e35cf940a5fe"
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
            user user4 = new user
            {
                id = 4,
                username = "RobMcElhenney"
            };
            var users = new List<user> { user1, user2, user3, user4 };
            return users;
        }

        /// <summary>
        /// Gets the fake workout_exercises
        /// </summary>
        /// <returns>A queryable list of fake workout_exercises</returns>
        private List<workout_exercise> getSeedWorkoutExercises() 
        {
            var workout_exercises = new List<workout_exercise> {
                new workout_exercise { id = 1, workout_id = 2/*, workout = testWorkout2, exercise = ex1*/},
                new workout_exercise { id = 2, workout_id = 2/*, workout = testWorkout2, exercise = ex2*/},
                new workout_exercise { id = 3, workout_id = 3/*, workout = testWorkout3, exercise = ex2*/}
            };
            return workout_exercises;
        }

        /// <summary>
        /// Gets the fake exercises
        /// </summary>
        /// <returns>A queryable list of fake exercises</returns>
        private List<exercise> getSeedExercises()
        {
            var users = getSeedUsers();
            exercise ex1 = new exercise
            {
                id = 1,
                name = "ex1",
                user = users[1],
                created_by_user_id = 2
            };
            exercise ex2 = new exercise
            {
                id = 2,
                name = "ex2",
                user = users[1],
                created_by_user_id = 2
            };
            exercise ex3 = new exercise
            {
                id = 3,
                name = "ex3",
                user = users[2],
                created_by_user_id = 3
            };
            var exercises = new List<exercise> { ex1, ex2, ex3 };
            return exercises;
        }

        /// <summary>
        /// Gets the fake db user_workouts collection with its associated entities declared inline
        /// </summary>
        /// <returns>A queryable list of user_workouts</returns>
        private List<user_workout> getSeedUserWorkouts()
        {
            List<category> categories = getSeedCategories();
            List<user> users = getSeedUsers();
            List<workout> workouts = getSeedWorkouts();

            var user_workouts = new List<user_workout>
            {
                new user_workout { 
                    user_id = 2,
                    workout_id = 1,
                    id = 1,
                    workout = workouts[0]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 2,
                    id = 2,
                    workout = workouts[1]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 3,
                    workout = workouts[2],
                    date_started = Convert.ToDateTime("2015-06-18")
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 4,
                    workout = workouts[2]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 2,
                    id = 5,
                    workout = workouts[1],
                    date_started = Convert.ToDateTime("2015-06-18")
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 1,
                    id = 6,
                    workout = workouts[0]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 1,
                    id = 7,
                    workout = workouts[0],
                    date_started = Convert.ToDateTime("2015-06-18"),
                    date_finished = Convert.ToDateTime("2015-06-18")
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 8,
                    workout = workouts[2]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 3,
                    id = 9,
                    workout = workouts[2]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 2,
                    id = 10,
                    workout = workouts[1]
                },
                new user_workout { 
                    user_id = 3,
                    workout_id = 1,
                    id = 11,
                    workout = workouts[0]
                },
                // User workouts for Recommend tests
                new user_workout { 
                    user_id = 4,
                    workout_id = 25,
                    id = 12,
                    workout = workouts[24],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 25,
                    id = 13,
                    workout = workouts[24],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 26,
                    id = 14,
                    workout = workouts[25],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 26,
                    id = 15,
                    workout = workouts[25],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 27,
                    id = 16,
                    workout = workouts[26],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 27,
                    id = 17,
                    workout = workouts[26],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 27,
                    id = 18,
                    workout = workouts[26],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 28,
                    id = 19,
                    workout = workouts[27],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 29,
                    id = 20,
                    workout = workouts[28],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 29,
                    id = 21,
                    workout = workouts[28],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 29,
                    id = 22,
                    workout = workouts[28],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                },
                new user_workout { 
                    user_id = 4,
                    workout_id = 29,
                    id = 23,
                    workout = workouts[28],
                    date_started = DateTime.Now,
                    date_finished = DateTime.Now
                }
            };

            return user_workouts;
        }

        /// <summary>
        /// Gets the fake comments
        /// </summary>
        /// <returns>A list of fake comments</returns>
        private List<comment> getSeedComments()
        {
            List<workout> workouts = getSeedWorkouts();
            List<user> users = getSeedUsers();

            comment comment1 = new comment
            {
                id = 1,
                message = "Comment1",
                date_created = Convert.ToDateTime("2015-01-01"),
                user = users[1],
                workout = workouts[1]
            };
            comment comment2 = new comment
            {
                id = 2,
                message = "Comment2",
                date_created = Convert.ToDateTime("2015-02-02"),
                user = users[1],
                workout = workouts[2]
            };
            comment comment3 = new comment
            {
                id = 3,
                message = "Comment3",
                date_created = Convert.ToDateTime("2015-03-03"),
                user = users[2],
                workout = workouts[1],
            };
            var comment = new List<comment> { comment1, comment2, comment3 };
            return comment;
        }

        private List<user_favorite_workout> getSeedUserFavWorkouts()
        {
            var users = getSeedUsers();
            var workouts = getSeedWorkouts();
            user_favorite_workout fav1 = new user_favorite_workout
            {
                id = 1,
                user_id = 3,
                user = users[1],
                workout = workouts[0],
                workout_id = 1,
                timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0}
            };
            user_favorite_workout fav2 = new user_favorite_workout
            {
                id = 2,
                user_id = 3,
                user = users[1],
                workout = workouts[1],
                workout_id = 2,
                timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            user_favorite_workout fav3 = new user_favorite_workout
            {
                id = 3,
                user_id = 3,
                user = users[1],
                workout = workouts[10],
                workout_id = 11,
                timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            user_favorite_workout fav4 = new user_favorite_workout
            {
                id = 4,
                user_id = 3,
                user = users[1],
                workout = workouts[11],
                workout_id = 12,
                timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            var userFavs = new List<user_favorite_workout> { fav1, fav2, fav3, fav4 };
            return userFavs;
        }

        private List<workout_rating> getSeedWorkoutRating()
        {
            var workouts = getSeedWorkouts();
            workout_rating rating1 = new workout_rating
            {
                workout_id = 1,
                workout = workouts[0],
                average_rating = 10.0M,
                times_rated = 1
            };
            workout_rating rating2 = new workout_rating
            {
                workout_id = 2,
                workout = workouts[1],
                average_rating = 9.0M,
                times_rated = 3
            };
            workout_rating rating3 = new workout_rating
            {
                workout_id = 3,
                workout = workouts[2],
                average_rating = 10.0M,
                times_rated = 2
            };
            var workoutRating = new List<workout_rating> { rating1, rating2, rating3 };
            return workoutRating;
        }
    }
}
