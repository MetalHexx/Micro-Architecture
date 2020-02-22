using CustomersApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CustomersApi.Tests
{
    public class CustomerListValidTestData : TheoryData<List<Customer>>
    {
        public CustomerListValidTestData()
        {
            Add(new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "William",
                    LastName = "Pereira"
                }
            });
        }
    }

    public class CustomerValidTestData : TheoryData<Customer>
    {
        public CustomerValidTestData()
        {
            Add(new Customer
            {
                Id = 1,
                FirstName = "William",
                LastName = "Pereira"
            });
        }
    }

    public class CustomerInvalidTestData : TheoryData<Customer>
    {
        public CustomerInvalidTestData()
        {
            Add(new Customer());

            Add(new Customer
            {
                Id = 1,
                FirstName = "",
                LastName = "Pereira"
            });

            Add(new Customer
            {
                Id = 1,
                LastName = "Pereira"
            });

            Add(new Customer
            {
                Id = 1,
                FirstName = "William",
                LastName = ""
            });

            Add(new Customer
            {
                Id = 1,
                FirstName = "William",
            });

            Add(new Customer
            {
                Id = 1,
                FirstName = "",
                LastName = ""
            });

            Add(new Customer
            {
                Id = 1
            });
        }
    }

    public class CustomerNonExistentTestData : TheoryData<Customer>
    {
        public CustomerNonExistentTestData()
        {
            Add(new Customer
            {
                Id = 0,
                FirstName = "William",
                LastName = "Pereira"
            });
        }
    }

    public class CustomerExistingTestData : TheoryData<Customer>
    {
        public CustomerExistingTestData()
        {
            Add(new Customer
            {
                Id = 1,
                FirstName = "William",
                LastName = "Pereira"
            });
        }
    }
}
