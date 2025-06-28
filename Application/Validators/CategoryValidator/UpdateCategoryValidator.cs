using FluentValidation;
using Onion.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Validators.CategoryValidator
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDTOs>
    {

        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category adı boş ola bilmez.")
                .MaximumLength(40).WithMessage("Category adı en cox 40 simvol olabiler.");
        }
    }
}
