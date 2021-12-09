using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Care.Helpers
{
    public class StatisticsMiddleware
    {
        private readonly RequestDelegate _next;

        public StatisticsMiddleware(RequestDelegate next)
        {
            _next = next;
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
            var actionName = controllerActionDescriptor.ActionName;*/

            await _next(context);

            sw.Stop();

            Debug.WriteLine($"It took {sw.ElapsedMilliseconds} ms to perform " +
                $"this action {actionName} in this controller {controllerName}");
        }
    }
}
