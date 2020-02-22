using CustomersApi.Validators;
using System;

namespace CustomersApi.Models
{
    public class Customer: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
