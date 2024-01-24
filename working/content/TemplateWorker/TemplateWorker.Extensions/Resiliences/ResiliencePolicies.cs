namespace VesteTemplateWorker.Extensions.Resiliences;

public static class ResiliencePolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetApiRetryPolicy(IServiceProvider serviceProvider, int quantidadeDeRetentativas)
    {
        var quantidadeTotalDeRetentativas = quantidadeDeRetentativas;

        const string _retryMessage = "Retentativas de chamadas externas foram excedidas.";

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode != HttpStatusCode.OK)
            .RetryAsync(quantidadeDeRetentativas, onRetry: (message, numeroDeRetentativas) =>
          {
              if (quantidadeTotalDeRetentativas == numeroDeRetentativas)
              {
                  if (message.Result is not null)
                  {
                      var logServices = serviceProvider.GetService<ILogServices>();
                      logServices.LogData.AddMessageInformation(_retryMessage)
                                         .AddResponseStatusCode((int)message.Result.StatusCode)
                                         .AddRequestUrl(message.Result.RequestMessage.RequestUri.AbsoluteUri)
                                         .AddContractBody(message.Result.RequestMessage.Content)
                                         .AddResponseBody(message.Result.Content)
                                         .AddTimestamp();

                      logServices.WriteLogFromResiliences();
                  }
              }
          });
    }
}
