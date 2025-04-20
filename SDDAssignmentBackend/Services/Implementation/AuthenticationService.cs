using Microsoft.IdentityModel.Tokens;
using SDDAssignmentBackend.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SDDAssignmentBackend.Services.Interface;
using SDDAssignmentBackend.Entities;
using Microsoft.Extensions.Options;
using SDDAssignmentBackend.Configurations.Options;
using SDDAssignmentBackend.Helpers;

namespace SDDAssignmentBackend.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtOption> _jwtOption;
        public AuthenticationService(IUserService userService, IOptions<JwtOption> jwtOption)
        {
            _userService = userService;
            _jwtOption = jwtOption;
        }
        public async Task<LoginDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _userService.GetUser(loginRequestDTO.UserName, loginRequestDTO.Password);
            return GenerateJwtToken(user);
        }

        private LoginDTO GenerateJwtToken(UserEntity user)
        {
            byte[] secret = Encoding.ASCII.GetBytes(_jwtOption.Value.Key);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOption.Value.Issuer,
                Audience = _jwtOption.Value.Audience,
                Subject = new ClaimsIdentity(
                [
                    //new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    //for refrash token
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOption.Value.Expired),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            //handler.WriteToken(token);
            return new LoginDTO()
            {
                Token = handler.WriteToken(token),
                UserName = user.Username,
                ExpiredInMinute = _jwtOption.Value.Expired,
                Role = user.Role
            };
        }

    }
}
