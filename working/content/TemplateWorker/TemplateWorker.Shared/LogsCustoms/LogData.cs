namespace VesteTemplateWorker.Shared.LogsCustoms;

/// <summary>
/// Representa o log estruturado.Pode ser alterado de acordo com a implementação
/// </summary>
public class LogData
{
    public DateTime Timestamp { get; private set; }
    public object RequestData { get; private set; }
    public object ContractData { get; private set; }
    public string? RequestMethod { get; private set; }
    public string? RequestUri { get; private set; }
    public object ResponseData { get; private set; }
    public int ResponseStatusCode { get; private set; }
    public string? TraceId { get; private set; }
    public Exception Exception { get; private set; }
    public string? Mensagem { get; private set; }

    public LogData()
    {
        Timestamp = DateTime.Now;
    }

    public LogData AddTimestamp()
    {
        Timestamp = DateTimeExtensions.GetGmtDateTime();
        return this;
    }

    public LogData AddMessageInformation(string? mensagem)
    {
        Mensagem = mensagem;
        return this;
    }

    public LogData AddResponseStatusCode(int codigo)
    {
        ResponseStatusCode = codigo;
        return this;
    }

    public LogData AddRequestType(string metodo)
    {
        RequestMethod = metodo;
        return this;
    }

    public LogData AddRequestUrl(string? url)
    {
        RequestUri = url;
        return this;
    }

    public LogData AddTraceIdentifier(string? indentificador)
    {
        TraceId = indentificador;
        return this;
    }

    public LogData AddRequestBody(object requestData)
    {
        if (requestData is null)
            RequestData = "No Request Data";
        else
            RequestData = requestData;

        return this;
    }

    public LogData AddRequestBody(string requestData)
    {
        if (string.IsNullOrEmpty((string)requestData))
            RequestData = "No Request Data";
        else
            RequestData = requestData;

        return this;
    }

    public LogData AddContractBody(object contractData)
    {
        if (contractData is null)
            ContractData = "No Contract Data";
        else
            ContractData = contractData;

        return this;
    }

    public LogData AddContractBody(string contractData)
    {
        if (string.IsNullOrEmpty((string)contractData))
            ContractData = "No Request Data";
        else
            ContractData = contractData;

        return this;
    }

    public LogData AddResponseBody(object responseData)
    {
        ResponseData = responseData;

        return this;
    }

    public LogData AddException(Exception exception)
    {
        Exception = exception;
        return this;
    }

    public LogData ClearLogData()
    {
        Timestamp = DateTimeExtensions.GetGmtDateTime();
        RequestData = string.Empty;
        RequestMethod = string.Empty;
        RequestUri = string.Empty;
        ResponseData = string.Empty;
        ResponseStatusCode = 0;
        TraceId = string.Empty;

        return this;
    }

    public LogData ClearLogExceptionData()
    {
        Exception = default;

        return this;
    }
}
