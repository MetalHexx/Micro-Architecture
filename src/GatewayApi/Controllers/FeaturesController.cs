using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayApi.Features;
using GatewayApi.Features.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IAppFeaturesService _featuresService;

        public FeaturesController(IAppFeaturesService featuresService)
        {
            _featuresService = featuresService;
        }
        [HttpGet("features")]
        public async Task<ActionResult<AppFeatures>> GetFeatures()
        {
            return Ok(await _featuresService.Get());
        }
    }
}