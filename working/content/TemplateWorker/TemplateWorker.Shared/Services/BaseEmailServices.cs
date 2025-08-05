namespace VesteTemplateWorker.Extensions.Shared.Services;

public abstract class BaseEmailServices
{
    EmailConfigurationOptions _options;
    protected BaseEmailServices(IOptionsMonitor<EmailConfigurationOptions> options)
    {
        _options = options.CurrentValue;
    }

    private void VerificarDestinatarios(string destinatarios, MailMessage mailMessage)
    {
        var destinatariosSeparados = _options.Destinatarios.Split(";");

        foreach (var destinatarioSeparado in destinatariosSeparados)
        {
            mailMessage.Bcc.Add(destinatarioSeparado);
        }
    }

    public SmtpClient CriarClienteSmtpPadrao()
    {
        SmtpClient client = new SmtpClient(_options.SMTP, _options.Porta);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_options.Remetente, _options.Senha);

        return client;
    }

    public MailMessage CriarMensagem(EmailInfo emailInfo)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_options.Remetente, emailInfo.Titulo);

        VerificarDestinatarios(_options.Destinatarios, mailMessage);

        mailMessage.Subject = emailInfo.Assunto;
        mailMessage.Body = emailInfo.Conteudo;
        mailMessage.IsBodyHtml = true;
        mailMessage.Priority = MailPriority.High;

        return mailMessage;
    }
}
