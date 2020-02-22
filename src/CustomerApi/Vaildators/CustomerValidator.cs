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
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Customer first name cannot be null or empty");         
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be null or empty");
        }
    }
}