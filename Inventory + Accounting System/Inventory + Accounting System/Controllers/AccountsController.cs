using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Add-Accounts")]
        public async Task <IActionResult> Add([FromForm]AddaccountDto addaccountDto)
        {
            var res = await _accountService.AddAcount(addaccountDto);
            return Ok(res);
        }
    }
}
