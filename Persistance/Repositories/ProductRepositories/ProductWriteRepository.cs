using Onion.Application.Repositories.ProductRepositories;
using Onion.Domain.Entities.Models;
using Onion.Persistance.Context;

namespace Persistence.Repositories.ProductRepositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
