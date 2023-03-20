using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public OrdersController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }

        // GET: api/Orders
        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            IEnumerable<Orders> orders = new List<Orders>();
            ActionResult result = null;
            try
            {
                orders = _dynamicEcommerceRepo.GetOrders();
                result = Ok(orders);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting Orders {ex.Message}");
            }

            return result;
        }

        // GET api/Orders/id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrder(int id)
        {
            Orders order = null;
            ActionResult result = null;

            try
            {
                order = _dynamicEcommerceRepo.GetOrderById(id);
                if (order == null)
                {
                    result = NotFound($"Order with id {id} not found.");
                }
                else
                {
                    result = Ok(order);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Order {ex.Message}");
            }

            return result;
        }
        // GET api/Orders/userID
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<Orders>> GetOrderByUserId(int userId)
        {
            Orders order = null;
            ActionResult result = null;

            try
            {
                order = _dynamicEcommerceRepo.GetOrderByUserId(userId);
                if (order == null)
                {
                    result = NotFound($"Order with UserID {userId} not found.");
                }
                else
                {
                    result = Ok(order);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Order {ex.Message}");
            }

            return result;
        }

        // POST api/<OrdersController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Orders>> AddOrder(Orders order)
        {
            ActionResult result = null;
            try
            {
                if (order == null)
                {
                    result = BadRequest();
                }
                else
                {
                    if (_dynamicEcommerceRepo.AddOrder(order))
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
                    $"Error creating new Order record {ex.Message}");
            }

            return result;
        }

        // DELETE api/<OrdersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> DeleteOrder(int Id)
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
                    if (_dynamicEcommerceRepo.DeleteOrder(Id))
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
                    $"Error delete Order {ex.Message}");
            }

            return result;
        }
    }
}
