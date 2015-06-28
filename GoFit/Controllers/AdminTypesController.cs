using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoFit.Models;
using PagedList;
using GoFit.Controllers.ControllerHelpers;
using System.Data.Entity.Infrastructure;

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminTypesController : GoFitBaseController
    {
        private masterEntities db;
        private const int PAGE_SIZE = 10;

        /// <summary>
        /// Getter/setter for the pageSize instance variable
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Constructor to create the default db context
        /// </summary>
        public AdminTypesController()
        {
            db = new masterEntities();
            pageSize = PAGE_SIZE;
        }

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

        // GET: AdminTypes
        public ActionResult Index(string filterString, string sortBy, int? page, TypeSearch typeSearch)
        {
            //return View(db.types.ToList());
            var types = from t in db.types select t;
            types = this.doSearch(types, typeSearch, filterString, sortBy, page);
            types = this.doSort(types, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", types.ToPagedList(pageNumber, pageSize));
            return view;
        }

        // GET: AdminTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type type = db.types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: AdminTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,measure,timestamp")] type type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.types.Add(type);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(type);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminTypes", "Create");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the type."));
            }

        }

        // GET: AdminTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type type = db.types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: AdminTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,measure,timestamp")] type type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var t = db.types.Find(type.id);
                    var entry = db.Entry(t);
                    var state = entry.State;
                    if (state == EntityState.Detached)
                    {
                        db.Entry(type).State = EntityState.Modified;
                    }
                    else
                    {
                        entry.OriginalValues["timestamp"] = type.timestamp;
                        entry.CurrentValues.SetValues(type);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(type);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user as another user/admin may have already update this user"));
            }
            catch (DbUpdateException ex)
            {
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit user."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminTypes", "Edit");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the type."));
            }

        }

        // GET: AdminTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type type = db.types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: AdminTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                type type = db.types.Find(id);
                db.types.Remove(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminTypes", "DeleteConfirmed");
                return View("_AdminDetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the type as it may be referenced in the database."));
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Private helper method to perform a new search or maintain a previous search through 
        /// pagination and filter changes
        /// </summary>
        /// <param name="types">The base type query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched types</returns>
        private IQueryable<type> doSearch(IQueryable<type> types, TypeSearch search, String filterString, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy) || !String.IsNullOrEmpty(filterString))
            {
                search = setSearchFromSession(search);
            }
            else setSessionFromSearch(search);

            if (!String.IsNullOrEmpty(search.name)) types = types.Where(t => t.name.Contains(search.name));
            if (!String.IsNullOrEmpty(search.measure)) types = types.Where(t => t.measure.Contains(search.measure));

            return types;
        }

        /// <summary>
        /// Private helper method set and return the sorted types
        /// </summary>
        /// <param name="types">The base type query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted types</returns>
        private IQueryable<type> doSort(IQueryable<type> types, string sortBy)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                SessionVariableManager.setSessionFromSort(Session, sortBy);
            }
            else
            {
                sortBy = SessionVariableManager.setSortFromSession(Session, sortBy);
            }

            ViewBag.NameSortParam = (sortBy == "name") ? "name_desc" : "name";
            ViewBag.MeasureSortParam = (sortBy == "measure") ? "measure_desc" : "measure";

            switch (sortBy)
            {
                case "name_desc":
                    types = types.OrderByDescending(t => t.name);
                    break;
                case "measure":
                    types = types.OrderBy(t => t.measure);
                    break;
                case "measure_desc":
                    types = types.OrderByDescending(t => t.measure);
                    break;
                default:
                    types = types.OrderBy(t => t.name);
                    break;
            }

            return types;
        }

        /// <summary>
        /// Sets the TypeSearch object with the stored session search variables if they exist
        /// </summary>
        /// <param name="search">The TypeSearch object to set</param>
        /// <returns>The TypeSearch object set with the session search variables if the session exists, else the passed in TypeSearch object</returns>
        private TypeSearch setSearchFromSession(TypeSearch search)
        {
            if (Session != null)
            {
                search.name = Session["NameSearchParam"] as String;
                search.measure = Session["MeasureSearchParam"] as String;
            }
            return search;
        }

        /// <summary>
        /// Sets the session search parameters based on the current search values
        /// </summary>
        /// <param name="search">The WorkoutSearch object containing the values to set in the session</param>
        private void setSessionFromSearch(TypeSearch search)
        {
            if (Session != null)
            {
                if (!String.IsNullOrEmpty(search.name)) Session["NameSearchParam"] = search.name;
                else Session["NameSearchParam"] = "";

                if (!String.IsNullOrEmpty(search.measure)) Session["MeasureSearchParam"] = search.measure;
                else Session["MeasureSearchParam"] = "";


            }
        }
    }
}
