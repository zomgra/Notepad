using Microsoft.Extensions.DependencyInjection;

namespace Notepad.API.Middleware
{
    public static class ApplicationExceprionHandlerMiddlewareExtentions
    {
        public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ApplicationExceprionHandlerMiddleware>();
            return builder;
        }
    }
}
