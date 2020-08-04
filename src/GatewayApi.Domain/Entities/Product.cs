using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayApi.Domain.Entities
{
    public partial class Product
    {   
        public int? Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
