namespace WebScraperApi.Helpers;

public static class Seeding
{
    public static void SeedingDB(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider;
        try
        {
            var context = service.GetRequiredService<ScrapDBContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"something went wrong while seeding database {ex.Message}");
        }
    }
}
