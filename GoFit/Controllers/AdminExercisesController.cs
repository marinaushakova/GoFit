using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminExercisesController : Controller
    {
        private masterEntities db = new masterEntities();

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAdmin = 0;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().is_admin;
            }

            // Redirect non-administrative users to the home page upon authorization
            if (isAdmin != 1)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }

        // GET: AdminExercises
        public ActionResult Index()
        {
            var exercises = db.exercises.Include(e => e.type).Include(e => e.user);
            return View(exercises.ToList());
        }

        // GET: AdminExercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercise exercise = db.exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: AdminExercises/Create
        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.types, "id", "name");
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: AdminExercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,type_id,created_by_user_id,created_at,link,description,timestamp,name")] exercise exercise)
        {
            if (ModelState.IsValid)
            {
                exercise.timestamp = DateTime.Now;
                exercise.created_at = DateTime.Now;
                exercise.created_by_user_id = db.users.Where(a => a.username.Equals(User.Identity.Name)).FirstOrDefault().id;
                db.exercises.Add(exercise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.type_id = new SelectList(db.types, "id", "name", exercise.type_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", exercise.created_by_user_id);
            return View(exercise);
        }

        // GET: AdminExercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercise exercise = db.exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            ViewBag.type_id = new SelectList(db.types, "id", "name", exercise.type_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", exercise.created_by_user_id);
            return View(exercise);
        }

        // POST: AdminExercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,type_id,created_by_user_id,created_at,link,description,timestamp,name")] exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_id = new SelectList(db.types, "id", "name", exercise.type_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", exercise.created_by_user_id);
            return View(exercise);
        }

        // GET: AdminExercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercise exercise = db.exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: AdminExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            exercise exercise = db.exercises.Find(id);
            db.exercises.Remove(exercise);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
