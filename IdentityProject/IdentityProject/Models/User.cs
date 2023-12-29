using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter email address")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword",ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
