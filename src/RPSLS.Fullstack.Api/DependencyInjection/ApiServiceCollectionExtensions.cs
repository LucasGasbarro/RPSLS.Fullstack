using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;

namespace RPSLS.Fullstack.Api.DependencyInjection;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RPSLS Fullstack API",
                Version = "v1",
                Description = "Rock Paper Scissors Lizard Spock API endpoints."
            });

            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            if (File.Exists(apiXmlPath))
            {
                options.IncludeXmlComments(apiXmlPath);
            }

            var domainAssemblyName = typeof(Models.Choice).Assembly.GetName().Name;
            var domainXmlPath = Path.Combine(AppContext.BaseDirectory, $"{domainAssemblyName}.xml");
            if (File.Exists(domainXmlPath))
            {
                options.IncludeXmlComments(domainXmlPath);
            }
        });

        services.AddCors(options =>
        {
            options.AddPolicy("frontend", policy =>
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                var key = context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";
                return RateLimitPartition.GetFixedWindowLimiter(
                    key,
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 120,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    });
            });
        });

        return services;
    }
}
