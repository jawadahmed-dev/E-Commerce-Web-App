using Core.Models.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Validators.Product
{
    public class GetProductsModelValidator : AbstractValidator<GetProductsModel>
    {
        public GetProductsModelValidator()
        {
            RuleFor(model => model.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("Page index must be greater than or equal to 1.");

            RuleFor(model => model.PageSize)
                .InclusiveBetween(1, GetProductsModel.MaxPageSize)
                .WithMessage($"Page size must be between 1 and {GetProductsModel.MaxPageSize}.");

            RuleFor(model => model.Sort)
                .MaximumLength(255).WithMessage("Sort field length cannot exceed 255 characters.");

            RuleFor(model => model.BrandId)
                .GreaterThanOrEqualTo(1).When(model => model.BrandId.HasValue)
                .WithMessage("Brand ID must be greater than or equal to 1 if provided.");

            RuleFor(model => model.TypeId)
                .GreaterThanOrEqualTo(1).When(model => model.TypeId.HasValue)
                .WithMessage("Type ID must be greater than or equal to 1 if provided.");

            RuleFor(model => model.Search)
                .MaximumLength(255).When(model => !string.IsNullOrEmpty(model.Search))
                .WithMessage("Search query length cannot exceed 255 characters.");
        
                
        }
    }
}
