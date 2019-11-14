using CbInsights.CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Vaildators
{
    public interface IPutValidator : IValidator<Customer>
    {
    }
}
