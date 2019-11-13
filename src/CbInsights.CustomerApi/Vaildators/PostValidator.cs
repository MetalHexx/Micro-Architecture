using CbInsights.CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Validators
{
    public class PostValidator : AbstractValidator<Customer>
    {
        public PostValidator()
        {
            RuleFor(x => x).SetValidator(new CustomerValidator());
            RuleFor(x => x.Id).Equal(0).WithMessage("ID must be 0");
        }
    }
}
