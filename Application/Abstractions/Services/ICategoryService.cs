using Onion.Application.DTOs.CategoryDTOs;
using Onion.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(CreateCategoryDTOs category);
        Task<bool> UpdateCategoryAsync(UpdateCategoryDTOs category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
