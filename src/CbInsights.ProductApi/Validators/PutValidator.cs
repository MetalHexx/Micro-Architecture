using CbInsights.ProductsApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.ProductsApi.Validators
{
    public class PutValidator: AbstractValidator<Product>, IPutValidator
    {
        public PutValidator()
        {
            RuleFor(p => p).SetValidator(new ProductValidator());
            When(p => p != null, () => 
            {
                RuleFor(p => p.Id).GreaterThan(0).WithMessage("Product Id must be greater than 0");
            });
        }
    }
}
