using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerSErvice _service;

        public LedgerController(ILedgerSErvice service)
        {
            _service = service;
        }
        [HttpPost("Add-entry")]
        public async Task<IActionResult> Add([FromBody]AddLedgerDto dto)
        {
            var res = await _service.AddEntry(dto);
            
                return Ok(res);
  
        }
        [HttpDelete("Delete-Ledger")]
        public async Task <IActionResult> Delete(int id)
        {
            var res = await _service.Delete(id);
            return Ok(res);
        }
        [HttpGet ("Get-Ledgers")]

        public async Task<IActionResult> Get()
        {
            var res = await _service.Get();
            return Ok(res);
        }
    }
}
