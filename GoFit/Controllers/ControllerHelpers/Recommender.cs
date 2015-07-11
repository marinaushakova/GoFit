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
            List<category> categoryList = db.Set<category>().ToList();
            if (categoryList.Count < 1) return null;

            // get the users completed workouts
            var completed = db.user_workout.Where(
                w => w.user_id == userID && 
                w.date_finished != null
            );
            if (completed.Count() < 1) return null;
            List<user_workout> completedList = completed.ToList();
            List<int> completedIdList = completed.Select(c => c.workout_id).ToList();

            // Set up a disctionary with a key for each category to track the number of completed exercises in each category
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (category current in categoryList)
            {
                categoryCount.Add(current.name, 0);
            }

            // Tally the completed workouts for each category
            foreach (user_workout current in completedList)
            {
                string currentCategory = current.workout.category.name;
                categoryCount[currentCategory] += 1;
            }

            // Generate a random number to determin the algorithm to use
            Random random = new Random();
            int algKey = random.Next(1, 3);
            algKey = 1;

            workout recommendation = null;
            switch (algKey)
            {
                // Return a workout that has the same category as the users most popular category and highest rating of the workouts the user hasn't done
                case 1:
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
                    recommendation = notDoneWorkoutsInCategory.Where(w => w.workout_rating.average_rating == highestRating).FirstOrDefault();
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            return recommendation;
        }
    }
}