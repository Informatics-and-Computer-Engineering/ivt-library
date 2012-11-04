using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace IvtLibrary.Controllers
{ 
    public class DraftController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Draft/

        public ViewResult Index()
        {
            return View(db.Draft.ToList());
        }

        //
        // GET: /Draft/Details/5

        public ViewResult Details(long id)
        {
            Draft draft = db.Draft.Single(d => d.id == id);
            return View(draft);
        }

        //
        // GET: /Draft/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Draft/Create

        [HttpPost]
        public ActionResult Create(Draft draft)
        {
            if (ModelState.IsValid)
            {
                db.Draft.AddObject(draft);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(draft);
        }
        
        //
        // GET: /Draft/Edit/5
 
        public ActionResult Edit(long id)
        {
            Draft draft = db.Draft.Single(d => d.id == id);
            return View(draft);
        }

        //
        // POST: /Draft/Edit/5

        [HttpPost]
        public ActionResult Edit(Draft draft)
        {
            if (ModelState.IsValid)
            {
                db.Draft.Attach(draft);
                db.ObjectStateManager.ChangeObjectState(draft, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(draft);
        }

        //
        // GET: /Draft/Delete/5
 
        public ActionResult Delete(long id)
        {
            Draft draft = db.Draft.Single(d => d.id == id);
            return View(draft);
        }

        //
        // POST: /Draft/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Draft draft = db.Draft.Single(d => d.id == id);
            db.Draft.DeleteObject(draft);
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