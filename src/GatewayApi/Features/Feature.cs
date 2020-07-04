using GatewayApi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features
{
    public class Feature
    {
        public bool Enabled { get; set; }
        public IEnumerable<int> EnabledTenants { get; set; }
        public IEnumerable<string> RequiredRoles { get; set; }
        
    }
}
