using CbInsights.CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Validations
{
    public class DeleteValidator : AbstractValidator<int>
    {
        public DeleteValidator()
        {
            RuleFor(x => x).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
