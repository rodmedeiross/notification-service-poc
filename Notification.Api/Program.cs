using MassTransit;
using Notification.Api.Extensions;
using Notification.Domain.Models;
using Notification.Services.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMassTransitService(builder.Configuration)
    .AddSignalR(opt =>
    {
        opt.EnableDetailedErrors = true;
        opt.KeepAliveInterval = TimeSpan.FromSeconds(30);
    });

builder.Host.UseSerilog((ctx, conf) =>
{
    conf
        .ReadFrom
            .Configuration(ctx.Configuration)
        .Enrich
            .FromLogContext();
});

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notification");
});

app.MapPost("/api/send", (TransactionMessage message, IBus bus) =>
{
    bus.Publish(message);
    return Results.Ok("Message Sent");
});

app.MapPost("/api/send-certification", (TransactionMessage message, IBus bus) =>
{
    var endpoint = bus.GetSendEndpoint(new Uri("exchange:Certification")).Result;
    endpoint.Send(new CertificationMessage(Guid.NewGuid(), message.Name));
    return Results.Ok("Message Sent");
});

app.Run();
