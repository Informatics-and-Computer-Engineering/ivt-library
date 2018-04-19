using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ResearchAuthor
    {
        public int AuthorId { get; set; }
        public int ResearchId { get; set; }

        public Author Author { get; set; }
        public Research Research { get; set; }
    }
}
