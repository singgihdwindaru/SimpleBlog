using System;
using System.Collections.Generic;

#nullable disable

namespace simpleBlog.Api.data
{
    public partial class User
    {
        public User()
        {
            Artikels = new HashSet<Artikel>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Artikel> Artikels { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
