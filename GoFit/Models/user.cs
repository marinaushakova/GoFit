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
    
    public partial class user
    {
        public user()
        {
            this.exercises = new HashSet<exercise>();
            this.user_favorite_exercise = new HashSet<user_favorite_exercise>();
            this.user_favorite_workout = new HashSet<user_favorite_workout>();
            this.user_workout = new HashSet<user_workout>();
            this.workouts = new HashSet<workout>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public Nullable<short> is_male { get; set; }
        public short is_admin { get; set; }
        public Nullable<int> weight { get; set; }
        public Nullable<decimal> height { get; set; }
        public System.DateTime timestamp { get; set; }
    
        public virtual ICollection<exercise> exercises { get; set; }
        public virtual ICollection<user_favorite_exercise> user_favorite_exercise { get; set; }
        public virtual ICollection<user_favorite_workout> user_favorite_workout { get; set; }
        public virtual ICollection<user_workout> user_workout { get; set; }
        public virtual ICollection<workout> workouts { get; set; }
    }
}