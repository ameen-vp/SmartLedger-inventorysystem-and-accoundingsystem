using Applications.Dto;
using Applications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory___Accounting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private readonly IJournalentryService _ournalentryService;

        public JournalEntryController(IJournalentryService journalentryService)
        {
            _ournalentryService = journalentryService;
        }

        [HttpPost("Add-journalEntrys")]

        public async Task<IActionResult> Addentry(JournalDtos journalDtos)
        {
            var res = await _ournalentryService.AddJournalEntrys(journalDtos);

            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _ournalentryService.Get();
            return Ok(res);
        }
    }
}
