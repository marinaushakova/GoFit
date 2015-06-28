using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GoFit.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFoundError()
        {
            return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The requested resource could not be found")); 
        }
	}
}