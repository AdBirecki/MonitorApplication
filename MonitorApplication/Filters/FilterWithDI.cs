﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Filters
{
    /* This class is meant to test how DI behaves in .NET core. It has no other role right now. 
     * I was simply to check if a Filter used as Attribute can be "constructor" injected.
     * It seems it can using [ServiceFilter(typeof(FilterWithDI))] */
    public class FilterWithDI : IActionFilter
    {
        private readonly ILogger _logger;

        public FilterWithDI(ILoggerFactory loggerFactory) {
            _logger = loggerFactory.CreateLogger(nameof(FilterWithDI));
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted ");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("OnActionExecuting");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("OnActionAsync start");
            await next();
            _logger.LogInformation("OnActionAsync end");
        }
    }
}
