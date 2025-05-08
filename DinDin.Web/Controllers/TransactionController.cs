using DinDin.Domain.Transactions;
using DinDin.Services.Transactions;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(TransactionService transactionService) : ControllerBase
    {
        private readonly TransactionService _transactionService = transactionService;

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            await _transactionService.Add(transaction);
            return Created();
        }
    }
}
