namespace VesteTemplateWorker.Extensions.Healthchecks;

public class WorkerHealthchecks(ILogServices logServices,
                              IOptions<BaseConfigurationOptions> options,
                              WorkerStateService workerStateService) : IHealthCheck
{

    public async Task<IHealthCheckResult> ExecuteAsync(CancellationToken cancellationToken)
    {
        if (workerStateService.IsRunning)
        {
            return new JsonHealthCheckResult(
               new
               {
                   Status = "Healthy!",
                   Timestamp = DateTime.UtcNow,
                   IsServiceRunning = workerStateService.IsRunning,
                   Application = options.Value.NomeProjeto
               },
               HttpStatusCode.OK);
        }

        logServices.WriteStaticMessage($"Erro na aplicação {options.Value.NomeProjeto}");
        logServices.WriteStaticMessage($"Status: Inativo");

        return new JsonHealthCheckResult(
            new
            {
                Status = "Unhealthy!",
                Timestamp = DateTime.UtcNow,
                IsServiceRunning = workerStateService.IsRunning,
                Application = options.Value.NomeProjeto,
                ErrorMessage = $"Erro na aplicação {options.Value.NomeProjeto}"
            },
            HttpStatusCode.InternalServerError);
    }
}