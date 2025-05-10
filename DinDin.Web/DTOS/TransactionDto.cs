namespace DinDin.Web.DTOS
{
    public record TransactionDto(string Type, string Category, decimal Amont, string Description, DateTime TransactionDate, string UserId);
}
