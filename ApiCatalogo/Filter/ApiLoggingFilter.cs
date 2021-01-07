using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Filter
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
             _logger.LogInformation("############## Excutando  -> OnActionExecuting ##############################");
            _logger.LogInformation("##############################################################################");
            _logger.LogInformation(DateTime.Now.ToLongDateString());
            _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
            _logger.LogInformation("###############################################################################");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("############## Excutando  -> OnActionExecuting ##############################");
            _logger.LogInformation("##############################################################################");
            _logger.LogInformation(DateTime.Now.ToLongDateString());
            _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
            _logger.LogInformation("###############################################################################"); 
        }
    }
}
