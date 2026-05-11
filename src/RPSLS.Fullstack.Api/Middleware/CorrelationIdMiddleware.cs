namespace RPSLS.Fullstack.Api.Middleware;

public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    public const string HeaderName = "X-Correlation-Id";
    public const string ItemName = "CorrelationId";

    public async Task InvokeAsync(HttpContext context, ILogger<CorrelationIdMiddleware> logger)
    {
        var correlationId = context.Request.Headers.TryGetValue(HeaderName, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue)
            ? headerValue.ToString()
            : Guid.NewGuid().ToString("N");

        context.Items[ItemName] = correlationId;
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[HeaderName] = correlationId;
            return Task.CompletedTask;
        });

        using (logger.BeginScope(new Dictionary<string, object> { [HeaderName] = correlationId }))
        {
            await next(context);
        }
    }
}
