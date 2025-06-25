using Applications.Dto;
using Applications.Interface;
using Domain.Enum;
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
        [HttpGet("Get-Accounts")]
        public async Task<IActionResult> Get()
        {
            var res = await _accountService.Get();
            return Ok(res);
        }
        [HttpGet("Get-AccountsHeads")]
        public IActionResult Get(AccountType accountType)
        {
            var res = _accountService.GetByTypes(accountType);
            return Ok(res);
        }
        [HttpDelete("Delete-heads")]

        public async Task<IActionResult> Delete(int id)
        {
            var del = await _accountService.Delete(id);
            return Ok(del);
        }
    }
}
