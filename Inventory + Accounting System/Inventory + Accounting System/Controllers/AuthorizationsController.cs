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
    public class AuthorizationsController : ControllerBase
    {
        private readonly Iauthservice _authservice;
        private readonly ILogger<AuthorizationsController> _logger;
        public AuthorizationsController(Iauthservice authservice, ILogger<AuthorizationsController> logger)
        {
            _authservice = authservice;
            _logger = logger;
        }

            [HttpPost("register")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Register([FromForm] UserregisterDto userregisterDto)
            {
                try
                {
                    if (userregisterDto == null)
                    {
                        return BadRequest("User registration data is null.");
                    }
                    var response = await _authservice.Register(userregisterDto);
                    if (response.Success == true)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(response.Message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while registering the user.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
                }
            }
        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromForm] Logindto logindto)
        {
            var res = await _authservice.Loginuser(logindto);
            return Ok(res);
        }
      
        [HttpDelete("Delete-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deleteuser(string name)
        {
            if (HttpContext.Items["UserId"] is not int userid)
            {
                return Unauthorized("UserId not found in request.");
            }
            var res = await _authservice.Deleteuser(name, userid);
            return Ok(res);
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("Get all users")]
        public async Task<IActionResult> Getusers()
        {
            var res = await _authservice.Getusers();
            return Ok(res);
        }
        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> Refresh([FromForm] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var res = await _authservice.RefreshToken(refreshTokenRequestDto);
            return Ok(res);
        }
    }
    } 

