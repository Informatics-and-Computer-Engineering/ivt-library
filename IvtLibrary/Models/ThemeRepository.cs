using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using IvtLibrary;

namespace IvtLibrary.Models
{ 
    public class ThemeRepository : IThemeRepository
    {
        IvtLibraryEntities context = new IvtLibraryEntities();

        public IQueryable<Theme> All
        {
            get { return context.Theme; }
        }

        public IQueryable<Theme> AllIncluding(params Expression<Func<Theme, object>>[] includeProperties)
        {
            IQueryable<Theme> query = context.Theme;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Theme Find(int id)
        {
            return context.Theme.Single(x => x.id == id);
        }

        public void InsertOrUpdate(Theme theme)
        {
            if (theme.id == default(int)) {
                // New entity
                context.Theme.AddObject(theme);
            } else {
                // Existing entity
                context.Theme.Attach(theme);
                context.ObjectStateManager.ChangeObjectState(theme, EntityState.Modified);
            }
        }

        // заполняет список чекбоксов тем
        public List<SelectListItem> FillThemesCheckBoxList(IEnumerable<Theme> themes)
        {
            // получаем список тем, привязанных к автору, если он есть
            HashSet<int> themeIds;
            if (themes != null)
            {
                themeIds = new HashSet<int>(themes.Select(c => c.id));
            }
            else
            {
                themeIds = new HashSet<int>();
            }
            var allThemes = context.Theme;
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
            var theme = context.Theme.Single(x => x.id == id);
            context.Theme.DeleteObject(theme);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}