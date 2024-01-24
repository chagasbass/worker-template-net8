namespace VesteTemplateWorker.Shared.Configurations;

public record WorkerHealthchecksOptions
{
    public const string HealthConfig = "WorkerHealthchecks";

    public int Port { get; set; }
    public string? UrlPath { get; set; }
    public string? Hostname { get; set; }

    public WorkerHealthchecksOptions() { }

}