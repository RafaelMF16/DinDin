using DinDin.Domain.MonthlySummaries;

namespace DinDin.Infra.MonthlySummaries
{
    public sealed class MonthlySummarySingleton : List<MonthlySummary>
    {
        private MonthlySummarySingleton() { }

        private static readonly Lazy<MonthlySummarySingleton> lazy = new(() => new MonthlySummarySingleton());

        public static MonthlySummarySingleton Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}