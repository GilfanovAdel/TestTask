using Core.Dto;
using Core.Entity;
using Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    public class UserController :  ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsersbyOrganization")]
        public async Task<List<UserDto>> GetUsersbyOrganization(string name)
        {
            return await _userService.GetUsersByOrganizationAsync(name);
        }
    }
}
