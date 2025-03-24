using APICRUDTESTING.Models;
using APICRUDTESTING.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICRUDTESTING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ManagementProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("AllProduct")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery] string name = null )
        {
            List<Product> result;

            if (string.IsNullOrEmpty(name))
            {
                result = await _productService.GetAllProduct();
            }
            else
            {
                result = await _productService.SearchProductsByKeyword(name);
            }

            return Ok(result);
        }

        [Route("AddProduct")]
        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product request) //saat add id tidak perlu diisi atau dibuat saja 0
        {
            var result = await _productService.AddProduct(request);
            return Ok(result);
        }


        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result == null)
                return NotFound("Product not found.");
            return Ok("Delete Product Successfully");
        }


        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product request)
        {
            var result = await _productService.UpdateProduct(id, request);
            if (result == null)
                return NotFound("Product Not Found.");
            return Ok(result);
        }


        [HttpGet("GetSingleProduct/{id}")]
        public async Task<ActionResult<List<Product>>> GetSingleProduct(int id)
        {
            var result = await _productService.GetSingleProduct(id);
            if (result == null)
                return NotFound("Product Not Found.");
            return Ok(result);
        }

    }
}
