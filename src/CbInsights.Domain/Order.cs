using System;
using System.Collections.Generic;
using System.Text;

namespace CbInsights.Domain
{
    public class Order
    {
        public int? OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
