using System.ComponentModel.DataAnnotations;
namespace simpleBlog.Ui.Models
{
    public class LoginModel
    {
        public class Request
        {
            [Required]
            [DataType(DataType.Password)]
            public string password { get; set; } = string.Empty;

            [Display(Name = "Remember me?")]
            public bool rememberMe { get; set; } = false;

            [Required]
            public string username { get; set; } = string.Empty;
        }
        public class Response
        {
            public string username { get; set; }
            public string fullname { get; set; }
            public bool? is_active { get; set; }
            public string token { get; set; }
            public string refresh_token { get; set; }
            public string role { get; set; }
        }
    }
}
