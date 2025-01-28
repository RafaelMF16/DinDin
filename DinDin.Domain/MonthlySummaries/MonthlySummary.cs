using System.Transactions;

namespace DinDin.Domain.MonthlySummaries
{
    public class MonthlySummary
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
        public List<Transaction> Transactions { get; set; } = [];
    }
}