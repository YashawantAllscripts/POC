using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLibrary;
using InterfaceLibrary;
using Utilities_Library;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ProductOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductController : ControllerBase
    {
        
        private readonly IProductService _productService;

        public ProductController(ProductService productService)
        {
            this._productService = productService;
        }

        //Get Method for retrive all products
        // Get /api/Product

        [HttpGet("/api/Products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetProducts()
        {
            var products = new List<IProduct>();
            try
            {
                 products = _productService.GetAllProducts().ToList();
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error" + ex.Message);
            }
            return Ok(products);
        }

        //Get Method for retrive product by product Id
        //Get /api/Product/ProductId/id

        [HttpGet("ProductId/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProductById(int productId) 
        {
            var products = new List<IProduct>(); 
            try
            {
                if (productId > 0)
                {
                    products = _productService.GetProductsById(productId).ToList<IProduct>();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("ID must be greater tha zero");
                }
                // Check wheather Product is Empty or not 
                if (products.Count == 0)
                {
                    return NotFound("Product Not Found");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,ex.Message) ;
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);      
            }
            return Ok(products);
        }

        /// <summary>
        /// Post action Create New Product Action
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNewProduct([FromBody] ProductDTO productDTO)
        {
            IActionResult result;
            try
            {
                if (string.IsNullOrEmpty(productDTO.Name) || productDTO.Price <= 0 || string.IsNullOrEmpty(productDTO.Manufacturer))
                {
                    throw new ArgumentException("Enter valid parameters(Name not Null, Price must greater than zero, Manufacturer is not null");
                }
                else
                {
                    _productService.CreateNewProduct(productDTO.Name, productDTO.Price, productDTO.Manufacturer);
                    result= Ok("New Product Created Successfully");
                }
            }
            catch(ArgumentException ex)
            {
                result= StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (SqlException ex)
            {
                result = StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }
        return result;
        }
        /// <summary>
        /// Put action for updating a product
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateProduct([FromBody] ProductUpdateDTO productDTO)
        {
            IActionResult result;
            try
            {
                if (productDTO.ID<= 0 || string.IsNullOrEmpty(productDTO.Name) || productDTO.Price <= 0 || string.IsNullOrEmpty(productDTO.Manufacturer))
                {
                    throw new ArgumentException("Enter valid parameters(Id less than 0,Name not Null, Price must greater than zero, Manufacturer is not null");
                }
                else
                {
                    _productService.UpdateProduct(productDTO.ID,productDTO.Name, productDTO.Price, productDTO.Manufacturer);
                    
                    result= Ok("Product Details Updated Successfully");
                }
            }
            catch (ArgumentException ex)
            {
                result = StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (SqlException ex)
            {
                result = StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex)
            {
                result =  BadRequest(ex.Message);
            }
        return result;
        }
        /// <summary>
        /// Method Delete Product
        /// </summary>
        /// <param name="prodctId"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProduct(int Id)
        {
            IActionResult result;
            try
            {
                if (Id <= 0)
                {
                    throw new ArgumentException("Enter valid parameters(Id not equal or less than 0");
                }
                else
                {
                    _productService.DeleteProduct(Id);
                    result= Ok("Product Deleted Successfully");
                }
            }
            catch (ArgumentException ex)
            {
                result = StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
            catch (SqlException ex)
            {
                result = StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }
        return result;
        }
    }
}

