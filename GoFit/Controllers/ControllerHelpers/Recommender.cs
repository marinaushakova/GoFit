using GoFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Controllers.ControllerHelpers
{
    /// <summary>
    /// Calculates recommended workouts based on a given user's completed workout 
    /// history
    /// </summary>
    public class Recommender
    {
        private masterEntities db;

        /// <summary>
        /// Constructs a recommender with the given db context
        /// </summary>
        /// <param name="context"></param>
        public Recommender(masterEntities context)
        {
            db = context;
        }

        /// <summary>
        /// Provides a recommendation base on one of a few randomly selected
        /// algorithms
        /// </summary>
        /// <param name="userID">The user to get a recommendation for</param>
        /// <returns>The recommended workout</returns>
        public workout Recommend(int userID)
        {
            // Get a list of all categories
            List<category> categoryList = getFullCategoryList();
            if (categoryList.Count < 1) return null;

            // get the users completed workouts
            var completed = getCompletedWorkoutsForUser(userID);
            if (completed.Count() < 1) return null;
            List<user_workout> completedList = completed.ToList();
            List<int> completedIdList = completed.Select(c => c.workout_id).ToList();

            // Set up a disctionary with a key for each category to track the number of completed exercises in each category
            Dictionary<string, int> categoryCount = getCategoryTally(categoryList, completedList);

            // Generate a random number to determin the algorithm to use
            int algKey = getRandomNum(1, 2);
            algKey = 1;

            workout recommendation = null;
            switch (algKey)
            {
                case 1:
                    recommendation = getSimilarWorkout(categoryCount, completedIdList);
                    break;
                default:
                    recommendation = getDissimilarWorkout(categoryCount, completedIdList);
                    break;
            }
            return recommendation;
        }

        /// <summary>
        /// Gets a recommended workout in a category that the user uses the least, with the highest rating. 
        /// Gets a workout the user has not completed, if possible.
        /// </summary>
        /// <param name="categoryCount">A dictionary of the category names with the corresponding count of workouts 
        /// the user has completed in that category</param>
        /// <param name="completedIdList">A list of the workout ids of the workouts the user has completed</param>
        /// <returns>The recommended workout</returns>
        private workout getDissimilarWorkout(Dictionary<string, int> categoryCount, List<int> completedIdList)
        {
            // Calculate the least common category for this user
            string category = "";
            int least = categoryCount.Values.Min();
            var dictEntry = categoryCount.Where(cc => cc.Value == least).FirstOrDefault();
            category = dictEntry.Key;

            // Get a list of workouts in the fav cateogry that the user has not completed
            List<workout> notDoneWorkoutsInCategory = db.workouts.Where(
                w => w.category.name == category &&
                    w.workout_rating != null &&
                !completedIdList.Contains(w.id)
            ).ToList();
            if (notDoneWorkoutsInCategory.Count < 1)
            {
                notDoneWorkoutsInCategory = db.workouts.Where(
                    w => w.category.name == category &&
                    w.workout_rating != null
                ).ToList();
            }
            decimal highestRating = notDoneWorkoutsInCategory.Max(w => w.workout_rating.average_rating);
            workout recommendation = notDoneWorkoutsInCategory.Where(w => w.workout_rating.average_rating == highestRating).FirstOrDefault();
            return recommendation;
        }

        /// <summary>
        /// Gets a recommended workout in the category that the given user completes the most, with the highest rating. 
        /// Gets a workout the user has not completed, if possible. 
        /// </summary>
        /// <param name="categoryCount">A dictionary of the category names with the corresponding count of workouts 
        /// the user has completed in that category</param>
        /// <param name="completedIdList">A list of the workout ids of the workouts the user has completed</param>
        /// <returns>The recommended workout</returns>
        private workout getSimilarWorkout(Dictionary<string, int> categoryCount, List<int> completedIdList)
        {
            // Calculate the most popular category for this user
            string favCategory = "";
            int greatest = categoryCount.Values.Max();
            var dictEntry = categoryCount.Where(cc => cc.Value == greatest).FirstOrDefault();
            favCategory = dictEntry.Key;

            // Get a list of workouts in the fav cateogry that the user has not completed
            List<workout> notDoneWorkoutsInCategory = db.workouts.Where(
                w => w.category.name == favCategory &&
                    w.workout_rating != null &&
                !completedIdList.Contains(w.id)
            ).ToList();
            if (notDoneWorkoutsInCategory.Count < 1)
            {
                notDoneWorkoutsInCategory = db.workouts.Where(
                    w => w.category.name == favCategory &&
                    w.workout_rating != null
                ).ToList();
            }
            decimal highestRating = notDoneWorkoutsInCategory.Max(w => w.workout_rating.average_rating);
            workout recommendation = notDoneWorkoutsInCategory.Where(w => w.workout_rating.average_rating == highestRating).FirstOrDefault();
            return recommendation;
        }

        /// <summary>
        /// Gets a random number between the given bounds inclusive on both ends
        /// </summary>
        /// <param name="min">The min boundary (inclusive)</param>
        /// <param name="max">The max boundary (inclusive)</param>
        /// <returns>The random int withing the inclusive boundaries</returns>
        private int getRandomNum(int min, int max)
        {
            max += 1;
            Random random = new Random();
            int num = random.Next(min, max);
            return num;
        }

        /// <summary>
        /// Gets a list of all the categories as a list
        /// </summary>
        /// <returns>The list of all the categories</returns>
        private List<category> getFullCategoryList()
        {
            List<category> categoryList = db.Set<category>().ToList();
            return categoryList;
        }

        /// <summary>
        /// Gets an IQueryable list of the workouts completed by the given user id
        /// </summary>
        /// <param name="userID">The user ID to get completed workouts for</param>
        /// <returns>The queryable list of completed workouts for the user</returns>
        private IQueryable<user_workout> getCompletedWorkoutsForUser(int userID)
        {
            var completed = db.user_workout.Where(
                w => w.user_id == userID &&
                w.date_finished != null
            );
            return completed;
        }

        /// <summary>
        /// Sets up a dictionary with keys as category names and values as the number of 
        /// workouts a given user has completed in that category
        /// </summary>
        /// <param name="categoryList">List of all categories</param>
        /// <param name="completedWorkoutList">List of all completed workouts for a given user</param>
        /// <returns>The Dictionary showing the total number of workouts completed in each category by the given user</returns>
        private Dictionary<string, int> getCategoryTally(List<category> categoryList, List<user_workout> completedWorkoutList)
        {
            // Set up a disctionary with a key for each category to track the number of completed exercises in each category
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (category current in categoryList)
            {
                categoryCount.Add(current.name, 0);
            }

            // Tally the completed workouts for each category
            foreach (user_workout current in completedWorkoutList)
            {
                string currentCategory = current.workout.category.name;
                categoryCount[currentCategory] += 1;
            }
            return categoryCount;
        }
    }
}