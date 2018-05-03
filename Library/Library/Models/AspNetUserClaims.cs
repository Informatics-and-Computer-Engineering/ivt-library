using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class AspNetUserClaims
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
