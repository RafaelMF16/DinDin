
using DinDin.Domain.Transactions.Enums;

namespace DinDin.Services.Enums
{
    public class EnumService
    {
        public List<string> GetTypes()
        {
            return
            [
                TransactionType.Income.ToString(),
                TransactionType.Expense.ToString()
            ];
        }
    }
}