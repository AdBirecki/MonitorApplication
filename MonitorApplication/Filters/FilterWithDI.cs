using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Filters
{
    public class FilterWithDI : IActionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public FilterWithDI(ILoggerFactory loggerFactory) {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(nameof(FilterWithDI));
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogCritical("OnActionAsync start");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogCritical("OnActionAsync end");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogCritical("OnActionAsync start");
            await next();
            _logger.LogCritical("OnActionAsync end");
        }
    }
}
