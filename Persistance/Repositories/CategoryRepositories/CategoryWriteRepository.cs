using Onion.Application.Repositories;
using Onion.Application.Repositories.CategoryRepositories;
using Onion.Application.Repositories.ProductRepositories;
using Onion.Domain.Entities.Models;
using Onion.Persistance.Context;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Persistance.Repositories.CategoryRepositories
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
