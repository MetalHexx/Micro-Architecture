using CbInsights.OrdersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.OrdersApi.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(o => o).NotEmpty().WithMessage("Order item cannot be null");
            When(o => o != null, () => 
            {
                RuleFor(o => o.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0");
                RuleFor(o => o.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            });            
        }
    }
}
