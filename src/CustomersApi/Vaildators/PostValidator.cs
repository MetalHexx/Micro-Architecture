using CustomersApi.Vaildators;
using CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Validators
{
    public class PostValidator : AbstractValidator<Customer>, IPostValidator
    {
        public PostValidator()
        {
            RuleFor(x => x).SetValidator(new CustomerValidator());
            RuleFor(x => x.Id).Equal(0).WithMessage("ID must be 0");
        }
    }
}
