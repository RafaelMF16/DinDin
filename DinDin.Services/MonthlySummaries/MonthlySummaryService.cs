using DinDin.Domain.MonthlySummaries;
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

        public void Add(MonthlySummary monthlySummary)
        {
            try
            {
                _monthlySummaryValidator.ValidateAndThrow(monthlySummary);
                _monthlySummaryRepository.Add(monthlySummary);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _monthlySummaryRepository.Delete(id);
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public List<MonthlySummary> GetAll()
        {
            return _monthlySummaryRepository.GetAll();
        }

        public MonthlySummary GetById(int id)
        {
            return _monthlySummaryRepository.GetById(id)
                ?? throw new ArgumentNullException($"Not find monthly summary with id: {id}");
        }

        public void Update(MonthlySummary monthlySummary)
        {
            try
            {
                _monthlySummaryValidator.ValidateAndThrow(monthlySummary);
                _monthlySummaryRepository.Update(monthlySummary);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}