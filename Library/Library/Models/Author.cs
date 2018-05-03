using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Author
    {
        public Author()
        {
            Article = new HashSet<Article>();
            AuthorArticle = new HashSet<AuthorArticle>();
            AuthorBook = new HashSet<AuthorBook>();
            AuthorKeyword = new HashSet<AuthorKeyword>();
            DisciplineAuthor = new HashSet<DisciplineAuthor>();
            ResearchAuthor = new HashSet<ResearchAuthor>();
            ThemeAuthor = new HashSet<ThemeAuthor>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public ICollection<Article> Article { get; set; }
        public ICollection<AuthorArticle> AuthorArticle { get; set; }
        public ICollection<AuthorBook> AuthorBook { get; set; }
        public ICollection<AuthorKeyword> AuthorKeyword { get; set; }
        public ICollection<DisciplineAuthor> DisciplineAuthor { get; set; }
        public ICollection<ResearchAuthor> ResearchAuthor { get; set; }
        public ICollection<ThemeAuthor> ThemeAuthor { get; set; }
    }
}
