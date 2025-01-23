using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.Models
{
    public class MyTransaction
    {
        public string MyTransactionId { get; set; }
        public Transactions Transaction { get; set; }
        public enum Transactions { Transfer, Deposit, Withdraw }
        public decimal Amount { get; set; }
        public decimal NewAmount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
