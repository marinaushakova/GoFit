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
    public class AdminCommentsController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: AdminComments
        public ActionResult Index()
        {
            var comments = db.comments.Include(c => c.user).Include(c => c.workout);
            return View(comments.ToList());
        }

        // GET: AdminComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: AdminComments/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.users, "id", "username");
            ViewBag.Workout_id = new SelectList(db.workouts, "id", "name");
            return View();
        }

        // POST: AdminComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,message,timestamp,User_id,Workout_id,date_cteated")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_id = new SelectList(db.users, "id", "username", comment.User_id);
            ViewBag.Workout_id = new SelectList(db.workouts, "id", "name", comment.Workout_id);
            return View(comment);
        }

        // GET: AdminComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.users, "id", "username", comment.User_id);
            ViewBag.Workout_id = new SelectList(db.workouts, "id", "name", comment.Workout_id);
            return View(comment);
        }

        // POST: AdminComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,message,timestamp,User_id,Workout_id,date_cteated")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.users, "id", "username", comment.User_id);
            ViewBag.Workout_id = new SelectList(db.workouts, "id", "name", comment.Workout_id);
            return View(comment);
        }

        // GET: AdminComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
