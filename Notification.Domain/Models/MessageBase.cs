namespace Notification.Domain.Models;
public abstract record MessageBase
{
    public MessageBase()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
}
