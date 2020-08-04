using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayApi.Domain.Entities
{
    public partial class Customer
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
