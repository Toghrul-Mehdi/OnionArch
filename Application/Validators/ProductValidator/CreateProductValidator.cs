using FluentValidation;
using Onion.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Validators.ProductValidator
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http;

    public class CreateProductDTOsValidator : AbstractValidator<CreateProductDTOs>
    {
        public CreateProductDTOsValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Məhsulun adı boş ola bilməz.")
                .MaximumLength(100).WithMessage("Məhsulun adı maksimum 100 simvol ola bilər.");

            RuleFor(x => x.ProductDescription)
                .MaximumLength(500).WithMessage("Məhsul təsviri maksimum 500 simvol ola bilər.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Zəhmət olmasa, düzgün kateqoriya seçin.");

            RuleFor(x => x.Image)
                .NotNull().WithMessage("Zəhmət olmasa, şəkil faylı yükləyin.")
                .Must(BeAValidImage).WithMessage("Şəkil faylı düzgün formatda olmalıdır (jpg, jpeg, png).");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

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
