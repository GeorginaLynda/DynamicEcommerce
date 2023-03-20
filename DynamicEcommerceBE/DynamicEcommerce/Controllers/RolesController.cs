using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public RolesController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            IEnumerable<Roles> role = new List<Roles>();
            ActionResult result = null;
            try
            {
                role = _dynamicEcommerceRepo.GetRoles();
                result = Ok(role);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting Roles {ex.Message}");
            }

            return result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRole(int roleId)
        {
            Roles role = null;
            ActionResult result = null;

            try
            {
                role = _dynamicEcommerceRepo.GetRoleById(2);
                if (role == null)
                {
                    result = NotFound($"Role with id {roleId} not found.");
                }
                else
                {
                    result = Ok(role);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Role {ex.Message}");
            }

            return result;
        }
    }
}
