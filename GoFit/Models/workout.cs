//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoFit.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class workout
    {
        public workout()
        {
            this.comments = new HashSet<comment>();
            this.user_favorite_workout = new HashSet<user_favorite_workout>();
            this.user_workout = new HashSet<user_workout>();
            this.workout_exercise = new HashSet<workout_exercise>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = "Name reqired", AllowEmptyStrings = false)]
        public string name { get; set; }
        [Required(ErrorMessage = "Description reqired", AllowEmptyStrings = false)]
        public string description { get; set; }
        [Required(ErrorMessage = "Category reqired", AllowEmptyStrings = false)]

        public int category_id { get; set; }
        [Required(ErrorMessage = "User id reqired", AllowEmptyStrings = false)]
        public int created_by_user_id { get; set; }
        public System.DateTime created_at { get; set; }
        [ConcurrencyCheck]
        public byte[] timestamp { get; set; }
    
        public virtual category category { get; set; }
        public virtual ICollection<comment> comments { get; set; }
        public virtual user user { get; set; }
        public virtual ICollection<user_favorite_workout> user_favorite_workout { get; set; }
        public virtual ICollection<user_workout> user_workout { get; set; }
        public virtual ICollection<workout_exercise> workout_exercise { get; set; }
        public virtual workout_rating workout_rating { get; set; }


        internal void CreateWorkoutExercise(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                workout_exercise.Add(new workout_exercise());
            }
        }
    }
}
