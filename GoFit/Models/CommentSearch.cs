using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Models
{
    public class CommentSearch
    {
        public CommentSearch()
        {

        }

        public string message { get; set; }
        public int User_id { get; set; }
    }
}