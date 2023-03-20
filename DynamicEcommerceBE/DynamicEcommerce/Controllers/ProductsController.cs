using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public ProductsController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }

        //GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            IEnumerable<Products> products = new List<Products>();
            ActionResult result = null;
            try
            {
                products = _dynamicEcommerceRepo.GetProducts();
                result = Ok(products);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting Products {ex.Message}");
            }

            return result;
        }

        //GET:api/Products/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            Products product = null;
            ActionResult result = null;

            try
            {
                product = _dynamicEcommerceRepo.GetProductById(id);
                if (product == null)
                {
                    result = NotFound($"Product with id {id} not found.");
                }
                else
                {
                    result = Ok(product);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Product {ex.Message}");
            }

            return result;
        }

        //GETBYIDCATEGORIE api/Products/ProductCategoriesID
        //GET:api/Products/id
        [HttpGet("{productCategoriesId}")]
        public async Task<ActionResult<Products>> GetProductByCategorie(int categorieId)
        {
            Products product = null;
            ActionResult result = null;

            try
            {
                product = _dynamicEcommerceRepo.GetProductByCategorie(categorieId);
                if (product == null)
                {
                    result = NotFound($"Product with categorieId {categorieId} not found.");
                }
                else
                {
                    result = Ok(product);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting Product {ex.Message}");
            }

            return result;
        }

        // POST api/Users
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<Products>> AddProduct(Products product)
        {
            ActionResult result = null;
            try
            {
                if (product == null)
                {
                    result = BadRequest();
                }
                else
                {
                    if (_dynamicEcommerceRepo.AddProduct(product))
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
                    $"Error creating new Product record {ex.Message}");
            }

            return result;
        }

        // PUT api/Products/id
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Products>> PutProduct(int id, Products putProduct)
        {
            try
            {
                var product = _dynamicEcommerceRepo.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                product.UnitPrice = putProduct.UnitPrice;
                product.Field1 = putProduct.Field1;
                product.Field2 = putProduct.Field2;
                product.Field3 = putProduct.Field3;
                product.Field4 = putProduct.Field4;
                product.Field5 = putProduct.Field5;
                

                var success = await _dynamicEcommerceRepo.PutProduct(product);

                if (success)
                {
                    return Ok(product);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating Product record: {ex.Message}");
            }
        }

        // DELETE api/Products/id
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProduct(int Id)
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
                    if (_dynamicEcommerceRepo.DeleteProduct(Id))
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
                    $"Error delete Product {ex.Message}");
            }

            return result;
        }
    }
}
