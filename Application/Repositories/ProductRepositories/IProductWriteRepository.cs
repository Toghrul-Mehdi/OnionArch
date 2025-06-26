using Onion.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Repositories.ProductRepositories
{
    public interface IProductWriteRepository : IWriteRepository<Product>
    {
    }
}
