using CustomersApi.Models;
using System.Collections.Generic;

namespace CustomersApi.Repository
{
    //TODO: Update repository to return a status object and return a not found if the object
    //wasn't there
    public class CustomersRepository: RepositoryBase<Customer>, ICustomersRespository
    {
        public CustomersRepository()
        {
            _items = new List<Customer>()
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "William",
                    LastName = "Pereira"
                },                
                new Customer
                {
                    Id = 2,
                    FirstName = "Luke",
                    LastName = "Skywalker"
                }
            };
            _currentId = _items.Count + 1;
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

        public RepoResult<Customer> DeleteCustomer(int id)
        {
            return base.DeleteItem(id);
        }
    }
}
