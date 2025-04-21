using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Services.Implementation;
using SDDAssignmentBackend.Services.Interface;

namespace SDDAssignmentBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        [HttpGet()]
        public async Task<IActionResult> GetUsers([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string orderBy, [FromQuery] string orderType, [FromQuery] string? search)
        {
            return Ok(Sucess(await _userService.GetUsers(page, pageSize, orderBy, orderType, search)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUser([FromRoute] Guid id)
        {
            return Ok(Sucess(await _userService.GetUser(id)));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostCreateUser([FromBody] CreateUserDTO user)
        {
            await _userService.CreateUser(user);
            return Ok(Sucess("Done"));
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutUpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO userDTO)
        {
            return Ok(Sucess(await _userService.UpdateUser(id, userDTO)));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOneUser([FromRoute] Guid id)
        {
            await _userService.DeleteUser(id);
            return Ok(Sucess("User deleted successfully"));
        }
    }
}
