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
    public class AdminWorkoutsController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: AdminWorkouts
        public ActionResult Index()
        {
            var workouts = db.workouts.Include(w => w.category).Include(w => w.user);
            return View(workouts.ToList());
        }

        // GET: AdminWorkouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: AdminWorkouts/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "id", "name");
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: AdminWorkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,category_id,created_by_user_id,created_at,timestamp")] workout workout)
        {
            if (ModelState.IsValid)
            {
                db.workouts.Add(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
            return View(workout);
        }

        // GET: AdminWorkouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
            return View(workout);
        }

        // POST: AdminWorkouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,category_id,created_by_user_id,created_at,timestamp")] workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.categories, "id", "name", workout.category_id);
            ViewBag.created_by_user_id = new SelectList(db.users, "id", "username", workout.created_by_user_id);
            return View(workout);
        }

        // GET: AdminWorkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            workout workout = db.workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: AdminWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            workout workout = db.workouts.Find(id);
            db.workouts.Remove(workout);
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
