using Microsoft.AspNetCore.Mvc;
using Product.Models;
using Product.Services;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductRequest productUpdateRequest)
        {
            if (productUpdateRequest != null && productUpdateRequest.productList.Count > 0)
            {
                foreach (var model in productUpdateRequest.productList)
                {
                    await _productService.Update(model);
                }
            }
            return Ok(new { message = "Products Price Updated Successfully." });
        }
    }
}
