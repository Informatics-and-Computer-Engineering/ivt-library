using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace IvtLibrary.Controllers
{ 
    public class HypothesisController : Controller
    {
        private readonly IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Hypothesis/

        public ViewResult Index()
        {
            return View(db.Hypothesis.ToList());
        }

        //
        // GET: /Hypothesis/Details/5

        public ViewResult Details(long id)
        {
            Hypothesis hypothesis = db.Hypothesis.Single(h => h.id == id);
            return View(hypothesis);
        }

        //
        // GET: /Hypothesis/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Hypothesis/Create

        [HttpPost]
        public ActionResult Create(Hypothesis hypothesis)
        {
            if (ModelState.IsValid)
            {
                db.Hypothesis.AddObject(hypothesis);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(hypothesis);
        }
        
        //
        // GET: /Hypothesis/Edit/5
 
        public ActionResult Edit(long id)
        {
            Hypothesis hypothesis = db.Hypothesis.Single(h => h.id == id);
            return View(hypothesis);
        }

        //
        // POST: /Hypothesis/Edit/5

        [HttpPost]
        public ActionResult Edit(Hypothesis hypothesis)
        {
            if (ModelState.IsValid)
            {
                db.Hypothesis.Attach(hypothesis);
                db.ObjectStateManager.ChangeObjectState(hypothesis, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hypothesis);
        }

        //
        // GET: /Hypothesis/Delete/5
 
        public ActionResult Delete(long id)
        {
            Hypothesis hypothesis = db.Hypothesis.Single(h => h.id == id);
            return View(hypothesis);
        }

        //
        // POST: /Hypothesis/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Hypothesis hypothesis = db.Hypothesis.Single(h => h.id == id);
            db.Hypothesis.DeleteObject(hypothesis);
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