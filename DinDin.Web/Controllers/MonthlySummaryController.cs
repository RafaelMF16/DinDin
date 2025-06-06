using DinDin.Domain.MonthlySummaries;
using DinDin.Services.MonthlySummaries;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlySummaryController(MonthlySummaryService monthlySummaryService) : ControllerBase
    {
        private readonly MonthlySummaryService _monthlySummaryService = monthlySummaryService;

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

        [HttpGet("get-all-with-user-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromRoute] string id)
        {
            var monthlySummariesList = await _monthlySummaryService.GetAllWithUserId(id);
            return Ok(monthlySummariesList);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByID([FromRoute] string id)
        {
            var monthlySummary = await _monthlySummaryService.GetById(id);
            return Ok(monthlySummary);
        }
    }
}
