using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace IvtLibrary.Controllers
{ 
    public class AuthorController : Controller
    {
        private IvtLibraryEntities db = new IvtLibraryEntities();

        //
        // GET: /Author/

        public ViewResult Index()
        {
            return View(db.Author.ToList());
        }

        //
        // GET: /Author/Details/5

        public ViewResult Details(int id)
        {
            Author author = db.Author.Single(a => a.id == id);
            return View(author);
        }

        //
        // GET: /Author/Create

        public ActionResult Create()
        {
            FillThemesCheckBoxList(null);
            return View();
        } 

        //
        // POST: /Author/Create

        [HttpPost]
        public ActionResult Create(Author author, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Author.AddObject(author);
                SetAuthorThemes(author, themeIds);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(author);
        }
        
        //
        // GET: /Author/Edit/5
 
        public ActionResult Edit(int id)
        {
            Author author = db.Author.Single(a => a.id == id);
            FillThemesCheckBoxList(author);
            return View(author);
        }

        //
        // POST: /Author/Edit/5

        [HttpPost]
        public ActionResult Edit(Author author, int[] themeIds)
        {
            if (ModelState.IsValid)
            {
                db.Author.Attach(author);
                SetAuthorThemes(author, themeIds);
                db.ObjectStateManager.ChangeObjectState(author, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        private void SetAuthorThemes(Author author, int[] themeIds)
        {
            // получаем коллекцию тем, выбранных пользователем на форме
            var selectedThemes = GetThemesByIds(themeIds);
            // Вытаскиваем темы которые уже есть у данного автора
            var authorThemes = author.Theme.ToList();
            // удаляем темы которые исчезли из отмеченных
            foreach (var theme in authorThemes)
            {
                if (!selectedThemes.Contains(theme))
                {
                    author.Theme.Remove(theme);
                }
            }
            // добавляем темы которые добавил пользователь
            foreach (var theme in selectedThemes)
            {
                if (!authorThemes.Contains(theme))
                {
                    author.Theme.Add(theme);
                }
            }
                
        }

        private IList<Theme> GetThemesByIds(int[] themeIds)
        {
            List<Theme> selectedThemes = new List<Theme>();
            //выбираем все темы с заданными id
            foreach (int themeId in themeIds)
            {
                Theme theme = db.Theme.Single(t => t.id == themeId);
                selectedThemes.Add(theme);
            }
            return selectedThemes;
        }

        // заполняет список чекбоксов тем
        private void FillThemesCheckBoxList(Author author)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> themes;
            if(author != null)
            {
                themes = new HashSet<int>(author.Theme.Select(c => c.id));
            }
            else
            {
                themes = new HashSet<int>();
            }
            var allThemes = db.Theme;
            var themesCheckBoxList = new List<SelectListItem>();
            foreach (var theme in allThemes)
            {
                themesCheckBoxList.Add(new SelectListItem
                {
                    Value = theme.id.ToString(),
                    Text = theme.name,
                    Selected = themes.Contains(theme.id)
                });
            }
            ViewBag.ThemesList = themesCheckBoxList;
        }

        //
        // GET: /Author/Delete/5
 
        public ActionResult Delete(int id)
        {
            Author author = db.Author.Single(a => a.id == id);
            return View(author);
        }

        //
        // POST: /Author/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Author author = db.Author.Single(a => a.id == id);
            db.Author.DeleteObject(author);
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