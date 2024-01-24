namespace VesteTemplateWorker.Extensions.Telemetry;

public class AvailabilityServices : IAvailabilityServices
{
    private readonly TelemetryClient _telemetryClient;
    private readonly BaseConfigurationOptions _options;

    public AvailabilityServices(TelemetryClient telemetryClient, IOptionsMonitor<BaseConfigurationOptions> options)
    {
        _telemetryClient = telemetryClient;
        _options = options.CurrentValue;
    }

    public async Task FlushAsync(CancellationToken cancellationToken) => await _telemetryClient.FlushAsync(cancellationToken);

    public IOperationHolder<RequestTelemetry> StartOperation() => _telemetryClient.StartOperation<RequestTelemetry>(_options.NomeProjeto);

    public void TrackErrorEvent(Exception exception)
    {
        _telemetryClient.TrackEvent(new EventTelemetry($"Erro na operação do {_options.NomeProjeto}"));
        _telemetryClient.TrackException(exception);
    }
}