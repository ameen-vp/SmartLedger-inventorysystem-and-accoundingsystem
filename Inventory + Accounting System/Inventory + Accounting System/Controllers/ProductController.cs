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
        [HttpPost("Add-Products")]
        
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
        [HttpGet("Get-Allproducts")]
        public async Task<IActionResult> Getallproducts()
        {
            var res = await _iproductService.GetAllproducts();
            return Ok(res);
        }
        [HttpGet("Getproductbyid")]

        public async Task<IActionResult>Getproductid(int id)
        {
            var res = await _iproductService.Getproductbyid(id);
            return Ok(res);
        }
        [HttpPatch("update- products")]

        public async Task<IActionResult> Updateprod(int id ,[FromForm]productupdatedto dto)
        {
            var res = await _iproductService.Updateproduct(id, dto);
            return Ok(res);
        }

    }
}
