using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class DisciplineAuthor
    {
        public int DisciplineId { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public Discipline Discipline { get; set; }
    }
}
