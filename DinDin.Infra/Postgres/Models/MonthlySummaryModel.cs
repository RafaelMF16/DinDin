namespace DinDin.Infra.Postgres.Models
{
    public class MonthlySummaryModel
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}