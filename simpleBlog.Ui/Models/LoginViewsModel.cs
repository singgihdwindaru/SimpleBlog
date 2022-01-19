using System.ComponentModel.DataAnnotations;
namespace simpleBlog.Ui.Models
{
    public class LoginViewsModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = false;

        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}
