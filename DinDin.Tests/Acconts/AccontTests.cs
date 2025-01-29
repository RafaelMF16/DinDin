using DinDin.Domain.Acconts;
using DinDin.Infra.Acconts;
using DinDin.Services.Acconts;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests.Acconts
{
    public class AccontTests : BaseTest
    {
        private readonly AccontService _accontService;

        public AccontTests()
        {
            _accontService = ServiceProvider.GetRequiredService<AccontService>();

            AccontSingleton.Instance.Clear();
        }

        [Fact]
        public void Must_be_able_to_create_a_new_accont()
        {
            var newAccont = new Accont
            {
                UserId = 1,
                MonthlySummaries = []
            };

            _accontService.Add(newAccont);

            Assert.Contains(AccontSingleton.Instance, user => user == newAccont);
        }
    }
}