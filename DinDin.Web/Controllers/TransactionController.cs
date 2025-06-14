using System.Security.Claims;
using DinDin.Domain.Transactions;
using DinDin.Services.Transactions;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(TransactionService transactionService) : ControllerBase
    {
        private readonly TransactionService _transactionService = transactionService;

        [Authorize]
        [HttpGet("get-all-by-monthly-summary-id/{monthlySummaryId}")]
        public async Task<IActionResult> GetAllByMonthlySummaryId([FromRoute] int monthlySummaryId)
        {
            var transactions = await _transactionService.GetAllByMonthlySummaryId(monthlySummaryId);
            return Ok(transactions);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto transactionDto)
        {
            var userId = 0;
            if (User.Identity.IsAuthenticated)
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var transaction = new Transaction 
            {
                Amont = transactionDto.Amont,
                Description = transactionDto.Description,
                ExpenseCategory = transactionDto.ExpenseCategory,
                IncomeCategory = transactionDto.IncomeCategory,
                TransactionDate = transactionDto.TransactionDate,
                Type = transactionDto.Type,
                MonthlySummaryId = transactionDto.MonthlySummaryId
            };

            await _transactionService.AddTransaction(transaction, userId);
            return Created();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionDto transactionDto, [FromRoute] int id)
        {
            var transaction = new Transaction
            {
                Id = id,
                Amont = transactionDto.Amont,
                Description = transactionDto.Description,
                ExpenseCategory = transactionDto.ExpenseCategory,
                IncomeCategory = transactionDto.IncomeCategory,
                TransactionDate = transactionDto.TransactionDate,
                Type = transactionDto.Type,
                MonthlySummaryId = transactionDto.MonthlySummaryId
            };

            await _transactionService.UpdateTransaction(transaction);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            await _transactionService.DeleteTransaction(id);
            return NoContent();
        }
    }
}
