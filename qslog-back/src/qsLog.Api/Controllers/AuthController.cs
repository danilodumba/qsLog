using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using qsLog.Domains.Users.Repository;
using qsLog.Presentetion.Models;
using qsLog.Domains.Users;
using Microsoft.Extensions.Configuration;

namespace qsLog.Presentetion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        readonly IUserRepository _loginService;
        readonly IConfiguration _configuration;
        public AuthController(IUserRepository loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _loginService.GetByUserName(model.UserName);
                var modelError = new {
                     Success = false,
                };

                if (user == null)
                {
                    return Ok(modelError);
                }

                if (!user.PasswordEquals(model.Password))
                {
                    return Ok(modelError);
                }

                var userModel = new {
                    user.Name,
                    user.Email,
                    user.UserName,
                    IsAdmin = user.Administrator,
                    Success = true,
                    Token = GetToken(user)
                };

                return Ok(userModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetToken(User user)
        {
            var securityKey = _configuration.GetSection("SecurityKey").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            if (user.Administrator)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}