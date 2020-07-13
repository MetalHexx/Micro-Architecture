using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features.Models
{
    public class AppFeaturesDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string DatabaseName { get; set; }
        public int ConnectionTimeoutMs { get; set; }
    }
}
