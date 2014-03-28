using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace IvtLibrary.Controllers
{ 
    public class ThemeController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Theme/

        public ViewResult Index()
        {
            return View(db.Theme.ToList());
        }

        //
        // GET: /Theme/Details/5

        public ViewResult Details(int id)
        {
            Theme theme = db.Theme.Single(t => t.id == id);
            return View(theme);
        }

        //
        // GET: /Theme/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Theme/Create

        [HttpPost]
        public ActionResult Create(Theme theme)
        {
            if (ModelState.IsValid)
            {
                db.Theme.AddObject(theme);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(theme);
        }
        
        //
        // GET: /Theme/Edit/5
 
        public ActionResult Edit(int id)
        {
            Theme theme = db.Theme.Single(t => t.id == id);
            return View(theme);
        }

        //
        // POST: /Theme/Edit/5

        [HttpPost]
        public ActionResult Edit(Theme theme)
        {
            if (ModelState.IsValid)
            {
                db.Theme.Attach(theme);
                db.ObjectStateManager.ChangeObjectState(theme, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theme);
        }

        //
        // GET: /Theme/Delete/5
 
        public ActionResult Delete(int id)
        {
            Theme theme = db.Theme.Single(t => t.id == id);
            return View(theme);
        }

        //
        // POST: /Theme/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Theme theme = db.Theme.Single(t => t.id == id);
            db.Theme.DeleteObject(theme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}