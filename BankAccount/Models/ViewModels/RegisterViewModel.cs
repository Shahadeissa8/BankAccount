using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your name")]
        [MinLength(3, ErrorMessage = "Minimum 3 letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress]
        [MinLength(6, ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Confirm your email")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email not match")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }
        public string? Mobile { get; set; }
    }
}
