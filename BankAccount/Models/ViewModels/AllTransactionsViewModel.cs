using System.ComponentModel;
using static BankAccount.Models.MyTransaction;

namespace BankAccount.Models.ViewModels
{
    public class AllTransactionsViewModel
    {
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        [DisplayName("Type of transaction")]
        public Transactions TypeOT { get; set; }
        public decimal Balance { get; set; }
    }
}
