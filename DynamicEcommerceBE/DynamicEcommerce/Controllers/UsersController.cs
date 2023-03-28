using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public UsersController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }

        //GET: api/Users
        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            IEnumerable<Users> users = new List<Users>();
            ActionResult result = null;
            try
            {
                users = _dynamicEcommerceRepo.GetUsers();
                result = Ok(users);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting Users {ex.Message}");
            }

            return result;
        }

        //GET:api/Users/id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            Users user = null;
            ActionResult result = null;

            try
            {
                user = _dynamicEcommerceRepo.GetUserById(id);
                if (user == null)
                {
                    result = NotFound($"User with id {id} not found.");
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

        // POST api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> AddUser([FromBody] Users user)
        {
            ActionResult result = null;
            try
            {
                if (user == null)
                {
                    result = BadRequest();
                }
                else
                {
                    Roles role = _dynamicEcommerceRepo.GetRoleById(2);
                    if (_dynamicEcommerceRepo.AddUser(user))
                    {
                        UserRole userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID };
                        if (_dynamicEcommerceRepo.AddUserRole(userRole))
                        {
                            result = Ok();
                        }
                        else
                        {
                            result = StatusCode(StatusCodes.Status500InternalServerError);
                        }
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
                    $"Error creating new user record {ex.Message}");
            }

            return result;
        }

        // PUT api/Users/id
        [Authorize(Roles = "2")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> PutUser(int id, Users putUser)
        {
            try
            {
                var user = _dynamicEcommerceRepo.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Name = putUser.Name;
                user.Surname = putUser.Surname;
                user.Username = putUser.Username;
                user.Password = putUser.Password;
                user.Field3 = putUser.Field3;
                user.Field4 = putUser.Field4;
                user.Birthdate = putUser.Birthdate;
                user.Field6 = putUser.Field6;
                user.Field7 = putUser.Field7;
                user.Field8 = putUser.Field8;

                var success = await _dynamicEcommerceRepo.PutUser(user);

                if (success)
                {
                    return Ok(user);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating user record: {ex.Message}");
            }
        }

        // DELETE api/Users/id
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int Id)
        {
            ActionResult result = null;
            try
            {
                if (Id == null)
                {
                    result = BadRequest();
                }
                else
                {
                    if (_dynamicEcommerceRepo.DeleteUser(Id))
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
                    $"Error delete user {ex.Message}");
            }

            return result;
        }

    }
}

