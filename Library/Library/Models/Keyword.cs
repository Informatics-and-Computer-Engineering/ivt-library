using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Keyword
    {
        public Keyword()
        {
            ArticleKeyword = new HashSet<ArticleKeyword>();
            AuthorKeyword = new HashSet<AuthorKeyword>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ArticleKeyword> ArticleKeyword { get; set; }
        public ICollection<AuthorKeyword> AuthorKeyword { get; set; }
    }
}
