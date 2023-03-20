using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
            private IDynamicEcommerceRepo _dynamicEcommerceRepo;
            public LoginController(IDynamicEcommerceRepo dynamicEcommerceRepo)
            {
                _dynamicEcommerceRepo = dynamicEcommerceRepo;
            }

            //GET:api/Users/Username
            [HttpGet("{username}")]
        public async Task<ActionResult<Users>> GetUserByUsername(string username)
        {
            Users user = null;
            ActionResult result = null;

            try
            {
                user = _dynamicEcommerceRepo.GetUserByUsername(username);
                if (user == null)
                {
                    result = NotFound($"User with id {username} not found.");
                }
                else
                {
                    result = Ok(user);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting user {ex.Message}");
            }

            return result;
        } 
      
      
    }
}
