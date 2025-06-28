using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Abstractions.Services;
using Onion.Application.DTOs.CategoryDTOs;
using Onion.Domain.Entities.Models;
using Onion.Persistance.Services;
using System.ComponentModel.DataAnnotations;

namespace Onion.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService _service) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPut("Add")]

        public async Task<ActionResult> CreateCategory(CreateCategoryDTOs dto)
        {

            try
            {
                Category category = await _service.CreateCategoryAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    Message = "Validasiya xətası baş verdi.",
                    Details = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Server xətası baş verdi.",
                    Details = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTOs dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateCategoryAsync(id, dto);
                if (!result)
                    return NotFound(new { message = $"Category with ID {id} not found." });

                return NoContent(); // 204: başarı
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            try
            {
                var category = await _service.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);
            if (!result)
                return NotFound(new { message = $"Category with ID {id} not found." });
            return NoContent();
        }


    }
}
