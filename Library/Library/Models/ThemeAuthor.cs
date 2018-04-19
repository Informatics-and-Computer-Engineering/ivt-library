using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ThemeAuthor
    {
        public int ThemeId { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public Theme Theme { get; set; }
    }
}
