using Onion.Application.Repositories.ProductRepositories;
using Onion.Domain.Entities.Models;
using Onion.Persistance.Context;

namespace Persistence.Repositories.ProductRepositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
