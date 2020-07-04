using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features
{
    public class AppFeatures
    {
        [JsonIgnore]
        public const string SectionName = "MicroArchitectureFeatures";
        public Feature ViewCustomers { get; set; }
        public Feature ViewOrderDetails { get; set; }
    }
}
