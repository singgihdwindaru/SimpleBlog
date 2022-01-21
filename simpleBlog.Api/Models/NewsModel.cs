using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Api.Models
{
    public class NewsModel
    {
        [Required]
        public int artikelId { get; set; } = -1;
        [Required]
        public int reporterId { get; set; } = -1;
        public IList<string> categories { get; } = new List<string>();

        public IList<string> tags { get; } = new List<string>();

        public IList<CommentModel> comments { get; } = new List<CommentModel>();

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
}
