namespace VesteTemplateWorker.Shared.Notifications;

public class NotificationServices : INotificationServices
{

    private readonly ICollection<Notification> _notificacoes;

    public NotificationServices()
    {
        _notificacoes = new List<Notification>();
    }

    public void AddNotification(Notification notificacao) => _notificacoes.Add(notificacao);

    public void AddNotifications(IEnumerable<Notification> notificacoes)
    {
        foreach (var notificacao in notificacoes)
        {
            _notificacoes.Add(notificacao);
        }
    }

    public bool HasNotifications() => GetNotifications().Any();

    public IEnumerable<Notification> GetNotifications() => _notificacoes;

    public void ClearNotifications()
    {
        _notificacoes.Clear();
    }
}
