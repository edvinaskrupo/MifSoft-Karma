using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Care.Helpers
{
    [LogCall]
    public class StatisticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public StatisticsMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var controllerActionDescriptor =
                context
                .GetEndpoint()
                .Metadata
                .GetMetadata<ControllerActionDescriptor>();

            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.ActionName;

            await _next(context);

            sw.Stop();

            Debug.WriteLine($"It took {sw.ElapsedMilliseconds} ms to perform" +
                $" {controllerName}/{actionName}");

            _logger.Information($"It took {sw.ElapsedMilliseconds} ms to perform" +
                $" {controllerName}/{actionName}");
        }
    }
}
