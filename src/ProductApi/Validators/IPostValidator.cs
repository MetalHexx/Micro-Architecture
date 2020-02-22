using CbInsights.ProductsApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.ProductsApi.Validators
{
    public interface IPostValidator: IValidator<Product>
    {
    }
}
