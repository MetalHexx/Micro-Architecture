using OrdersApi.Models;
using System.Collections.Generic;

namespace OrdersApi.Repository
{
    public interface IOrdersRepository
    {
        RepoResult<Order> GetOrderById(int orderId);
        RepoResult<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
        RepoResult<Order> InsertOrder(Order order);
        RepoResult<Order> DeleteOrder(int orderId);
        RepoResult<Order> UpdateOrder(Order order);
    }
}