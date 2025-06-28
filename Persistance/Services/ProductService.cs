using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Abstractions.Services;
using Onion.Application.DTOs.ProductDTOs;
using Onion.Application.Repositories;
using Onion.Domain.Entities.Models;

namespace Onion.Persistance.Services
{
    public class ProductService : IProductService
    {
        private readonly IWriteRepository<Product> _productWriteRepository;
        private readonly IReadRepository<Product> _productReadRepository;
        private readonly IReadRepository<Category> _categoryReadRepository;

        public ProductService(
            IWriteRepository<Product> productWriteRepository,
            IReadRepository<Product> productReadRepository,
            IReadRepository<Category> categoryReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productReadRepository
                .GetAll()
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productReadRepository
                .GetWhere(p => p.Id == id)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();

            if (product == null)
                throw new Exception($"Product with ID {id} not found.");

            return product;
        }

        public async Task<Product> CreateProductAsync(CreateProductDTOs dto)
        {
            
            var products = await _categoryReadRepository.GetWhere(c => c.Id == dto.CategoryId).FirstOrDefaultAsync();
            if (products == null)
                throw new Exception($"Category with ID {dto.CategoryId} not found.");

            
            var imagePath = await SaveImageAsync(dto.Image);

            
            var product = new Product
            {
                Name = dto.ProductName,
                Description = dto.ProductDescription,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                ImagePath = imagePath
            };

            var result = await _productWriteRepository.AddAsync(product);
            if (!result)
                throw new Exception("Failed to add product.");

            await _productWriteRepository.SaveAsync();
            return product;
        }


        public async Task<bool> UpdateProductAsync(int id,UpdateProductDTOs dto)
        {
            var existingProduct = await _productReadRepository.GetByIdAsync(id);
            if (existingProduct == null)
                throw new Exception($"Product with ID {id} not found.");

            var category = await _categoryReadRepository.GetWhere(c => c.Id == dto.CategoryId).FirstOrDefaultAsync();
            if (category == null)
                throw new Exception($"Category with ID {dto.CategoryId} not found.");

            existingProduct.Name = dto.ProductName;
            existingProduct.Description = dto.ProductDescription;
            existingProduct.Price = dto.Price;
            existingProduct.CategoryId = dto.CategoryId;

            if (dto.Image != null)
            {
                var imagePath = await SaveImageAsync(dto.Image);
                existingProduct.ImagePath = imagePath;
            }

            var updated = _productWriteRepository.Update(existingProduct);
            if (!updated) return false;

            await _productWriteRepository.SaveAsync();
            return true;
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await _productWriteRepository.RemoveAsync(id);
            if (!result) return false;

            await _productWriteRepository.SaveAsync();
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new Exception("Image file is empty.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var savePath = Path.Combine("wwwroot", "images", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath)); 

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/images/{fileName}"; 
        }

    }

}
