using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Services.Implementation;
using SDDAssignmentBackend.Services.Interface;

namespace SDDAssignmentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateUser([FromBody] CreateUserDTO user)
        {
            await _userService.CreateUser(user);
            return Ok(Sucess("Done"));
        }
    }
}
