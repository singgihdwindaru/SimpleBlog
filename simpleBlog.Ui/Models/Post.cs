using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
namespace simpleBlog.Ui.Models
{
    public class Post
    {
        public int? artikelId { get; set; } = -1;

        public int reporterId { get; set; } = -1;
        public string reporterName { get; set; } = string.Empty;
        public IList<string> categories { get; } = new List<string>();

        public IList<string> tags { get; } = new List<string>();

        public IList<Comment> comments { get; } = new List<Comment>();

        [Required]
        public string content { get; set; } = string.Empty;

        [Required]
        public string excerpt { get; set; } = string.Empty;


        public bool isPublished { get; set; } = true;

        public DateTime lastModified { get; set; } = DateTime.UtcNow;

        public DateTime pubDate { get; set; } = DateTime.UtcNow;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string title { get; set; } = string.Empty;
        public string renderContent()
        {
            var result = this.content;

            // Set up lazy loading of images/iframes
            if (!string.IsNullOrEmpty(result))
            {
                // Set up lazy loading of images/iframes
                var replacement = " src=\"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==\" data-src=\"";
                var pattern = "(<img.*?)(src=[\\\"|'])(?<src>.*?)([\\\"|'].*?[/]?>)";
                result = Regex.Replace(result, pattern, m => m.Groups[1].Value + replacement + m.Groups[4].Value + m.Groups[3].Value);

                // Youtube content embedded using this syntax: [youtube:xyzAbc123]
                var video = "<div class=\"video\"><iframe width=\"560\" height=\"315\" title=\"YouTube embed\" src=\"about:blank\" data-src=\"https://www.youtube-nocookie.com/embed/{0}?modestbranding=1&amp;hd=1&amp;rel=0&amp;theme=light\" allowfullscreen></iframe></div>";
                result = Regex.Replace(
                    result,
                    @"\[youtube:(.*?)\]",
                    m => string.Format(CultureInfo.InvariantCulture, video, m.Groups[1].Value));
            }

            return result;
        }

    }
}
