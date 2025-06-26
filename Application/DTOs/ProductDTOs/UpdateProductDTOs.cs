using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.DTOs.ProductDTOs
{
    public class UpdateProductDTOs
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
