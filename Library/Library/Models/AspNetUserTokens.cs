using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class AspNetUserTokens
    {
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
    }
}
