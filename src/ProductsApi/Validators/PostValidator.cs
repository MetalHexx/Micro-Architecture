using ProductsApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Validators
{
    public class PostValidator : AbstractValidator<Product>, IPostValidator
    {
        public PostValidator()
        {
            RuleFor(p => p).SetValidator(new ProductValidator());
            When(p => p != null, () =>
            {
                RuleFor(p => p.Id).Equal(0).WithMessage("New product Id must be zero");
            });
        }
    }
}
