using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ResearchTheme
    {
        public int ResearchId { get; set; }
        public int ThemeId { get; set; }

        public Research Research { get; set; }
        public Theme Theme { get; set; }
    }
}
