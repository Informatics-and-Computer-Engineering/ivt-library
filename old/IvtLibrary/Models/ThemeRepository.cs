using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace IvtLibrary.Models
{ 
    public class ThemeRepository : IThemeRepository
    {
        private readonly IvtLibraryEntities db;

        public ThemeRepository(IvtLibraryEntities db)
        {
            this.db = db;
        }

        public IQueryable<Theme> All
        {
            get { return db.Theme; }
        }

        public IQueryable<Theme> AllIncluding(params Expression<Func<Theme, object>>[] includeProperties)
        {
            IQueryable<Theme> query = db.Theme;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Theme Find(int id)
        {
            return db.Theme.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Theme theme)
        {
            if (theme.id == default(int)) {
                // New entity
                db.Theme.AddObject(theme);
            } else {
                // Existing entity
                db.Theme.Attach(theme);
                db.ObjectStateManager.ChangeObjectState(theme, EntityState.Modified);
            }
        }

        // заполняет список чекбоксов тем
        public List<SelectListItem> FillThemesCheckBoxList(IEnumerable<Theme> themes)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> themeIds = themes != null ? new HashSet<int>(themes.Select(c => c.id)) : new HashSet<int>();
            var allThemes = db.Theme;
            var themesCheckBoxList = new List<SelectListItem>();
            foreach (var theme in allThemes)
            {
                themesCheckBoxList.Add(new SelectListItem
                {
                    Value = theme.id.ToString(),
                    Text = theme.name,
                    Selected = themeIds.Contains(theme.id)
                });
            }
            return themesCheckBoxList;
        }

        public void Delete(int id)
        {
            var theme = db.Theme.Single(x => x.id == id);
            db.Theme.DeleteObject(theme);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}