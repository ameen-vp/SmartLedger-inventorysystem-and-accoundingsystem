using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentorsController : ControllerBase
    {
        private readonly IVendorService _vendors;
        public VentorsController(IVendorService vendorService)
        {
            _vendors = vendorService;
        }
        [HttpPost]
        public async Task<IActionResult> Addventor([FromForm]VendorAdddto vendorAdddto)
        {
            var res = await _vendors.Addvendors(vendorAdddto);
            return Ok(res);
        }
        [HttpGet("Getventors")]

        public async Task<IActionResult> Getventors()
        {
            var res = await _vendors.Get();
            return Ok(res);
        }
        [HttpGet("Get-vendors-ById")]
        public async Task<IActionResult> Getbyid( int id)
        {
            var res = await _vendors.GetventorById(id);
            return Ok(res);
        }
        [HttpDelete("Delete vendors")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _vendors.Delete(id);
            return Ok(res);
        }
    }
}
