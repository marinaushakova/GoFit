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
        public AdminTypesController() : base()
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
        }

        /// <summary>
        /// Constructor that takes the db as a parmeter and calls the base contructor
        /// with it. 
        /// </summary>
        /// <param name="context">the db to use</param>
        public AdminTypesController(masterEntities context)
            : base(context)
        {
            db = this.getDB();
            pageSize = PAGE_SIZE;
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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No type to view was specified"));
            }
            type type = db.types.Find(id);
            if (type == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The type could not be found or does not exist"));
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
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the type. Please try again."));
            }

        }

        // GET: AdminTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No type to edit was specified"));
            }
            type type = db.types.Find(id);
            if (type == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The type could not be found or does not exist"));
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
                    var oldType = db.types.Find(type.id);
                    if (oldType == null)
                    {
                        return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The type does not exist or has already been deleted"));
                    }
                    var entry = db.Entry(oldType);
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
            catch (DbUpdateConcurrencyException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit type as another admin may have already updated this type"));
            }
            catch (DbUpdateException)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit type."));
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminTypes", "Edit");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the type."));
            }

        }
        /*
        // GET: AdminTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No type to delete was specified"));
            }
            type type = db.types.Find(id);
            if (type == null)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.NotFound, "The type could not be found or does not exist"));
            }
            return View(type);
        }

        // POST: AdminTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "id,timestamp")] type type)
        {
            try
            {
                type oldType = db.types.Find(type.id);
                if (oldType == null)
                {
                    return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The type does not exist or has already been deleted"));
                }
                var entry = db.Entry(oldType);
                var state = entry.State;
                if (state == EntityState.Detached)
                {
                    db.types.Remove(type);
                }
                else
                {
                    entry.OriginalValues["timestamp"] = type.timestamp;
                    db.types.Remove(oldType);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the type as another admin may have modified this type"));
            }
            catch (DbUpdateException ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the type as it may be referenced by another item."));
            }
            catch (Exception ex)
            {
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the type."));
            }
            
        }*/

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
