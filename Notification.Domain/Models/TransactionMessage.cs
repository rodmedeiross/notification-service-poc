namespace Notification.Domain.Models;

public record TransactionMessage: MessageBase
{
    public string Name { get; set; } = null!;
    public int Status { get; set; }
}
