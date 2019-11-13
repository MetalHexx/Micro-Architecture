using CbInsights.OrdersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.OrdersApi.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o).NotNull().WithMessage("Order cannot be null");
            When(o => o != null, () => 
            {
                RuleFor(o => o.CustomerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
                RuleFor(o => o.Items).NotNull().WithMessage("Items must not be null");

                When(o => o.Items != null, () => 
                {
                    RuleForEach(o => o.Items).SetValidator(new OrderItemValidator());
                    RuleFor(o => o.Items.Count).GreaterThan(0).WithMessage("You must have at least one item in an order");
                });               
            });            
        }
    }
}
