using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DynamicEcommerce.Models;
using DynamicEcommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDynamicEcommerceRepo _dynamicEcommerceRepo;

        public AuthController(IDynamicEcommerceRepo repository)
        {
            _dynamicEcommerceRepo = repository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            var storedUser = _dynamicEcommerceRepo.GetUserByUsername(user.Username);

            if (storedUser == null || storedUser.Password != user.Password)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("pYTsEa8PRUwF08dFBdxGSF8wiTJbdPA6RaZPFSKB18w75WKYSKLXkVyG9o3y3mC4aGECbJ66IizsU2RIcgoW60FWZqBYZXnYMreLCfS1JLubuMdb4fXl6GVH9MctjEqbQ7PjFfPJy70H5GOVkvvSYVspepqPmx0oM3SVodDZoPVvQt9yQ92c3TOVrxSaNZuUR1fXmRBY9MiBtkziUHx2kO5X20UkFR0bSR0H3bgsJFAy9VUUI33ZobPAQic1XZqtenJOhcrdcrLMO5hg46othMYsprZcTZbMntlCZ0iidtpBt69GavGIqp3eT8QA4QsXTMpDy7BQdFixzVADKadnXzyUlJ85WLsOyi4OTDdBVh7IsBVh09p0TGDjJ7Ajme2N82TG8qipCYtYKwWnv75ICxqYpYJ9cB5Zz2wWmAp89Ca7ElJAy2OAjLCMpTvQJ4xTfWxVldDL0ZKNrDRso8WPYE2vssbKgaq5OVQ9WEW1PjO2miAAinRx9wxOOdQk5fcI");

            if (storedUser != null)
            {
                var userRole = _dynamicEcommerceRepo.GetUserRoleByUserId(storedUser.UserID);
                if (userRole == null)
                {
                    // Gestire il caso in cui non esiste un UserRole per l'utente
                    return BadRequest("UserRole non trovato per l'utente");
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, storedUser.UserID.ToString()),
                    new Claim(ClaimTypes.Name, storedUser.Username),
                    new Claim(ClaimTypes.Role, userRole.RoleID.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(12),
                    Audience = "your_audience",
                    Issuer = "your_issuer",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token), username = storedUser.Username, userId = storedUser.UserID, roleId = userRole.RoleID  });
            }
            else
            {
                // Gestire il caso in cui storedUser è null
                return BadRequest("Utente non trovato");
            }
        }
    }
}




