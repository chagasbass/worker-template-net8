namespace VesteTemplateWorker.Extensions.Telemetry;

public interface IAvailabilityServices
{
    void TrackErrorEvent(Exception exception);
    IOperationHolder<RequestTelemetry> StartOperation();
    Task FlushAsync(CancellationToken cancellationToken);
}