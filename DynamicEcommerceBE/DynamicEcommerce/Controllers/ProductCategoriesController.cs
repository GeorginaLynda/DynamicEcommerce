using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public ProductCategoriesController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategories>>> GetCategories()
        {
            IEnumerable<ProductCategories> categories = new List<ProductCategories>();
            ActionResult result = null;
            try
            {
                categories = _dynamicEcommerceRepo.GetCategories();
                result = Ok(categories);

            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while getting ProductCategories {ex.Message}");
            }

            return result;
        }

        // GETbyID: api/ProductCategories/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategories>> GetCategorieById(int id)
        {
            ProductCategories categorie = null;
            ActionResult result = null;

            try
            {
                categorie = _dynamicEcommerceRepo.GetCategorieById(id);
                if (categorie == null)
                {
                    result = NotFound($"Categorie with id {id} not found.");
                }
                else
                {
                    result = Ok(categorie);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error getting ProductCategories {ex.Message}");
            }

            return result;
        }

       

        // POST api/ProductCategoriesController
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<ProductCategories>> AddCategorie(ProductCategories categorie)
        {
            ActionResult result = null;
            try
            {
                if (categorie == null)
                {
                    result = BadRequest();
                }
                else
                {
                    if (_dynamicEcommerceRepo.AddCategorie(categorie))
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
                    $"Error creating new Categorie record {ex.Message}");
            }

            return result;
        }

        //DELETE api/ProductCategories/id
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategories>> DeleteCategorie(int Id)
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
                    if (_dynamicEcommerceRepo.DeleteCategorie(Id))
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
                    $"Error delete categorie {ex.Message}");
            }

            return result;
        }
    }
}