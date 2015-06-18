using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Models
{
    public class WorkoutSearch
    {
        public WorkoutSearch()
        {

        }

        public string name { get; set; }
        public string category { get; set; }
        public string dateAdded { get; set; }
        public string username { get; set; }
    }
}