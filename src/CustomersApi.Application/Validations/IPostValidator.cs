﻿using CustomersApi.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Application.Validations
{
    public interface IPostValidator : IValidator<Customer>
    {
    }
}
