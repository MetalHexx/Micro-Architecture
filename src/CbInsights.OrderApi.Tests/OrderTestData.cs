using CbInsights.OrdersApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CbInsights.OrderApi.Tests
{
    public class OrderListValidTestData : TheoryData<List<Order>>
    {
        public OrderListValidTestData()
        {
            Add(new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 1
                        }
                    }
                }                
            });
        }
    }

    public class OrderValidTestData : TheoryData<Order>
    {
        public OrderValidTestData()
        {
            Add(new Order()
            {
                Id = 1,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            });
        }
    }

    public class OrderInvalidTestData : TheoryData<Order>
    {
        public OrderInvalidTestData()
        {
            Add(new Order());

            Add(new Order()
            {
                Id = 1,
                CustomerId = 1
            });

            Add(new Order()
            {
                Id = 1,
                CustomerId = 0,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            });

            Add(new Order()
            {
                Id = 1,
                CustomerId = 1
            });

            Add(new Order()
            {
                Id = 1,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 0,
                        Quantity = 1
                    }
                }
            });

            Add(new Order()
            {
                Id = 1,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 0
                    }
                }
            });
        }
    }

    public class OrderNonExistentTestData : TheoryData<Order>
    {
        public OrderNonExistentTestData()
        {
            Add(new Order()
            {
                Id = 0,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            });
        }
    }

    public class OrderExistingTestData : TheoryData<Order>
    {
        public OrderExistingTestData()
        {
            Add(new Order()
            {
                Id = 1,
                CustomerId = 1,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            });
        }
    }
}
