using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Notification.Domain.Models;
using Notification.Services.Hubs;

namespace Notification.Api.Services
{
  public class NotificationConsumer : IConsumer<TransactionMessage>
  {
    private readonly ILogger<NotificationConsumer> _logger;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationConsumer(ILogger<NotificationConsumer> logger, IHubContext<NotificationHub> hubContext)
    {
      _logger = logger;
      _hubContext = hubContext;
    }

    public Task Consume(ConsumeContext<TransactionMessage> context)
    {
      _logger.LogInformation("Message received: {Message}", context.Message.Name);
      //var id = "ec7fba44-624f-4bfb-a5b7-4096e2c067f9";
      _hubContext.Clients.Group(context.Message.Id.ToString()).SendAsync("ReceiveMessage", context.Message);
      return Task.CompletedTask;
    }
  }
}
