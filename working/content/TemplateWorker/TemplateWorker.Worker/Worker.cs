namespace VesteTemplateWorker.Worker;

public class Worker(IOptions<WorkerConfigurationOptions> workerConfigOptions,
                    ILogServices logService,
                    IAvailabilityServices availabilityServices,
                    WorkerStateService workerStateService) : WorkerBase(logService, workerConfigOptions, workerStateService, availabilityServices)
{
    public override async Task DoWorkAsync()
    {
        Console.WriteLine($"executando..{DateTime.UtcNow}");
        await Task.CompletedTask;
    }
}