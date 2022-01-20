using System;
using System.Collections.Generic;

#nullable disable

namespace simpleBlog.Api.Data
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public DateTime? Createddate { get; set; }
        public string Createdby { get; set; }
        public DateTime Updateddate { get; set; }
        public string Updatedby { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
