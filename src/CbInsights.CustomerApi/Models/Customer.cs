using CbInsights.CustomerApi.Validators;
using System;

namespace CbInsights.CustomersApi.Models
{
    public class Customer: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
