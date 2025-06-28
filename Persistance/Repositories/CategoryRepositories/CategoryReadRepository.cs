using Onion.Application.Repositories.CategoryRepositories;
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
    public class  CategoryReadRepository  : ReadRepository<Category> , ICategoryReadRepository
    {
        public CategoryReadRepository(AppDbContext context) : base(context) { }
    }
}
