using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Galaxy.Planets.Presentation.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    logger.LogCritical($"General error: {context.Features.Get<IExceptionHandlerFeature>().Error}");
                    
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    await Task.CompletedTask;
                });
            });
        }

    }
}