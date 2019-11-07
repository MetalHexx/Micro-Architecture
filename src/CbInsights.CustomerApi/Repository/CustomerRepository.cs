using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Repository
{
    //TODO: Update repository to return a status object and return a not found if the object
    //wasn't there
    public class CustomerRepository: ICustomerRespository
    {
        private List<Customer> _customers;
        private int _currentId = 2;

        public CustomerRepository()
        {
            _customers = new List<Customer>()
            {
                new Customer
                {
                    Id = 0,
                    FirstName = "William",
                    LastName = "Pereira"
                },                
                new Customer
                {
                    Id = 1,
                    FirstName = "Luke",
                    LastName = "Skywalker"
                }
            };
        }

        public void DeleteCustomer(int id)
        {
            var customer = _customers.SingleOrDefault(c => c.Id == id);

            if (customer != null)
            {
                _customers.Remove(customer);
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customers;
        }

        public Customer GetCustomer(int id)
        {
            return _customers.SingleOrDefault(c => c.Id == id);
        }

        public int InsertCustomer(Customer customer)
        {
            customer.Id = _currentId;
            _customers.Add(customer);
            _currentId++;
            return customer.Id.Value;
        }

        public void UpdateCustomer(Customer customer)
        {
            var repoCustomer = _customers.SingleOrDefault(c => c.Id == customer.Id);
            if(repoCustomer != null)
            {
                _customers.Remove(repoCustomer);
            }
            _customers.Add(customer);
        }
    }
}
