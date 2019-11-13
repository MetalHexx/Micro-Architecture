using CbInsights.CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x).NotNull().WithMessage("Customer cannot be null");
            RuleFor(x => x.FirstName).MinimumLength(1).WithMessage("First name must be greater than 1");
            RuleFor(x => x.LastName).MinimumLength(1).WithMessage("Last name must be greater than 1");
        }
    }
}