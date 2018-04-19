using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class City
    {
        public City()
        {
            Article = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Article> Article { get; set; }
    }
}
