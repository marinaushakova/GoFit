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

namespace GoFit.Controllers
{
    [Authorize]
    public class AdminCategoriesController : Controller
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
        public AdminCategoriesController()
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

        // GET: AdminCategories
        public ActionResult Index(string filterString, string sortBy, int? page, CategorySearch categorySearch)
        {
            //return View(db.categories.ToList());

            var categories = from c in db.categories select c;
            categories = this.doSearch(categories, categorySearch, filterString, sortBy, page);
            categories = this.doSort(categories, sortBy);

            int pageNumber = (page ?? 1);
            var view = View("Index", categories.ToPagedList(pageNumber, pageSize));
            return view;
        }

        // GET: AdminCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: AdminCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,timestamp")] category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //category.timestamp = DateTime.Now;
                    db.categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(category);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminCategories", "Create");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to create the category."));
            }

        }

        // GET: AdminCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,timestamp")] category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                var err = new HandleErrorInfo(ex, "AdminCategories", "Edit");
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to edit the category."));
            }

        }

        // GET: AdminCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                category category = db.categories.Find(id);
                db.categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {   
                var err = new HandleErrorInfo(ex, "AdminCategories", "DeleteConfirmed"); 
                return View("DetailedError", new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the category as it may be referenced in the database."));
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
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">The passed sort string if it exists, else null</param>
        /// <param name="page">The passed page param if it exists, else null</param>
        /// <returns>The searched workouts</returns>
        private IQueryable<category> doSearch(IQueryable<category> categories, CategorySearch search, String filterString, string sortBy, int? page)
        {
            if (page != null || !String.IsNullOrEmpty(sortBy) || !String.IsNullOrEmpty(filterString))
            {
                search = setSearchFromSession(search);
            }
            else setSessionFromSearch(search);

            if (!String.IsNullOrEmpty(search.name)) categories = categories.Where(c => c.name.Contains(search.name));
            if (!String.IsNullOrEmpty(search.description)) categories = categories.Where(c => c.description.Contains(search.description));
            
            return categories;
        }

        /// <summary>
        /// Private helper method set and return the sorted workouts
        /// </summary>
        /// <param name="workouts">The base workout query result</param>
        /// <param name="sortBy">Indicates the sort order</param>
        /// <returns>The sorted workouts</returns>
        private IQueryable<category> doSort(IQueryable<category> categories, string sortBy)
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
            ViewBag.DescriptionSortParam = (sortBy == "description") ? "description_desc" : "description";
            ViewBag.TimestampSortParam = (sortBy == "time") ? "time_desc" : "time";
          
            switch (sortBy)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(c => c.name);
                    break;
                case "description":
                    categories = categories.OrderBy(c => c.description);
                    break;
                case "description_desc":
                    categories = categories.OrderByDescending(c => c.description);
                    break;
                case "time":
                    categories = categories.OrderBy(c => c.timestamp);
                    break;
                case "time_desc":
                    categories = categories.OrderByDescending(c => c.timestamp);
                    break;
                default:
                    categories = categories.OrderBy(c => c.name);
                    break;
            }

            return categories;
        }

        /// <summary>
        /// Sets the WorkoutSearch object with the stored session search variables if they exist
        /// </summary>
        /// <param name="search">The WorkoutSearch object to set</param>
        /// <returns>The WorkoutSearch object set with the session search variables if the session exists, else the passed in WorkoutSearch object</returns>
        private CategorySearch setSearchFromSession(CategorySearch search)
        {
            if (Session != null)
            {
                search.name = Session["NameSearchParam"] as String;
                search.description = Session["DescriptionSearchParam"] as String;
            }
            return search;
        }

        /// <summary>
        /// Sets the session search parameters based on the current search values
        /// </summary>
        /// <param name="search">The WorkoutSearch object containing the values to set in the session</param>
        private void setSessionFromSearch(CategorySearch search)
        {
            if (Session != null)
            {
                if (!String.IsNullOrEmpty(search.name)) Session["NameSearchParam"] = search.name;
                else Session["NameSearchParam"] = "";

                if (!String.IsNullOrEmpty(search.description)) Session["DescriptionSearchParam"] = search.description;
                else Session["DescriptionSearchParam"] = "";


            }
        }
    }
}
