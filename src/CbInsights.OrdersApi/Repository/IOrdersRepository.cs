using CbInsights.Domain;
using System.Collections.Generic;

namespace CbInsights.OrdersApi.Repository
{
    public interface IOrdersRepository
    {
        Order GetOrderById(int orderId);
        IEnumerable<Order> GetOrdersByCustomerId(int customerId);
        int InsertOrder(Order order);
        void DeleteOrder(int orderId);
        void UpdateOrder(Order order);
    }
}