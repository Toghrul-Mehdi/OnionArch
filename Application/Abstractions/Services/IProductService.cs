using Onion.Application.DTOs.ProductDTOs;
using Onion.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(CreateProductDTOs product);
        Task<bool> UpdateProductAsync(int id,UpdateProductDTOs product);
        Task<bool> DeleteProductAsync(int id);
    }

}
