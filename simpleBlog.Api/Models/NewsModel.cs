using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace simpleBlog.Api.Models
{
    public class NewsModel
    {
        public NewsModel()
        {
            categories = new List<string> { "" };
            tags = new List<string> { "" };
            comments = new List<CommentModel>
            {
                new CommentModel()
            };
        }
        [Required]
        public int artikelId { get; set; } = -1;
        [Required]
        public int reporterId { get; set; } = -1;
        [BindNever]
        public IList<string> categories { get; }
        [BindNever]
        public IList<string> tags { get; }
        [BindNever]
        public IList<CommentModel> comments { get; }

        [Required]
        public string content { get; set; } = string.Empty;

        [Required]
        public string excerpt { get; set; } = string.Empty;


        public bool isPublished { get; set; } = true;

        public DateTime lastModified { get; set; } = DateTime.UtcNow;

        public DateTime pubDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string title { get; set; } = string.Empty;

        public string reporterName { get; set; } = "Unknown";
    }

    public class NewsRequestModel
    {
        public int artikelId { get; set; }
    }
}
