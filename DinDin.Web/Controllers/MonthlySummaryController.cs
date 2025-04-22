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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listMonthly = await _monthlySummaryService.GetAll();
            return Ok(listMonthly);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] string id)
        {
            var monthlySummary = await _monthlySummaryService.GetById(id);
            return Ok(monthlySummary);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _monthlySummaryService.Delete(id);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] MonthlySummaryDto monthlySummaryDto)
        {
            var newMonthlySummary = new MonthlySummary
            {
                Month = monthlySummaryDto.Month,
                Year = monthlySummaryDto.Year,
                TotalIncome = monthlySummaryDto.TotalIncome,
                TotalExpense = monthlySummaryDto.TotalExpense
            };

            await _monthlySummaryService.Update(newMonthlySummary);
            return NoContent();
        }

    }
}
