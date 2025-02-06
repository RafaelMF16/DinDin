using DinDin.Domain.MonthlySummaries;

namespace DinDin.Services.MonthlySummaries
{
    public class MonthlySummaryService
    {
        private readonly IMonthlySummaryRepository _monthlySummaryRepository;

        public MonthlySummaryService(IMonthlySummaryRepository monthlySummaryRepository)
        {
            _monthlySummaryRepository = monthlySummaryRepository;
        }

        public void Add(MonthlySummary monthlySummary)
        {
            try
            {
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
            throw new NotImplementedException();
        }
    }
}