using DinDin.Domain.Constantes;
using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using FluentValidation;

namespace DinDin.Services.Transactions
{
    public class TransactionService(
        ITransactionRepository transactionRepository, 
        IMonthlySummaryRepository monthlySummaryRepository, 
        IValidator<Transaction> validator)
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMonthlySummaryRepository _monthlySummaryRepository = monthlySummaryRepository;
        private readonly IValidator<Transaction> _validator = validator;

        public async Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId)
        {
            return await _transactionRepository.GetAllByMonthlySummaryId(monthlySummaryId);
        }

        public async Task DeleteTransaction(int id)
        {
            await _transactionRepository.Delete(id);
        }

        public async Task AddTransaction(Transaction transaction, int userId)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(transaction, options =>
                    options.IncludeRuleSets(ApplicationConstants.TRANSACTION_CREATE_RULE_SET_NAME));

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                var monthlySummary = await GetByTransactionDate(transaction.TransactionDate, userId);

                if (monthlySummary is null)
                    transaction.MonthlySummaryId = await _monthlySummaryRepository.Add(transaction, userId);
                else
                {
                    transaction.MonthlySummaryId = monthlySummary.Id;
                    await _monthlySummaryRepository.Update(transaction);
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

        public async Task UpdateTransaction(Transaction transaction)
        {
            try
            {
                await _transactionRepository.Update(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}