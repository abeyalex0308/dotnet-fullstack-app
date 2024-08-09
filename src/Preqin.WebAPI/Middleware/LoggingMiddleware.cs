using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Preqin.WebAPI.Middleware
{

    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Log the incoming request
                _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogCritical(ex, $"An exception occurred: {ex.Message}");
                throw;
            }
        }
    }
}
