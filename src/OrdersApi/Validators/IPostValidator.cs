﻿using OrdersApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApi.Validators
{
    public interface IPostValidator: IValidator<Order>
    {
    }
}
