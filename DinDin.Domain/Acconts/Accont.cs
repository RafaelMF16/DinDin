using DinDin.Domain.MonthlySummaries;

namespace DinDin.Domain.Acconts
{
    public class Accont
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<MonthlySummary> MonthlySummaries { get; set; } = [];
    }
}