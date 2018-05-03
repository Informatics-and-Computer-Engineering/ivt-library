using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class AspNetRoleClaims
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }

        public ApplicationRole Role { get; set; }
    }
}
