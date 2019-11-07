using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.Domain;

namespace CbInsights.OrdersApi.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private List<Order> _orders;
        private int _currentId = 2;

        public OrdersRepository()
        {
            _orders = new List<Order> 
            {
                new Order
                {
                    OrderId = 0,
                    CustomerId = 0,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 0,
                            Quantity = 1
                        },
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 2
                        }
                    }
                },
                new Order
                {
                    OrderId = 1,
                    CustomerId = 0,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 3,
                            Quantity = 1
                        },
                        new OrderItem
                        {
                            ProductId = 4,
                            Quantity = 2
                        }
                    }
                },
                new Order
                {
                    OrderId = 2,
                    CustomerId = 1,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 0,
                            Quantity = 2
                        },
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 5
                        }
                    }
                },

            };
        }

        public void DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.SingleOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return _orders.Where(o => o.CustomerId == customerId);
        }

        public int InsertOrder(Order order)
        {
            order.OrderId = _currentId;
            _orders.Add(order);
            _currentId++;
            return order.OrderId;
        }

        public void UpdateOrder(Order order)
        {
            var repoOrder = _orders.SingleOrDefault(o => o.OrderId == order.OrderId);

            if(repoOrder != null)
            {
                _orders.Remove(repoOrder);
            }
            _orders.Add(order);
        }
    }
}
