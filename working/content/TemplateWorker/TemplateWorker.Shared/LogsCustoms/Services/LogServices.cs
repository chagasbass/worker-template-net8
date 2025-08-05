namespace VesteTemplateWorker.Shared.LogsCustoms.Services;

public class LogServices : ILogServices
{
    public LogData LogData { get; set; }
    BaseConfigurationOptions _options { get; set; }

    private readonly ILogger _logger = Log.ForContext<LogServices>();

    public LogServices(IOptionsMonitor<BaseConfigurationOptions> options)
    {
        LogData = new LogData();
        _options = options.CurrentValue;
    }

    public void WriteLog()
    {
        if (_options.HabilitarMensagensDeLog)
        {
            _logger.Information("[LogRequisição]:{mensagem} [ContractData]:{@ContractData} [RequestData]:{@RequestData} [ResponseData]:{@ResponseData}" +
           "[Timestamp]:{Timestamp}", LogData.Mensagem, LogData.ContractData, LogData.RequestData, LogData.ResponseData, LogData.Timestamp);
        }

        LogData.ClearLogData();
    }

    public void WriteLogWhenRaiseExceptions()
    {
        if (LogData is not null && LogData.Exception is not null)
        {
            _logger.Error("[ExceptionType]:{@Exception}" +
                "[ExceptionMessage]:{@Message}" + "[StackTrace]:{@StackTrace}",
                LogData.Exception.GetType().Name, LogData.Exception.Message, LogData.Exception.StackTrace);

            if (LogData?.Exception?.InnerException is not null)
            {
                _logger.Error("[InnerException]:{InnerException}", LogData.Exception?.InnerException?.Message);
            }

            LogData.ClearLogExceptionData();
        }
    }

    public void CreateStructuredLog(LogData logData) => LogData = logData;

    public void WriteMessage(string? mensagem)
    {
        if (_options.HabilitarMensagensDeLog)
        {
            _logger.Information($"{mensagem}");
        }
    }

    public void WriteStaticMessage(string? mensagem) => _logger.Information($"{mensagem}");

    public void WriteErrorLog()
    {
        var mensagem = "Requisição efetuada";
        _logger.Error("[LogRequisição]:{mensagem} [RequestData]:{@RequestData} " +
            "[Method]:{RequestMethod} [Path]:{RequestUri} [RequestTraceId]:{TraceId} " +
            "[ResponseData]:{@ResponseData} [ResponseStatusCode]:{@ResponseStatusCode}",
            mensagem, LogData.RequestData, LogData.RequestMethod, LogData.RequestUri,
            LogData.TraceId, LogData.ResponseData, LogData.ResponseStatusCode);

        LogData.ClearLogData();
    }

    public void WriteLogFromResiliences()
    {
        if (_options.HabilitarMensagensDeLog)
        {
            _logger.Information("[LogRequisição]:{mensagem} [RequestUri]:{RequestUri} [ResponseStatusCode]:{ResponseStatusCode} [ContractData]:{@ContractData} [RequestData]:{@RequestData} [ResponseData]:{@ResponseData}" +
             "[Timestamp]:{Timestamp}", LogData.Mensagem, LogData.RequestUri, LogData.ResponseStatusCode, LogData.ContractData, LogData.RequestData, LogData.ResponseData, LogData.Timestamp);
        }

        LogData.ClearLogData();
    }

    public void WriteContainerLog(string mensagem) => Console.WriteLine($"[{DateTimeExtensions.GetGmtDateTime():HH:mm:ss} INF] {mensagem}");

    public void WriteContainerErrorLog(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{DateTimeExtensions.GetGmtDateTime():HH:mm:ss} INF] {mensagem}");
    }
}