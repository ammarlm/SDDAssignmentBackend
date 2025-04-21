using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Services.Implementation;
using SDDAssignmentBackend.Services.Interface;

namespace SDDAssignmentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> PostUserLogin([FromBody] LoginRequestDTO loginRequestDTO)
        {
            return Ok(Sucess(await _authenticationService.Login(loginRequestDTO)));
        }
    }
}
