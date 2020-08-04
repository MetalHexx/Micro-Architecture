using CustomersApi.Domain;
using System.Collections.Generic;

namespace CustomersApi.Infrastructure.Persistance
{
    public interface ICustomersRepository
    {
        RepoResult<IEnumerable<Customer>> GetCustomers();
        RepoResult<Customer> GetCustomer(int id);
        RepoResult<Customer> InsertCustomer(Customer customer);
        RepoResult<Customer> UpdateCustomer(Customer customer);
        RepoResult<Customer> DeleteCustomer(int id);
    }
}
