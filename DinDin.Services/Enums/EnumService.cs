using DinDin.Domain.Transactions.Enums;

namespace DinDin.Services.Enums
{
    public class EnumService
    {
        public List<string> GetExpenseCategories()
        {
            return
            [
                ExpenseCategories.Food.ToString(),
                ExpenseCategories.housing.ToString(),
                ExpenseCategories.Transport.ToString(),
                ExpenseCategories.Health.ToString(),
                ExpenseCategories.Education.ToString(),
                ExpenseCategories.leisure.ToString(),
                ExpenseCategories.Guys.ToString(),
                ExpenseCategories.Financial.ToString(),
                ExpenseCategories.donations.ToString(),
                ExpenseCategories.Others.ToString()
            ];
        }

        public List<string> GetIncomeCategories()
        {
            return
            [
                IncomeCategories.Wage.ToString(),
                IncomeCategories.Investments.ToString(),
                IncomeCategories.Gifts.ToString(),
                IncomeCategories.Refunds.ToString(),
                IncomeCategories.Others.ToString()
            ];
        }

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