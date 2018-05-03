using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Conference
    {
        public Conference()
        {
            Article = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public int? ScaleId { get; set; }

        public Scale Scale { get; set; }
        public ICollection<Article> Article { get; set; }
    }
}
