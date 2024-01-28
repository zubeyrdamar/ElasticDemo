using Elastic.API.Models.DTOs;
using Elastic.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elastic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            return Ok(await _productService.SaveAsync(request));
        }
    }
}
