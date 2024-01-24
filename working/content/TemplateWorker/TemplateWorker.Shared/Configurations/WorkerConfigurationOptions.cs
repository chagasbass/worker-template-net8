namespace VesteTemplateWorker.Shared.Configurations;

public record WorkerConfigurationOptions
{
    public const string WorkerConfig = "WorkerConfiguration";
    public string? HorarioDeExecucao { get; set; }
    public int Runtime { get; set; }

    public WorkerConfigurationOptions() { }
}
