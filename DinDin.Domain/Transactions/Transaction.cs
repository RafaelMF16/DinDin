namespace DinDin.Domain.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public decimal Amont { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}