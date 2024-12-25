using Core.Dto;
using Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")] 
        public async Task<LoginResponseDto> Register([FromBody] UserRegistrationDto dto)
        {

            return await _authService.RegisterAsync(dto);
               
        }
        [HttpPost("login")]
        public async Task<LoginResponseDto> Login([FromBody] LoginDto dto)
        {

            return await _authService.LoginAsync(dto);

        }
    }
}
