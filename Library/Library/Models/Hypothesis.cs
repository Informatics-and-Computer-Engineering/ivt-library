using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Hypothesis
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Explanation { get; set; }
    }
}
