using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Discipline
    {
        public Discipline()
        {
            DisciplineAuthor = new HashSet<DisciplineAuthor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }

        public ICollection<DisciplineAuthor> DisciplineAuthor { get; set; }
    }
}
