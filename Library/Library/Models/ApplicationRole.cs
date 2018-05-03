using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int Id { get; set; }

        public ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}