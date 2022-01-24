using System;
using System.Collections.Generic;

#nullable disable

namespace simpleBlog.Api.data
{
    public partial class Artikel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PubDate { get; set; }
        public int? AuthorId { get; set; }
        public int? Status { get; set; }
        public string Excerpt { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Status Author { get; set; }
        public virtual User AuthorNavigation { get; set; }
    }
}
