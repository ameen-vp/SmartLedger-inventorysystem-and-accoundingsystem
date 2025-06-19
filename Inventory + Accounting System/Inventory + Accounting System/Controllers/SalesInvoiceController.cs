using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _salesInvoiceService;
        public SalesInvoiceController(ISalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }
        [HttpPost("Sales-invoice")]
        public async Task<IActionResult> SalesInvoice([FromBody]ADDSalesInvoice salesInvoice)
        {
            var res = await _salesInvoiceService.AddInvoice(salesInvoice);
           return Ok(res);
        }
        [HttpGet("Get-Invoices")]
        public async Task<IActionResult> Get()
        {
            var res = await _salesInvoiceService.Get();
            return Ok(res);
        }
    }
}
