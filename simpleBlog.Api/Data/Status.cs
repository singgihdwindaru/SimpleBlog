using System;
using System.Collections.Generic;

#nullable disable

namespace simpleBlog.Api.data
{
    public partial class Status
    {
        public Status()
        {
            Artikels = new HashSet<Artikel>();
        }

        public int Id { get; set; }
        public string Nama { get; set; }

        public virtual ICollection<Artikel> Artikels { get; set; }
    }
}
