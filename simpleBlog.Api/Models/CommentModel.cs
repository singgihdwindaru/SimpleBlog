using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace simpleBlog.Api.Models
{
    public class CommentModel
    {
        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        public bool IsAdmin { get; set; } = false;

        [Required]
        public DateTime PubDate { get; set; } = DateTime.UtcNow;

        public string RenderContent() => this.Content;
    }
}
