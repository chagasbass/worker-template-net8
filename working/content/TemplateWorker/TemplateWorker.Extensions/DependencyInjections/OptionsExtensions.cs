namespace VesteTemplateWorker.Extensions.DependencyInjections;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BaseConfigurationOptions>(configuration.GetSection(BaseConfigurationOptions.BaseConfig));
        services.Configure<ResilienceConfigurationOptions>(configuration.GetSection(ResilienceConfigurationOptions.ResilienciaConfig));
        services.Configure<WorkerConfigurationOptions>(configuration.GetSection(WorkerConfigurationOptions.WorkerConfig));
        services.Configure<WorkerHealthchecksOptions>(configuration.GetSection(WorkerHealthchecksOptions.HealthConfig));

        return services;
    }
}
