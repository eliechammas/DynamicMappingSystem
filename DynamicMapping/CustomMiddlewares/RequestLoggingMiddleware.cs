namespace DynamicMapping.CustomMiddlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext != null)
            {
                var startTime = DateTime.UtcNow;
                await _next(httpContext).ConfigureAwait(false);

                var endTime = DateTime.UtcNow;
                var elapsedTime = endTime - startTime;

                var logMessage = $"{httpContext.Request.Method} {httpContext.Request.Path} {httpContext.Response.StatusCode} {elapsedTime.TotalMilliseconds}ms";
                Console.WriteLine(logMessage);
            }
        }
    }

    //Extension method used to add the middleware to the HTTP request pipeline
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
