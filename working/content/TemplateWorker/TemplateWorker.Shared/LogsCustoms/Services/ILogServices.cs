namespace VesteTemplateWorker.Shared.LogsCustoms.Services;

public interface ILogServices
{
    public LogData LogData { get; set; }
    void WriteLog();
    void WriteErrorLog();
    void CreateStructuredLog(LogData logData);
    void WriteLogWhenRaiseExceptions();
    void WriteMessage(string? mensagem);
    void WriteStaticMessage(string? mensagem);
    void WriteLogFromResiliences();
    void WriteContainerLog(string mensagem);
    void WriteContainerErrorLog(string mensagem);
}