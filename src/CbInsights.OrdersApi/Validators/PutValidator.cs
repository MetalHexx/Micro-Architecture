using CbInsights.OrdersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.OrdersApi.Validators
{
    public class PutValidator : AbstractValidator<Order>
    {
        public PutValidator()
        {
            RuleFor(o => o).SetValidator(new OrderValidator());

            When(o => o != null, () => 
            {
                RuleFor(o => o.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
            });            
        }
    }
}
