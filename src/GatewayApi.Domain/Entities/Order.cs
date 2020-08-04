using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayApi.Domain.Entities
{
    public partial class Order
    {
        public int? Id { get; set; }        
        public int? CustomerId { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }

    public partial class OrderItem
    {        
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
