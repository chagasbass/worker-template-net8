namespace VesteTemplateWorker.Bases;

public class WorkerOptions : IWorkerOptions
{
    public int RepeatIntervalSeconds { get; set; } = 6000;
    public int RetryCounts { get; set; }
}
