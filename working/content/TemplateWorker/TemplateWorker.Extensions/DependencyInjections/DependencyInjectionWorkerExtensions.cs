
namespace VesteTemplateWorker.Worker.Extensions;

public static class DependencyInjectionWorkerExtensions
{
    public static IServiceCollection AddDependencyInjectionExtensions(this IServiceCollection services)
    {
        services.SolveLogServiceDependencies();

        return services;
    }
}
