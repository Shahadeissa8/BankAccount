using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Enter your name")]
        [MinLength(3, ErrorMessage = "Minimum 3 letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}
