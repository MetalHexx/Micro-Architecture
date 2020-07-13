using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features.Models
{
    public class Feature
    {
        public bool Enabled { get; set; }
        public IEnumerable<string> RequiredRoles { get; set; }        
    }
}
