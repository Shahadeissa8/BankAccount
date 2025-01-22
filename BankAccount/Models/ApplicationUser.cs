using Microsoft.AspNetCore.Identity;

namespace BankAccount.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal Balance { get; set; }
    }
}
