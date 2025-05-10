using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using FluentValidation;

namespace DinDin.Services.MonthlySummaries
{
    public class MonthlySummaryService
    {
        private readonly IMonthlySummaryRepository _monthlySummaryRepository;
        private readonly IValidator<MonthlySummary> _monthlySummaryValidator;

        public MonthlySummaryService(IMonthlySummaryRepository monthlySummaryRepository, IValidator<MonthlySummary> monthlySummaryValidator)
        {
            _monthlySummaryRepository = monthlySummaryRepository;
            _monthlySummaryValidator = monthlySummaryValidator;
        }

        public async Task Add(MonthlySummary monthlySummary)
        {
            try
            {
                await _monthlySummaryValidator.ValidateAndThrowAsync(monthlySummary);
                await _monthlySummaryRepository.Add(monthlySummary);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                await _monthlySummaryRepository.Delete(id);
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<MonthlySummary>> GetAllWithUserId(string id)
        {
            return await _monthlySummaryRepository.GetAllWithUserId(id);
        }

        public async Task<MonthlySummary> GetById(string id)
        {
            return await _monthlySummaryRepository.GetById(id)
                ?? throw new ArgumentNullException($"Not find monthly summary with id: {id}");
        }

        public async Task AddTransaction(Transaction transaction, string userId)
        {
            var monthlySummary = await _monthlySummaryRepository.GetByMonthAndYear(transaction, userId);

            _monthlySummaryRepository.AddTransactionInMonthlySummary(monthlySummary, transaction);
        }

        public async Task Update(MonthlySummary monthlySummary)
        {
            try
            {
                await _monthlySummaryValidator.ValidateAndThrowAsync(monthlySummary);
                await _monthlySummaryRepository.Update(monthlySummary);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}