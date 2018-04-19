using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ArticleBook
    {
        public int ArticleId { get; set; }
        public int BookId { get; set; }

        public Article Article { get; set; }
        public Book Book { get; set; }
    }
}
