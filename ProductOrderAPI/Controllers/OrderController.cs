using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary;
using UtilitiesLibrary;
using InterfaceLibrary;
using Swashbuckle.AspNetCore.Annotations;
using Utilities_Library;
using Microsoft.Data.SqlClient;


namespace ProductOrderAPI.Controllers
{
    //[Authorize(Policy = "AdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerTag("EndPoints for OrderController")]
    public class OrderController : ControllerBase
    {
        private readonly InputParameterValidator _inputParameterValidator;
        private readonly IOrderService _orderService;

        //InputParameterValidator inputParameterValidator=new InputParameterValidator();

        public OrderController(OrderService orderService,InputParameterValidator inputParameterValidator)
        {
            this._orderService = orderService;
            this._inputParameterValidator = inputParameterValidator;
        }
        //Get Method for retrive orders by product Id
        //Get /api/Order/productId
        /// <summary>
        /// Get Orders By ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrdersByProductId(int productId)
        {
            var orders=new List<IOrder>();
            try
            {
                if (productId > 0)
                {
                    orders = _orderService.GetOrdersByProductId(productId).ToList<IOrder>();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("ID must be greater tha zero");
                }
                // Check wheather order count is zero 

                if (orders.Count == 0)
                {
                    return NotFound("Order Not Found");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(orders);
        }
        /// <summary>
        /// Post action for create a new order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNewOrder([FromBody] OrderDTO orderDTO) 
        {
            try
            {
                if (orderDTO.ProductID <= 0 || orderDTO.Quantity <= 0)
                {
                    throw new ArgumentException("Enter valid parameters(ProductId must greater than zero,Quantity must greater than zero ");
                }
                else
                {
                    _orderService.CreateNewOrder(orderDTO.ProductID, orderDTO.Date, orderDTO.Quantity);
                    return Ok("New Order Created Successfully");
                }
            }
            catch(ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (SqlException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// Put Action for Update Order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateOrder([FromBody] UpdateOrderDTO orderDTO) 
        {
            try
            {
                if (orderDTO.ID<=0 ||orderDTO.ProductID <= 0 || orderDTO.Quantity <= 0)
                {
                    throw new ArgumentException("Enter valid parameters(ID,ProductId and Quantity must greater than zero");
                }
                else
                {
                    _orderService.UpdateOrder(orderDTO.ID,orderDTO.ProductID, orderDTO.Date, orderDTO.Quantity);
                    return Ok("Order Details Updated Successfully");
                }
            }

            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (SqlException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        /// <summary>
        /// Delete Action for Delete Order
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrder(int Id) 
        {
            try
            {
                if (Id <= 0)
                {
                    throw new ArgumentException("Enter valid parameters(Id not equal or less than 0");
                }
                else
                {
                    _orderService.DeleteOrder(Id);
                    return Ok("Order Deleted Successfully");
                }
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (SqlException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
