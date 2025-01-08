namespace DinDin.Domain.Acconts
{
    public class Accont
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }
}