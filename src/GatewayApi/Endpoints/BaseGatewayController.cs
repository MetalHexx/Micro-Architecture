using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayApi.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayApi.Controllers
{
    [ApiController]
    public class BaseGatewayController : ControllerBase
    {
        protected ActionResult GenerateErrorResult(Exception exception)
        {
            if(exception is ApiException)
            {
                var apiException = exception as ApiException;

                switch (apiException.StatusCode)
                {
                    default:
                        return new StatusCodeResult(StatusCodes.Status500InternalServerError);

                    case (StatusCodes.Status404NotFound):
                        return NotFound();

                    case (StatusCodes.Status400BadRequest):
                        return BadRequest(apiException.Response);
                }
            }
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}