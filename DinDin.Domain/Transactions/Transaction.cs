using DinDin.Domain.Transactions.Enums;

namespace DinDin.Domain.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public IncomeCategories? IncomeCategory { get; set; }
        public ExpenseCategories? ExpenseCategory { get; set; }
        public decimal Amont { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public int MonthlySummaryId { get; set; }
    }
}