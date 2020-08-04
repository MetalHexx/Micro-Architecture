using CustomersApi.Domain;
using CustomersApi.Vaildators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Validators
{
    public class PutValidator : AbstractValidator<Customer>, IPutValidator
    {
        public PutValidator()
        {
            RuleFor(x => x).SetValidator(new CustomerValidator());
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}

