using CbInsights.CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Validators
{
    public class PutValidator : AbstractValidator<Customer>
    {
        public PutValidator()
        {
            RuleFor(x => x).SetValidator(new CustomerValidator());
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}

