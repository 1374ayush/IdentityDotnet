using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityProject.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
