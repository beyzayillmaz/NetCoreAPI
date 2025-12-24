using System.Diagnostics;

namespace NetCoreAPI.Middlewares
{
    public class EndPointMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<EndPointMiddleWare> _logger;
        public EndPointMiddleWare(RequestDelegate next,ILogger<EndPointMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 5000)
            {
                _logger.LogWarning($"Bu İstek:  {context.Request.Method} {context.Request.Path} {stopwatch.ElapsedMilliseconds} ms sürdü.");
            }
        }

    }
}
