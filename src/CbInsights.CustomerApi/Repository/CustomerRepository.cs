using CbInsights.Core;
using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Repository
{
    //TODO: Update repository to return a status object and return a not found if the object
    //wasn't there
    public class CustomerRepository: RepositoryBase<Customer>, ICustomerRespository
    {
        public CustomerRepository()
        {
            _items = new List<Customer>()
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
            _currentId = _items.Count;
        }

        public RepoResult<Customer> DeleteCustomer(int id)
        {
            return base.DeleteItem(id);
        }

        public RepoResult<IEnumerable<Customer>> GetCustomers()
        {
            return new RepoResult<IEnumerable<Customer>>(_items)
            {
                Type = RepoResultType.Success
            };
        }

        public RepoResult<Customer> GetCustomer(int id)
        {
            return base.GetItemById(id);
        }

        public RepoResult<Customer> InsertCustomer(Customer customer)
        {
            return base.InsertItem(customer);
        }

        public RepoResult<Customer> UpdateCustomer(Customer customer)
        {
            return base.UpdateItem(customer);
        }
    }
}
