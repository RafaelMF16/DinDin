using DinDin.Domain.Constantes;
using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Domain.Transactions.Enums;
using FluentValidation;

namespace DinDin.Services.Transactions
{
    public class TransactionService(
        ITransactionRepository transactionRepository, 
        IMonthlySummaryRepository monthlySummaryRepository, 
        IValidator<Transaction> transactionValidator,
        IValidator<MonthlySummary> monthlySummaryValidator)
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMonthlySummaryRepository _monthlySummaryRepository = monthlySummaryRepository;
        private readonly IValidator<Transaction> _transactionValidator = transactionValidator;
        private readonly IValidator<MonthlySummary> _monthlySummaryValidator = monthlySummaryValidator;

        public async Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId)
        {
            return await _transactionRepository.GetAllByMonthlySummaryId(monthlySummaryId);
        }

        public async Task DeleteTransaction(int id)
        {
            var transaction = await _transactionRepository.GetById(id);
            await RemoveAmountOfMonthlySummary(transaction);

            await _transactionRepository.Delete(id);
        }

        private async Task RemoveAmountOfMonthlySummary(Transaction transaction)
        {
            try
            {
                var monthlySummary = await _monthlySummaryRepository.GetById(transaction.MonthlySummaryId);

                if (transaction.Type == TransactionType.Expense)
                    monthlySummary.TotalExpense -= transaction.Amont;
                else
                    monthlySummary.TotalIncome -= transaction.Amont;


                await _monthlySummaryValidator.ValidateAndThrowAsync(monthlySummary);
                await _monthlySummaryRepository.Update(monthlySummary);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddTransaction(Transaction transaction, int userId)
        {
            try
            {
                var validationResult = await _transactionValidator.ValidateAsync(transaction, options =>
                    options.IncludeRuleSets(ApplicationConstants.TRANSACTION_CREATE_RULE_SET_NAME));

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                var monthlySummary = await GetByTransactionDate(transaction.TransactionDate, userId);

                if (monthlySummary is null)
                {
                    var newMonthlySummary = CreateMonthlySummary(transaction, userId);

                    await _monthlySummaryValidator.ValidateAndThrowAsync(newMonthlySummary);
                    transaction.MonthlySummaryId = await _monthlySummaryRepository.Add(newMonthlySummary);
                }
                else
                {
                    if (transaction.Type == TransactionType.Expense)
                        monthlySummary.TotalExpense += transaction.Amont;
                    else
                        monthlySummary.TotalIncome += transaction.Amont;

                    transaction.MonthlySummaryId = monthlySummary.Id;

                    await _monthlySummaryValidator.ValidateAndThrowAsync(monthlySummary);
                    await _monthlySummaryRepository.Update(monthlySummary);
                }

                await _transactionRepository.Add(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<MonthlySummary?> GetByTransactionDate(DateTime transactionDate, int userId)
        {
            var monthlySummaries = await _monthlySummaryRepository.GetAllWithUserId(userId);

            return monthlySummaries.FirstOrDefault(monthlySummary => monthlySummary.Year == transactionDate.Year && monthlySummary.Month == transactionDate.Month);
        }

        private static MonthlySummary CreateMonthlySummary(Transaction transaction, int userId)
        {
            var monthlySummary = new MonthlySummary
            {
                Month = transaction.TransactionDate.Month,
                Year = transaction.TransactionDate.Year,
                UserId = userId,
            };

            if (transaction.Type == TransactionType.Expense)
                monthlySummary.TotalExpense = transaction.Amont;
            else
                monthlySummary.TotalIncome = transaction.Amont;

            return monthlySummary;
        }
    }
}