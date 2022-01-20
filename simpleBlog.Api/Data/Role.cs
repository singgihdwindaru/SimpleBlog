using System;
using System.Collections.Generic;

#nullable disable

namespace simpleBlog.Api.Data
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string RoleDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
