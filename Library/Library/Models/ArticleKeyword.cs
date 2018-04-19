using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ArticleKeyword
    {
        public int ArticleId { get; set; }
        public int KeywordId { get; set; }

        public Article Article { get; set; }
        public Keyword Keyword { get; set; }
    }
}
