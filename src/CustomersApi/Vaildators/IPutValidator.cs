﻿using CustomersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Vaildators
{
    public interface IPutValidator : IValidator<Customer>
    {
    }
}
