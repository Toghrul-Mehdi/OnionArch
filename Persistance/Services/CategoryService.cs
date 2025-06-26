using Microsoft.EntityFrameworkCore;
using Onion.Application.Abstractions.Services;
using Onion.Application.DTOs.CategoryDTOs;
using Onion.Application.Repositories;
using Onion.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Persistance.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IWriteRepository<Category> _categoryWriteRepository;
        private readonly IReadRepository<Category> _categoryReadRepository;

        public CategoryService(IWriteRepository<Category> categoryWriteRepository,
                               IReadRepository<Category> categoryReadRepository)
        {
            _categoryWriteRepository = categoryWriteRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryReadRepository.GetAll().ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryReadRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception($"Category with ID {id} not found.");
            return category;
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDTOs dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Category name cannot be empty.");

            var category = new Category
            {
                Name = dto.Name
            };

            var result = await _categoryWriteRepository.AddAsync(category);
            if (!result)
                throw new Exception("Failed to create category.");

            await _categoryWriteRepository.SaveAsync();
            return category;
        }
        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDTOs category)
        {
            var existingCategory = await _categoryReadRepository.GetByIdAsync(category.Id);
            if (existingCategory == null)
                throw new Exception($"Product with ID {category.Id} not found.");

            existingCategory.Name = category.Name;


            var updated = _categoryWriteRepository.Update(existingCategory);
            if (!updated) return false;

            await _categoryWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await _categoryWriteRepository.RemoveAsync(id);
            if (!result) return false;

            await _categoryWriteRepository.SaveAsync();
            return true;
        }

    }

}
