using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayApi.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly AppFeatures _features;

        public FeaturesController(IOptions<AppFeatures> features)
        {
            _features = features.Value;
        }
        [HttpGet("features")]
        public async Task<ActionResult<AppFeatures>> GetFeatures()
        {
            return Ok(_features);
        }
    }
}