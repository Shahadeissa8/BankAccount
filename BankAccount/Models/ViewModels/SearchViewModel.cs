using static BankAccount.Models.MyTransaction;

namespace BankAccount.Models.ViewModels
{
    public class SearchViewModel
    {
        public List<MyTransaction>? Transactions { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? SearchString { get; set; }
        public string? Type { get; set; }
    }
}
