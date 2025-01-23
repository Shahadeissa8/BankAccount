using static BankAccount.Models.MyTransaction;

namespace BankAccount.Models.ViewModels
{
    public class FundsTransferViewModel
    {
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public Transactions TypeOT { get; set; }
        public decimal Balance { get; set; }
    }//reciver from reciver
}
