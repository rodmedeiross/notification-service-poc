using Notification.Api.Services;
using Notification.Domain.Models;
using Notification.Services.Hubs;

namespace Notification.Tests.Consumers
{
    public class NotificationMessageConsumerTests
    {
        private readonly NotificationConsumer _consumer;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly ConsumeContext<TransactionMessage> _consumerContext;
        const string id = "ec7fba44-624f-4bfb-a5b7-4096e2c067f9";


        public NotificationMessageConsumerTests()
        {
            _hubContext = Substitute.For<IHubContext<NotificationHub>>();
            _logger = Substitute.For<ILogger<NotificationConsumer>>();
            _consumer = new NotificationConsumer(_logger, _hubContext);
            _consumerContext = Substitute.For<ConsumeContext<TransactionMessage>>();
        }

        [Fact]
        public async Task ConsumerWithValidMessage_ShouldSendNotificationToGroup()
        {
            // Arrange
            var transactionMessage = new TransactionMessage { Id = new Guid(id), Name = "Test Transaction" };
            _consumerContext.Message.Returns(transactionMessage);

            // Act
            await _consumer.Consume(_consumerContext);

            // Assert
            _hubContext.Clients.Group(id).ReceivedCalls().Should().HaveCount(1);
        }


        [Fact]
        public async Task ConsumerWithValidMessage_IdTransactionNotEqual_ShouldNotReceiveInGroup()
        {
            // Arrange
            var transactionMessage = new TransactionMessage { Id = Guid.NewGuid(), Name = "Test Transaction" };
            _consumerContext.Message.Returns(transactionMessage);

            // Act
            await _consumer.Consume(_consumerContext);

            // Assert
            _hubContext.Clients.Group(id).ReceivedCalls().Should().NotHaveCount(1);
        }
    }
}
