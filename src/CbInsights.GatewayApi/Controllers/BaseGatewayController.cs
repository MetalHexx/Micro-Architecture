using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CbInsights.GatewayApi.Controllers
{
    [ApiController]
    public class BaseGatewayController : ControllerBase
    {
        protected ActionResult GetResult<T>(ApiResult<T> result)
        {
            switch (result.StatusCode)
            {
                default:
                    return Ok(result.Content);

                case (StatusCodes.Status404NotFound):
                    return NotFound();

                case (StatusCodes.Status400BadRequest):
                    return BadRequest(result.Content);
            }
        }
    }
}