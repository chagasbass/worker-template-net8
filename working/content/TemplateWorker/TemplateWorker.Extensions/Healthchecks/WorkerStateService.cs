namespace VesteTemplateWorker.Extensions.Healthchecks;

public record WorkerStateService
{
    public bool IsRunning { get; set; }
    public DateTime Timestamp { get; set; }

    public WorkerStateService() { }
}