using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class AspNetUserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public ApplicationRole Role { get; set; }
        public ApplicationUser User { get; set; }
    }
}
