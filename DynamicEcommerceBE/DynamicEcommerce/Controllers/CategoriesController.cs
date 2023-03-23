using DynamicEcommerce.Interfaces;
using DynamicEcommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IDynamicEcommerceRepo _dynamicEcommerceRepo;
        public CategoriesController(IDynamicEcommerceRepo dynamicEcommerceRepo)
        {
            _dynamicEcommerceRepo = dynamicEcommerceRepo;
        }

        // GETIDCATBYCATEGORIES: api/ProductCategories/categorie
        [HttpGet]
        public async Task<ActionResult<ProductCategories>> GetIdByCategories(string categorie)
        {
            ProductCategories productCategoriesId = null;
            ActionResult result = null;

            try
            {
                productCategoriesId = _dynamicEcommerceRepo.GetIdByCategories(categorie);
                if (categorie == null)
                {
                    result = NotFound($"Id with Categorie {categorie} not found.");
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

            return productCategoriesId;
        }
    }
}
