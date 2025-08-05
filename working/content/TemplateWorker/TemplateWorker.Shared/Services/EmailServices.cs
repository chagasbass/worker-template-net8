namespace VesteTemplateWorker.Extensions.Shared.Services;

public class EmailServices(ILogServices logServices, IOptionsMonitor<EmailConfigurationOptions> options) : BaseEmailServices(options), IEmailServices
{
    public async Task EnviarEmailAsync(EmailInfo emailInfo)
    {
        using var cliente = CriarClienteSmtpPadrao();

        try
        {
            using var mailMessage = CriarMensagem(emailInfo);

            await cliente.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            logServices.LogData.AddException(ex);

            logServices.WriteLogWhenRaiseExceptions();
        }
    }
}
