using DinDin.Domain.MonthlySummaries;
using DinDin.Services.MonthlySummaries;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlySummaryController : ControllerBase
    {
        private readonly MonthlySummaryService _monthlySummaryService;

        public MonthlySummaryController(MonthlySummaryService monthlySummaryService)
        {
            _monthlySummaryService = monthlySummaryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonthlySummary([FromBody] MonthlySummaryDto monthlySummaryDto)
        {
            var newMonthlySummary = new MonthlySummary 
            {
                Month = monthlySummaryDto.Month,
                Year = monthlySummaryDto.Year,
                TotalIncome = monthlySummaryDto.TotalIncome,
                TotalExpense = monthlySummaryDto.TotalExpense
            };

            await _monthlySummaryService.Add(newMonthlySummary);

            return Ok();
        }
    }
}
