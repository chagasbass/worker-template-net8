namespace VesteTemplateWorker.Bases;

public abstract class WorkerBase : BackgroundService
{
    private readonly ILogServices _logServices;
    private readonly WorkerConfigurationOptions _workerConfigOptions;
    private readonly WorkerStateService _workerStateService;
    private readonly IAvailabilityServices _availabilityServices;

    private CrontabSchedule _schedule;
    private DateTime _nextRun;

    protected WorkerBase(ILogServices logServices,
                         IOptions<WorkerConfigurationOptions> workerConfigOptions,
                         WorkerStateService workerStateService,
                         IAvailabilityServices availabilityServices)
    {
        _logServices = logServices;
        _workerConfigOptions = workerConfigOptions.Value;
        _workerStateService = workerStateService;
        _availabilityServices = availabilityServices;

        ConfigureExecutionTime();
    }

    private void ConfigureExecutionTime()
    {
        _schedule = CrontabSchedule.Parse(_workerConfigOptions.HorarioDeExecucao, new CrontabSchedule.ParseOptions());
        _nextRun = _schedule.GetNextOccurrence(TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")));
    }

    private async Task CatchWorkerErrorsAsync(Exception? ex, CancellationToken stoppingToken, string? texto)
    {
        _logServices.WriteStaticMessage(texto);

        if (ex is not null)
        {
            _logServices.LogData.AddException(ex);
            _availabilityServices.TrackErrorEvent(ex);
        }
        else
        {
            _logServices.WriteStaticMessage("Aparecimento de Exception silenciosa");
        }

        _logServices.WriteLogWhenRaiseExceptions();

        await _availabilityServices.FlushAsync(stoppingToken);

        _workerStateService.IsRunning = false;

        await StopAsync(stoppingToken);
    }

    /// <summary>
    /// Work method run based on <see cref="IWorkerOptions.RepeatIntervalSeconds"/>. Exceptions
    /// thrown here are turned into alerts.
    /// </summary>
    public abstract Task DoWorkAsync();

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We catch anything and alert instead of rethrowing")]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Awaiting Task.Yield() transitions to asyncronous operation immediatly.
        // This allows startup to continue without waiting.
        await Task.Yield();

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _availabilityServices.StartOperation();

                var dataAtual = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

                _workerStateService.IsRunning = true;

                if (dataAtual > _nextRun)
                {
                    await DoWorkAsync().ConfigureAwait(false);

                    _nextRun = _schedule.GetNextOccurrence(TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")));
                }
            }

            _logServices.WriteStaticMessage($"Execução finalizada. Cancelation token cancelado = {stoppingToken.IsCancellationRequested}");
        }
        catch (Exception ex) when (stoppingToken.IsCancellationRequested)
        {
            await CatchWorkerErrorsAsync(ex, stoppingToken, "Execução Cancelada - TOKEN");
        }
        catch (Exception ex) when (ex is null)
        {
            await CatchWorkerErrorsAsync(ex, stoppingToken, "Execução  Parada");
        }
        catch (Exception ex)
        {
            await CatchWorkerErrorsAsync(ex, stoppingToken, "Execução  Parada");
        }
    }
}
