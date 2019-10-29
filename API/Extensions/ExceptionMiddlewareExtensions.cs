using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace API.Extensions
{
    public class CustomLoggingExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
 
        public CustomLoggingExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("CustomLoggingExceptionFilter");
        }
 
        public override void OnException(ExceptionContext context)
        {
            _logger.LogInformation(context.Exception.Message);
        }
    }
}