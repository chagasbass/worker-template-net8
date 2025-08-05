namespace VesteTemplateWorker.Extensions.Shared.Services;
public interface IEmailServices
{
    Task EnviarEmailAsync(EmailInfo emailInfo);
}
