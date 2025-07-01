using DinDin.Domain.Transactions.Enums;

namespace DinDin.Services.Enums
{
    public class EnumService
    {
        public Dictionary<int, string> GetExpenseCategories()
        {
            return Enum.GetValues(typeof(ExpenseCategories))
                .Cast<ExpenseCategories>()
                .ToDictionary(x => (int)x, x => x.ToString());
        }

        public Dictionary<int, string> GetIncomeCategories()
        {
            return Enum.GetValues(typeof(IncomeCategories))
                .Cast<IncomeCategories>()
                .ToDictionary(x => (int)x, x => x.ToString());
        }

        public Dictionary<int, string> GetTypes()
        {
            return Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .ToDictionary(x => (int)x, x => x.ToString());
        }
    }
}