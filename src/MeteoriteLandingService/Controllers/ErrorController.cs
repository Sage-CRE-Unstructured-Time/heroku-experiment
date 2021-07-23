namespace Sage.MeteoriteLandingService
{
    using System;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Sage.MeteoriteLandingService.ControllerModels;

    [ApiController]
    [Route("error")]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public Error Error()
        {
            _logger.LogInformation("Encountered uncaught exception");
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context != null)
            {
                var exception = context.Error;
                _logger.LogError(exception, "Uncaught exception");
            }

            Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new Error { 
                Message = "Failed to process your request" 
            };
            _logger.LogInformation("Exiting generic error handling");
            return response;
        }
    }
}
