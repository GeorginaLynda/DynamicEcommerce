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
    public class OrderDetailsController : ControllerBase
    {
        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public OrderDetailsController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }


        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetOrdersDetails()
        {
            

            IEnumerable<OrderDetails> orderDetails = new List<OrderDetails>();
            ActionResult result = null;
            try
            {
                orderDetails = _dynamicEcommerceRepo.GetOrdersDetails();
                result = Ok(orderDetails);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting OrderDetails {ex.Message}");
            }

            return result;
        }

        // GET api/OrderDetails/id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetailsById(int id)
        {
            OrderDetails orderDetails = null;
            ActionResult result = null;

            try
            {
                orderDetails = _dynamicEcommerceRepo.GetOrderDetailsById(id);
                if (orderDetails == null)
                {
                    result = NotFound($"Order Details with id {id} not found.");
                }
                else
                {
                    result = Ok(orderDetails);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Order Details {ex.Message}");
            }

            return result;
        }

        //GET api/OrderDetails/orderId
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetailsByOrderId(int orderId)
        {
            OrderDetails orderDetails = null;
            ActionResult result = null;

            try
            {
                orderDetails = _dynamicEcommerceRepo.GetOrderDetailsByOrderId(orderId);
                if (orderDetails == null)
                {
                    result = NotFound($"Order Details with OrderID {orderId} not found.");
                }
                else
                {
                    result = Ok(orderDetails);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Order Details {ex.Message}");
            }

            return result;
        }

      
        // DELETE api/<OrderDetailsController>/productId
        [HttpDelete("{ProductId}")]
        public async Task<ActionResult<OrderDetails>> DeleteOrderDetailsByProductId(int productId)
        {
            ActionResult result = null;
            try
            {
                if (productId == null)
                {
                    result = BadRequest();
                }
                else
                {
                    if (_dynamicEcommerceRepo.DeleteOrderDetailsByProductId(productId))
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
                    $"Error delete Order Details {ex.Message}");
            }

            return result;
        }
    }
}
