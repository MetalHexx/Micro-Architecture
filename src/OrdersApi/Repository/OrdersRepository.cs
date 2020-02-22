using CbInsights.OrdersApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.OrdersApi.Repository
{

    //TODO: Update repository to return a status object and return a not found if the object
    //wasn't there
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository()
        {
            _items = new List<Order> 
            {               
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(2019, 1, 1),
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 3,
                            Quantity = 1
                        },
                        new OrderItem
                        {
                            ProductId = 2,
                            Quantity = 2
                        }
                    }
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = new DateTime(2019, 2, 10),
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 3,
                            Quantity = 2
                        },
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 5
                        }
                    }
                },
                 new Order
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = new DateTime(2019, 3, 3),
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 2,
                            Quantity = 1
                        },
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 2
                        }
                    }
                }


            };
            _currentId = _items.Count + 1;
        }

        public RepoResult<Order> DeleteOrder(int orderId)
        {
            return base.DeleteItem(orderId);
        }

        public RepoResult<Order> GetOrderById(int orderId)
        {
            return base.GetItemById(orderId);
        }

        public RepoResult<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            var items = _items.Where(o => o.CustomerId == customerId);
            if (items == null)
            {
                return new RepoResult<IEnumerable<Order>>(items)
                {
                    Type = RepoResultType.NotFound
                };
            }
            return new RepoResult<IEnumerable<Order>>(items)
            {
                Type = RepoResultType.Success
            };            
        }

        public RepoResult<Order> InsertOrder(Order order)
        {
            return base.InsertItem(order);
        }

        public RepoResult<Order> UpdateOrder(Order order)
        {
            return base.UpdateItem(order);
        }
    }
}
