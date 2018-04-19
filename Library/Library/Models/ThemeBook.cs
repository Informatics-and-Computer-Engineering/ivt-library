using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ThemeBook
    {
        public int ThemeId { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public Theme Theme { get; set; }
    }
}
