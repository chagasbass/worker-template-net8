namespace VesteTemplateWorker.Extensions.Telemetry;

public static class TelemetryExtensions
{
    public static IServiceCollection AddApplicationInsightsTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ApplicationInsights");

        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddApplicationInsightsTelemetryWorkerService(new ApplicationInsightsServiceOptions
            {
                ConnectionString = connectionString
            });
        }

        return services;
    }

    public static IServiceCollection AddAvailabilityService(this IServiceCollection services)
    {
        services.AddTransient<IAvailabilityServices, AvailabilityServices>();
        return services;
    }
}
