using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoFit.Models
{
    public class UserWorkoutViewModel
    {
        public user TheUser { get; set; }
        public List<user_workout> BragFeedWorkoutList { get; set; }
    }
}