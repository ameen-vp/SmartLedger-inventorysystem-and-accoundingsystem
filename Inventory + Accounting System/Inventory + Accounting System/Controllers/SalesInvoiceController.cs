using Application.Services;
using Applications.Dto;
using Applications.Interface;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly IIInvoicePdfGenerator _invoicePdfGenerator;
        public SalesInvoiceController(ISalesInvoiceService salesInvoiceService, IIInvoicePdfGenerator iInvoicePdfGenerator)
        {
            _salesInvoiceService = salesInvoiceService;
            _invoicePdfGenerator = iInvoicePdfGenerator;
        }
        [HttpPost("Sales-invoice")]
        public async Task<IActionResult> SalesInvoice([FromBody] ADDSalesInvoice salesInvoice)
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
        [HttpPatch("Update-Status")]

        public async Task<IActionResult> Update([FromForm] UpdateStatusDto dto)
        {
            var res = await _salesInvoiceService.UpdateSataus(dto);
            return Ok(res);
        }

        [HttpGet("GetInvoicePdf/{id}")]
        public async Task<IActionResult> GetInvoicePdf(int id)
        {
            var pdfBytes = await _salesInvoiceService.GenerateInvoicePdfAsync(id); 

            if (pdfBytes == null)
                return NotFound("Invoice not found or missing data.");

            return File(pdfBytes, "application/pdf", $"Invoice_{id}.pdf");
        }
        [HttpGet("GetInvoiceById")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var res = await _salesInvoiceService.GetInvoiceById(id);
            return Ok(res);
        }
        [HttpGet("GetStatus")]

        public async Task<IActionResult> Get(InvoiceStatus invoiceStatus)
        {
            var res = await _salesInvoiceService.Getstatus(invoiceStatus);
            return Ok(res);
        }
    }
}
