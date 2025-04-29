using DinDin.Domain.Transactions;

namespace DinDin.Domain.MonthlySummaries
{
    public class MonthlySummary
    {
        public string Id { get; set; }
        public int Month { get; set; } = DateTime.UtcNow.Month;
        public int Year { get; set; } = DateTime.UtcNow.Year;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public string UserId { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
        public List<Transaction> Transactions { get; set; } = [];
    }
}