using Microsoft.AspNetCore.SignalR;

namespace Notification.Services.Hubs;

public class NotificationHub : Hub
{
    public async Task JoinTransactionEvent(string transactionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, transactionId);
    }

    public async Task LeaveTransactionEvent(string transactionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, transactionId);
    }
}
