using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Abstractions.Services;
using Onion.Application.DTOs.CategoryDTOs;
using Onion.Application.Repositories;
using Onion.Domain.Entities.Models;

namespace Onion.Persistance.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IWriteRepository<Category> _categoryWriteRepository;
        private readonly IReadRepository<Category> _categoryReadRepository;
        private readonly IValidator<CreateCategoryDTOs> _createvalidator;
        private readonly IValidator<UpdateCategoryDTOs> _updatevalidator;




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
            var validationResult = await _createvalidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
    .Select(e => new
    {
        PropertyName = e.PropertyName ?? "Bilinməyən sahə",
        ErrorMessage = e.ErrorMessage ?? "Bilinməyən xəta"
    })
    .ToList();

                throw new ValidationException(
                    $"Validasiya xətaları: {string.Join(", ", errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"))}");

            }


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
        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDTOs dto)
        {
            var validationResult = await _updatevalidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList();

                throw new ValidationException($"Validasiya xətaları: {string.Join(", ", errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"))}");
            }

            var existingCategory = await _categoryReadRepository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new Exception($"Product with ID {id} not found.");

            existingCategory.Name = dto.Name;


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
