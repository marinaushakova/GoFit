using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminHomeController : GoFitBaseController
    {
        private masterEntities db;

        public AdminHomeController() : base() 
        {
            db = this.getDB();
        }

        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}