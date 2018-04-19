using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ArticleArticle
    {
        public int HostArticleId { get; set; }
        public int ReferencedArticleId { get; set; }

        public Article HostArticle { get; set; }
        public Article ReferencedArticle { get; set; }
    }
}
