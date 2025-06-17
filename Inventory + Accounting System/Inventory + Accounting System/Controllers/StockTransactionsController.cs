using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionsController : ControllerBase
    {
        private readonly IStockTransactionServices _stockService;
        public StockTransactionsController(IStockTransactionServices stockService)
        {
            _stockService = stockService;
        }
        [HttpPost("Add-Transactions")]

        public async Task<IActionResult> Addtransactions([FromForm]AddStockTransactionDto addStockTransactionDto)
        {
            var res = await _stockService.AddTransactions(addStockTransactionDto);
            return Ok(res);
        }
        [HttpGet("Get-Transactions")]
        public async Task <IActionResult> GetTransactions()
        {
            var res = await _stockService.Get();
            return Ok(res);
        }
        [HttpDelete("Delete-transaction")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _stockService.Delete(id);
            return Ok(res);
        }
    }
}
