using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoiceController : ControllerBase
    {
        private readonly Ipurchaseinvoiceservice _ipurchaseinvoiceservice;
        public PurchaseInvoiceController(Ipurchaseinvoiceservice ipurchaseinvoiceservice)
        {
            _ipurchaseinvoiceservice = ipurchaseinvoiceservice;
        }
        [HttpPost("Add-invoice")]
        public async Task<IActionResult> Addinvoices([FromBody]AddPurchaseinvoiceDto addPurchaseinvoiceDto)
        {
            var res = await _ipurchaseinvoiceservice.AddInvoices(addPurchaseinvoiceDto);
            return Ok(res);
        }
        [HttpGet("Get-invoice")]

        public async Task<IActionResult> Get()
        {
            var res = await _ipurchaseinvoiceservice.GetInvoice();
            return Ok(res);
        }
        [HttpDelete("Delete-invoice")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _ipurchaseinvoiceservice.Delete(id);
            return Ok(res);
        }
        [HttpGet("Get-ById")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _ipurchaseinvoiceservice.GetInvoiceById(id);
            return Ok(res);
        }
    }
}
