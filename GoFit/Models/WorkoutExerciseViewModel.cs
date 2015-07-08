using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Models
{
    public class WorkoutExerciseViewModel
    {
        public GoFit.Models.workout Workout { get; set; }
        public GoFit.Models.workout_exercise WorkoutExercise { get; set; }
    }
}