using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Scale
    {
        public Scale()
        {
            Conference = new HashSet<Conference>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Conference> Conference { get; set; }
    }
}
