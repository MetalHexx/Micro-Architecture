using System;
using System.Collections.Generic;
using System.Text;

namespace CbInsights.Domain
{
    public class Product: IEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
