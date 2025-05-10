using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Services.MonthlySummaries;
using DinDin.Web.DTOS;
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

        [HttpPost("add-transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                Type = transactionDto.Type,
                Category = transactionDto.Category,
                Amont = transactionDto.Amont,
                Description = transactionDto.Description,
                TransactionDate = transactionDto.TransactionDate
            };

            await _monthlySummaryService.AddTransaction(transaction, transactionDto.UserId);
            return Ok();
        }

        [HttpGet("get-all-with-user-id/{id}")]
        public async Task<IActionResult> GetAll([FromRoute] string id)
        {
            var monthlySummariesList = await _monthlySummaryService.GetAllWithUserId(id);
            return Ok(monthlySummariesList);
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
