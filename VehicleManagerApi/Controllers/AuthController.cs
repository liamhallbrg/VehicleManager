using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using VehicleManager.Models;

namespace VehicleManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(TokenRequest request)
        {
            if (request.Username != "vehicleManager" || request.Password != "vehicleManager")
            {
                return Problem("Not Authorized");
            }

            if(ValidateToken(request.JwtString))
            {
                return request.JwtString;
            }

            return CreateToken(request);
        }

        private ActionResult<string> CreateToken(TokenRequest request)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, request.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.UtcNow.AddDays(30),
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"]);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new()
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value!)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            try
            {
                tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


    }
}
