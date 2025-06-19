using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpPost("Add-stock")]
        public async Task<IActionResult> Add([FromForm]StockAdddto stockAdddto)
        {
            var res = await _stockService.Addstock(stockAdddto);
            return Ok(res);
        }
        [HttpGet("Get-stocks")]
        public async Task<IActionResult> Get()
        {
            var res = await _stockService.Get();
            return Ok(res);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _stockService.GetById(id);
            return Ok(res);
        }
        [HttpDelete("Stock-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _stockService.Delete(id);
            return Ok(res);
        }
        //[HttpPut("Stock-update")]

        //public async Task<IActionResult> Update(Stockupdatedto stockupdatedto)
        //{
        //    var res = await _stockService.Updatestock(stockupdatedto);
        //    return Ok(res);
        //}
    }
}
