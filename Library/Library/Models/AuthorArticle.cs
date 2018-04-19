using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class AuthorArticle
    {
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }

        public Article Article { get; set; }
        public Author Author { get; set; }
    }
}
