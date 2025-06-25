using DinDin.Domain.Transactions.Enums;

namespace DinDin.Web.DTOS
{
    public record TransactionDto(
        TransactionType Type,
        IncomeCategories? IncomeCategory,
        ExpenseCategories? ExpenseCategory,
        decimal Amont,
        string Description,
        DateTime TransactionDate,
        int MonthlySummaryId);
}