using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IproductService _iproductService;

        public ProductController(IproductService iproductService)
        {
            _iproductService = iproductService;
        }
        [HttpPost]
        public async Task<IActionResult> Addproduct([FromForm] ProductAdddto productAdddto)
        {
            try
            {
                var res = await _iproductService.AddProduct(productAdddto);
                return Ok(res);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
