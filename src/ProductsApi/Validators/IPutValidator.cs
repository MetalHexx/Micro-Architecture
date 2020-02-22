using ProductsApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Validators
{
    public interface IPutValidator: IValidator<Product>
    {
    }
}
