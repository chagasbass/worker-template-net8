namespace VesteTemplateWorker.Extensions.Healthchecks;

public static class WorkerHealthcheckExtension
{
    public static IServiceCollection AddWorkerHealthchecks(this IServiceCollection services, IConfiguration configuration)
    {
        var healthcheckOptions = configuration.GetSection(WorkerHealthchecksOptions.HealthConfig).Get<WorkerHealthchecksOptions>();

        services.AddSingleton<WorkerStateService>();

        if (!string.IsNullOrEmpty(healthcheckOptions.Hostname))
        {
            services.AddCustomTinyHealthCheck<WorkerHealthchecks>(config =>
            {
                config.Port = healthcheckOptions.Port;
                config.UrlPath = healthcheckOptions.UrlPath;
                config.Hostname = healthcheckOptions.Hostname;
                return config;
            });

            return services;
        }

        services.AddCustomTinyHealthCheck<WorkerHealthchecks>(config =>
        {
            config.Port = healthcheckOptions.Port;
            config.UrlPath = healthcheckOptions.UrlPath;
            return config;
        });

        return services;
    }
}