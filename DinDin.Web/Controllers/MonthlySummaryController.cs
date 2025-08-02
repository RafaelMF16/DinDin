using DinDin.Domain.Extensions;
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

        [Authorize]
        [HttpGet("get-all-with-user-id")]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            var monthlySummariesList = await _monthlySummaryService.GetAllWithUserId(userId);
            return Ok(monthlySummariesList);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var monthlySummary = await _monthlySummaryService.GetById(id);
            return Ok(monthlySummary);
        }
    }
}
