using DinDin.Services.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnumController(EnumService enumService) : ControllerBase
    {
        private readonly EnumService _enumService = enumService;

        [HttpGet("Type")]
        public IActionResult GetTypeEnum()
        {
            var types = _enumService.GetTypes();
            return Ok(types);
        }

        [HttpGet("IncomeCategories")]
        public IActionResult GetIncomeCategories()
        {
            var categories = _enumService.GetIncomeCategories();
            return Ok(categories);
        }

        [HttpGet("ExpenseCategories")]
        public IActionResult GetExpenseCategories()
        {
            var categories = _enumService.GetExpenseCategories();
            return Ok(categories);
        }
    }
}
