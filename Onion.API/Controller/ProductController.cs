using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Abstractions.Services;
using Onion.Application.DTOs.CategoryDTOs;
using Onion.Application.DTOs.ProductDTOs;
using Onion.Domain.Entities.Models;
using Onion.Persistance.Services;

namespace Onion.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _service) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product>>> GetAllCategory()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPut("Add")]

        public async Task<ActionResult> CreateProduct(CreateProductDTOs dto)
        {
            await _service.CreateProductAsync(dto);
            return Ok(dto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTOs dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateProductAsync(id, dto);
                if (!result)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                var product = await _service.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProductAsync(id);
            if (!result)
                return NotFound(new { message = $"Product with ID {id} not found." });
            return NoContent();
        }
    }
}
