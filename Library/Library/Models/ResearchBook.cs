using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class ResearchBook
    {
        public int ResearchId { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public Research Research { get; set; }
    }
}
