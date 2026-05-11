using Microsoft.EntityFrameworkCore;
using RPSLS.Fullstack.Api.Data;
using RPSLS.Fullstack.Api.Middleware;

namespace RPSLS.Fullstack.Api.DependencyInjection;

public static class ApiApplicationExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static WebApplication UseApiPipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("frontend");
        app.UseRateLimiter();

        var webRootPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
        if (File.Exists(Path.Combine(webRootPath, "index.html")))
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        app.MapControllers();
        if (File.Exists(Path.Combine(webRootPath, "index.html")))
        {
            app.MapFallbackToFile("index.html");
        }

        return app;
    }
}
