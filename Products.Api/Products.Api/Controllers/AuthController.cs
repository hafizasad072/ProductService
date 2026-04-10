using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products.BO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwt;

        public AuthController(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }

        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "product-user") };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Key));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds);

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
