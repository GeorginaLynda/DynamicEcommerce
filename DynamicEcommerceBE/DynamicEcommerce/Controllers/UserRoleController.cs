using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public UserRoleController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }
        // GET: api/UserRoleController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRole()
        {
            IEnumerable<UserRole> userRoles = new List<UserRole>();
            ActionResult result = null;
            try
            {
                userRoles = _dynamicEcommerceRepo.GetUserRoles();
                result = Ok(userRoles);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting UserRoles {ex.Message}");
            }

            return result;
        }

        //METODO PER IL LOGIN
        //GET api/OrderDetails/orderId
        [HttpGet("{UserByUserRole}")]
        public async Task<ActionResult<UserRole>> GetUserRoleByUserId(int userId)
        {
            UserRole userRole = null;
            ActionResult result = null;

            try
            {
                userRole = _dynamicEcommerceRepo.GetUserRoleByUserId(userId);
                if (userRole == null)
                {
                    result = NotFound($"User Role with UserID {userId} not found.");
                }
                else
                {
                    result = Ok(userRole);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting User Role {ex.Message}");
            }

            return result;
        }

        // POST api/Userrole
        [HttpPost]
        public async Task<ActionResult<UserRole>> AddUserRole(UserRole userRole)
        {
            ActionResult result = null;
            try
            {
                if (userRole == null)
                {
                    result = BadRequest();
                }
                else
                {
                    if (_dynamicEcommerceRepo.AddUserRole(userRole))
                    {
                        result = Ok();
                    }
                    else
                    {
                        result = StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error creating new UserRole record {ex.Message}");
            }

            return result;
        }
    }
}
