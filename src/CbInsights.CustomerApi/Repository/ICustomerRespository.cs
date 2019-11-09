using CbInsights.Core;
using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Repository
{
    public interface ICustomerRespository
    {
        RepoResult<IEnumerable<Customer>> GetCustomers();
        RepoResult<Customer> GetCustomer(int id);
        RepoResult<Customer> InsertCustomer(Customer customer);
        RepoResult<Customer> UpdateCustomer(Customer customer);
        RepoResult<Customer> DeleteCustomer(int id);
    }
}
