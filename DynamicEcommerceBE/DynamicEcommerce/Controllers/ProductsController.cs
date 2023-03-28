using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Drawing;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.Http;


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
        [HttpGet, DisableRequestSizeLimit]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            IEnumerable<Products> products = new List<Products>();
            ActionResult result = null;
            try
            {
                products = _dynamicEcommerceRepo.GetProducts();

                foreach (var product in products)
                {
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        byte[] imageBytes = Convert.FromBase64String(product.Image);
                        string imageSrc = Convert.ToBase64String(imageBytes);
                        product.Image = imageSrc;
                    }
                    if (product.UnitPrice.ToString().Length == 4)
                    {
                        product.UnitPrice = Convert.ToDecimal(product.UnitPrice);
                    }
                }
                
                result = Ok(products);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting Products {ex.Message} inner: {ex.InnerException}");
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

        
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<Products>> AddProduct([FromForm] Products product, IFormCollection formData)
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
                    

                    // Recupera i dati dell'immagine dal FormData
                    var image = formData.Files[0];

                    if (image.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            var base64String = Convert.ToBase64String(fileBytes);
                            product.Image = base64String;
                        }
                       
                        //int productCategoriesId = int.Parse(formData["productCategoriesId"]);
                        //ProductCategories productCategory = _dynamicEcommerceRepo.GetCategorieById(productCategoriesId);
                        //product.ProductCategoriesID = productCategoriesId;

                        //decimal price = decimal.Parse(formData["unitPrice"]);
                        //product.UnitPrice = price;
                    }
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
                     $"Error creating new Product record. {ex.Message}. Inner Exception: {ex.InnerException?.Message}");

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

                product.ProductCategoriesID = putProduct.ProductCategoriesID;
                product.UnitPrice = putProduct.UnitPrice;
                product.Image = putProduct.Image;
                product.Title = putProduct.Title;
                product.Field2 = putProduct.Field2;
                product.Field3 = putProduct.Field3;
                product.Field4 = putProduct.Field4;
               
                

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
