using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICatservice _catservice;

        public CategoryController(ICatservice catservice)
        {
            _catservice = catservice;
        }
        [HttpPost("AddCategory")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Addcategory([FromForm] CategoryAdddto categoryAdddto)
        {
            try
            {
                var res = await _catservice.AddCategory(categoryAdddto);
                return Ok(res);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("Get-categories")]
        [Authorize]
        public async Task<IActionResult> Getcat()
        {
            var res = await _catservice.GetAllCategorys();
            return Ok(res);
        }
    }
}
