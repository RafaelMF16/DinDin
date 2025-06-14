using DinDin.Domain.MonthlySummaries;
using FluentValidation;

namespace DinDin.Services.MonthlySummaries
{
    public class MonthlySummaryService(IMonthlySummaryRepository monthlySummaryRepository, IValidator<MonthlySummary> monthlySummaryValidator)
    {
        private readonly IMonthlySummaryRepository _monthlySummaryRepository = monthlySummaryRepository;
        private readonly IValidator<MonthlySummary> _monthlySummaryValidator = monthlySummaryValidator;

        public async Task Add(MonthlySummary monthlySummary)
        {
            try
            {
                await _monthlySummaryValidator.ValidateAndThrowAsync(monthlySummary);
                //await _monthlySummaryRepository.Add(monthlySummary);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<MonthlySummary>> GetAllWithUserId(int id)
        {
            return await _monthlySummaryRepository.GetAllWithUserId(id);
        }

        public async Task<MonthlySummary> GetById(int id)
        {
            return await _monthlySummaryRepository.GetById(id);
        }
    }
}