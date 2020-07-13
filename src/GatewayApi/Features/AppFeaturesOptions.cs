using GatewayApi.Features.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features
{
    public class AppFeaturesOptions: AppFeatures
    {
        [JsonIgnore]
        public const string SectionName = "MicroArchitectureFeatures";
    }
}
