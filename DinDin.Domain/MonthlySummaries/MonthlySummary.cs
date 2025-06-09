namespace DinDin.Domain.MonthlySummaries
{
    public class MonthlySummary
    {
        public int Id { get; set; }
        public int Month { get; set; } = DateTime.UtcNow.Month;
        public int Year { get; set; } = DateTime.UtcNow.Year;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public int UserId { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}