using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Models
{
    public class UserWorkoutViewModel
    {
        public GoFit.Models.user User { get; set; }
        public List<GoFit.Models.user_workout> UserWorkoutList { get; set; }
    }
}