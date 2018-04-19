using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Draft
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
