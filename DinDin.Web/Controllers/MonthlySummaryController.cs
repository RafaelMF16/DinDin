using DinDin.Services.MonthlySummaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlySummaryController(MonthlySummaryService monthlySummaryService) : ControllerBase
    {
        private readonly MonthlySummaryService _monthlySummaryService = monthlySummaryService;

        [HttpGet("get-all-with-user-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromRoute] int id)
        {
            var monthlySummariesList = await _monthlySummaryService.GetAllWithUserId(id);
            return Ok(monthlySummariesList);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var monthlySummary = await _monthlySummaryService.GetById(id);
            return Ok(monthlySummary);
        }
    }
}
