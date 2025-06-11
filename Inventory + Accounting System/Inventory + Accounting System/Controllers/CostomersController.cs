using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostomersController : ControllerBase
    {
        private readonly ICostomerService _costomerService;
        public CostomersController(ICostomerService costomerService)
        {
            _costomerService = costomerService;
        }

        [HttpPost("Add-Costomer")]
        public async Task<IActionResult> AddCostomer([FromForm] CostomerDto costomerDto)
        {
            var res = await _costomerService.AddCostomer(costomerDto);
            return Ok(res);
        }
        [HttpGet("Get-costomers")]

        public async Task<IActionResult> Get()
        {
            var res = await _costomerService.Get();
            return Ok(res);
        }
        [HttpDelete("Delete-Costomer/{id}")]
        public async Task<IActionResult> Delete(int id) 
        { 
            var res = await _costomerService.Delete(id);
            return Ok(res);
        }
    }
}
