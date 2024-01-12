using MassTransit;
using Notification.Api.Services;

namespace Notification.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<NotificationConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"]);

                cfg.ReceiveEndpoint(configuration["RabbitMq:Queue"]!, e => 
                {
                    e.ConfigureConsumer<NotificationConsumer>(context);
                });
            });
        });

        return services;
    }
}
