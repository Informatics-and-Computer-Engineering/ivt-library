using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ResearchArticle
    {
        public int ResearchId { get; set; }
        public int ArticleId { get; set; }

        public Article Article { get; set; }
        public Research Research { get; set; }
    }
}
