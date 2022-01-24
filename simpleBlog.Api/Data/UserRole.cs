using System;
using System.Collections.Generic;

#nullable disable

namespace simpleBlog.Api.data
{
    public partial class UserRole
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
