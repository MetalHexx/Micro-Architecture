using GatewayApi.Features.Candidate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features
{
    public class CrmFeatures
    {
        [JsonIgnore]
        public const string SectionName = "CrmFeatures";
        public Feature CandidatesModule { get; set; }
        public CandidatesSearchView CandidatesSearchView { get; set; }
    }
}
