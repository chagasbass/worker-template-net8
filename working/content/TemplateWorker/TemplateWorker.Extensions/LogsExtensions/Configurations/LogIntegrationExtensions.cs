namespace VesteTemplateWorker.Extensions.LogsExtensions.Configurations;

public static class LogIntegrationsExtensions
{
    public static Logger ConfigureLog()
    {
        var telemetryConfiguration = new TelemetryConfiguration
        {
            ConnectionString = "InstrumentationKey=cc879a98-c2fb-4b8d-94d1-54b3bacadbda;IngestionEndpoint=https://brazilsouth-1.in.applicationinsights.azure.com/;LiveEndpoint=https://brazilsouth.livediagnostics.monitor.azure.com/"
        };

        return new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                   .MinimumLevel.Override("System", LogEventLevel.Error)
                   .MinimumLevel.Override("TinyHealthCheck", LogEventLevel.Error)
                   .Enrich.FromLogContext()
                   .WriteTo.Console()
                   .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces)
                   .CreateLogger();

    }


    public static IServiceCollection AddFilterToSystemLogs(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {

            builder.AddFilter("Microsoft", LogLevel.Warning)
                   .AddFilter("System", LogLevel.Warning)
                   .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning)
                   .AddFilter("Microsoft.AspNetCore", LogLevel.Warning)
                   .AddFilter("Microsoft.AspNetCore.Hosting.Diagnostics", LogLevel.Warning)
                   .AddFilter("TinyHealthCheck", LogLevel.Warning)
                   .AddConsole();
        });

        return services;
    }

    public static IServiceCollection SolveLogServiceDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ILogServices, LogServices>();
        services.AddSingleton<LogData>();

        return services;
    }
}
