using OrdersApi.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApi.Validators
{
    public interface IPutValidator : IValidator<Order>
    {
    }
}
