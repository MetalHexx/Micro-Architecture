using CustomersApi.Models;
using System.Collections.Generic;

namespace CustomersApi.Repository
{
    public interface ICustomersRespository
    {
        RepoResult<IEnumerable<Customer>> GetCustomers();
        RepoResult<Customer> GetCustomer(int id);
        RepoResult<Customer> InsertCustomer(Customer customer);
        RepoResult<Customer> UpdateCustomer(Customer customer);
        RepoResult<Customer> DeleteCustomer(int id);
    }
}
