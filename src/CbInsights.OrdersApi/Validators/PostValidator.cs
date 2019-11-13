using CbInsights.OrdersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.OrdersApi.Validators
{
    public class PostValidator : AbstractValidator<Order>, IPostValidator
    {
        public PostValidator()
        {
            RuleFor(o => o).SetValidator(new OrderValidator());
            When(o => o != null, () =>
            {
                RuleFor(o => o.Id).Equal(0).WithMessage("ID must be 0");
            });
        }
    }
}
