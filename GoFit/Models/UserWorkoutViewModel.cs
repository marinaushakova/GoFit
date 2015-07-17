using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Models
{
    public class UserWorkoutViewModel
    {
        public user User { get; set; }
        public List<user_workout> UserWorkoutList { get; set; }
    }
}