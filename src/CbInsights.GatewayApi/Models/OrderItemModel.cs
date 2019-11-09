using CbInsights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CbInsights.GatewayApi.Models
{
    public class OrderItemModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

        public OrderItemModel(Product product, OrderItem orderItem)
        {
            Quantity = orderItem.Quantity;
            Product = new ProductModel(product);
        }
    }
}
