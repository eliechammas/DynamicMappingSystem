using Microsoft.Extensions.Caching.Memory;

namespace DynamicMapping.CustomMiddlewares
{
    public class CachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;

        public CachingMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(httpContext != null)
            {
                var cacheKey = httpContext.Request.Path;

                if(_memoryCache.TryGetValue(cacheKey, out byte[] cachedData))
                {
                    httpContext.Response.Body = new MemoryStream(cachedData);
                    return;
                }

                var originalBodyStream = httpContext.Response.Body;
                using (var memoryStream = new MemoryStream())
                {
                    httpContext.Response.Body = memoryStream;

                    await _next(httpContext).ConfigureAwait(false);

                    var responseData = memoryStream.ToArray();
                    _memoryCache.Set(cacheKey, responseData, TimeSpan.FromMinutes(5));
                    memoryStream.Position = 0;
                    await memoryStream.CopyToAsync(originalBodyStream).ConfigureAwait(false);
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCachingMiddleware(IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CachingMiddleware>();
        }
    }
}
