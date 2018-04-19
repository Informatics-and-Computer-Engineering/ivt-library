using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Theme
    {
        public Theme()
        {
            ResearchTheme = new HashSet<ResearchTheme>();
            ThemeArticle = new HashSet<ThemeArticle>();
            ThemeAuthor = new HashSet<ThemeAuthor>();
            ThemeBook = new HashSet<ThemeBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ResearchTheme> ResearchTheme { get; set; }
        public ICollection<ThemeArticle> ThemeArticle { get; set; }
        public ICollection<ThemeAuthor> ThemeAuthor { get; set; }
        public ICollection<ThemeBook> ThemeBook { get; set; }
    }
}
