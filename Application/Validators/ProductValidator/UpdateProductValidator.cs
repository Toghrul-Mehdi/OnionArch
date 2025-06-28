using FluentValidation;
using Microsoft.AspNetCore.Http;
using Onion.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Validators.ProductValidator
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDTOs>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductName)
            .MaximumLength(100).WithMessage("Məhsulun adı maksimum 100 simvol ola bilər.");

            RuleFor(x => x.ProductDescription)
                .MaximumLength(500).WithMessage("Məhsul təsviri maksimum 500 simvol ola bilər.");

            RuleFor(x => x.Price)
                .GreaterThan(0).When(x => x.Price != default)
                .WithMessage("Qiymət 0-dan böyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).When(x => x.CategoryId != default)
                .WithMessage("Düzgün kateqoriya seçin.");

            RuleFor(x => x.Image)
                .Must(BeAValidImage).When(x => x.Image != null)
                .WithMessage("Şəkil faylı düzgün formatda olmalıdır (jpg, jpeg, png, gif və s.).");
        }

        private bool BeAValidImage(IFormFile file)
        {
            var allowedExtensions = new[]
            {
            ".jpg", ".jpeg", ".png",
            ".gif", ".bmp", ".webp",
            ".tiff", ".svg"
        };

            var fileName = file.FileName.ToLower();
            return allowedExtensions.Any(ext => fileName.EndsWith(ext));
        }
    }
}
