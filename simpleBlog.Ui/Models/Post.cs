﻿using System;
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
        public string id { get; set; } //= DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
       
        public string reporterId { get; set; } = string.Empty;
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

        public bool AreCommentsOpen(int commentsCloseAfterDays) =>
            this.pubDate.AddDays(commentsCloseAfterDays) >= DateTime.UtcNow;

        public string getEncodedLink() => $"/news/{System.Net.WebUtility.UrlEncode(this.Slug)}/";

        public string getLink() => $"/news/{this.Slug}/";

        public bool isVisible() => this.pubDate <= DateTime.UtcNow && this.isPublished;

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

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string RemoveReservedUrlCharacters(string text)
        {
            var reservedCharacters = new List<string> { "!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\", "^", "_", "'", "{", "}", "|", "~", "`", "+" };

            foreach (var chr in reservedCharacters)
            {
                text = text.Replace(chr, string.Empty, StringComparison.OrdinalIgnoreCase);
            }

            return text;
        }
    }
}
