using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public CustomerModel(Customer customer)
        {
            Id = customer.Id.Value;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }

        public Customer GetDomainModel()
        {
            return new Customer
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }
}
