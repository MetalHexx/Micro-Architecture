using ProductsApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Validators
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p).NotEmpty().WithMessage("Product cannot be null");
            When(p => p != null, () => 
            {
                RuleFor(p => p.Name).NotEmpty().WithMessage("Product name cannot be null or empty");
            });
        }
    }
}
