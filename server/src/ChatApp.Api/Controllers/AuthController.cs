using ChatApp.Core.DataTransferObjects;
using ChatApp.Core.DataTransferObjects.Identity;
using ChatApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(ILogger<AuthController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userRegister)
        {
            var result = await _authenticationService.RegisterUserAsync(userRegister);

            if (!result.Succeeded)
                return BadRequest(ModelState);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
        {
            if(!await _authenticationService.ValidateUser(user))
                return Unauthorized();

            var tokenDto = await _authenticationService.CreateToken(populateExp: true);

            return Ok(tokenDto);
;       }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            try
            {
                var tokenDtoToReturn = await _authenticationService.RefreshToken(tokenDto);
                
                return Ok(tokenDtoToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Invalid refresh token");
                Console.WriteLine(ex);
                
                return Unauthorized("Invalid refresh token");
            }
        }

    }
}
