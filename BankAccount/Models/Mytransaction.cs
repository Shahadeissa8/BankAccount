namespace BankAccount.Models
{
    public class Mytransaction
    {
        public string TransactionId { get; set; }
        public Transactions Transaction { get; set; }
        public enum Transactions {Transfer, Deposit, Withdraw}
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
