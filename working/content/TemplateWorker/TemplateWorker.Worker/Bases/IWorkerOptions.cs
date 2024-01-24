namespace VesteTemplateWorker.Bases;

public interface IWorkerOptions
{
    /// <summary>
    /// Defines the period between the end of one unit of work and the start of the next.
    /// </summary>
    int RepeatIntervalSeconds { get; set; }
    int RetryCounts { get; set; }
}
