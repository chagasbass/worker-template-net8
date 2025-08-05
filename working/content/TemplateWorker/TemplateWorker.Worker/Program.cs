Log.Logger = LogIntegrationsExtensions.ConfigureLog();

try
{
    Log.Information($"Iniciando o Worker");

    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        #region configurações do Worker
        var env = hostContext.HostingEnvironment;

        var config = GetConfiguration(args, env);

        services.AddOptionsPattern(config)
                .AddApplicationInsightsTelemetry(config)
                .AddWorkerHealthchecks(config)
                .AddFilterToSystemLogs()
                .AddAvailabilityService()
                .AddNotificationControl()
                .AddDependencyInjectionExtensions()
                .AddWorkerDI();

        services.Configure<HostOptions>(config.GetSection("HostOptions"));

        #endregion

        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();

    return 0;

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminado inexperadamente.");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

static IConfiguration GetConfiguration(string[] args, IHostEnvironment environment)
{
    return new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
       .AddEnvironmentVariables()
       .AddCommandLine(args)
       .Build();
}
