using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ThemeArticle
    {
        public int ThemeId { get; set; }
        public int ArticleId { get; set; }

        public Article Article { get; set; }
        public Theme Theme { get; set; }
    }
}
