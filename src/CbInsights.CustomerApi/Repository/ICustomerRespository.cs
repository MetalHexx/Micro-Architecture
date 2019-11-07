using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.CustomerApi.Repository
{
    public interface ICustomerRespository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        int InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
