using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitAndLossController : ControllerBase
    {
        private readonly IProfitlossRepo _profitAndLossRepo;

        public ProfitAndLossController(IProfitlossRepo profitAndLossRepo)
        {
            _profitAndLossRepo = profitAndLossRepo;
        }
        [HttpGet("Get-ProfitandLoss")]
        public async Task<IActionResult> Get([FromQuery] string startDate, [FromQuery] string endDate)
        {
            if (!DateOnly.TryParseExact(startDate, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out var start))
                return BadRequest("Invalid startDate format. Use dd/MM/yyyy");

            if (!DateOnly.TryParseExact(endDate, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out var end))
                return BadRequest("Invalid endDate format. Use dd/MM/yyyy");

            var res = await _profitAndLossRepo.CalculateProfitandloss(start, end);
            return Ok(res);
        }
    }
}
