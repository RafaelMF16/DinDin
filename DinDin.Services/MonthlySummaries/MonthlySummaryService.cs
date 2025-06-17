using DinDin.Domain.MonthlySummaries;
using FluentValidation;

namespace DinDin.Services.MonthlySummaries
{
    public class MonthlySummaryService(IMonthlySummaryRepository monthlySummaryRepository, IValidator<MonthlySummary> monthlySummaryValidator)
    {
        private readonly IMonthlySummaryRepository _monthlySummaryRepository = monthlySummaryRepository;

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