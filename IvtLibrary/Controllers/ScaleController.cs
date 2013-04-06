using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace IvtLibrary.Controllers
{ 
    public class ScaleController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Scale/

        public ViewResult Index()
        {
            return View(db.Scale.ToList());
        }

        //
        // GET: /Scale/Details/5

        public ViewResult Details(int id)
        {
            Scale scale = db.Scale.Single(s => s.id == id);
            return View(scale);
        }

        //
        // GET: /Scale/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Scale/Create

        [HttpPost]
        public ActionResult Create(Scale scale)
        {
            if (ModelState.IsValid)
            {
                db.Scale.AddObject(scale);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(scale);
        }
        
        //
        // GET: /Scale/Edit/5
 
        public ActionResult Edit(int id)
        {
            Scale scale = db.Scale.Single(s => s.id == id);
            return View(scale);
        }

        //
        // POST: /Scale/Edit/5

        [HttpPost]
        public ActionResult Edit(Scale scale)
        {
            if (ModelState.IsValid)
            {
                db.Scale.Attach(scale);
                db.ObjectStateManager.ChangeObjectState(scale, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scale);
        }

        //
        // GET: /Scale/Delete/5
 
        public ActionResult Delete(int id)
        {
            Scale scale = db.Scale.Single(s => s.id == id);
            return View(scale);
        }

        //
        // POST: /Scale/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Scale scale = db.Scale.Single(s => s.id == id);
            db.Scale.DeleteObject(scale);
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