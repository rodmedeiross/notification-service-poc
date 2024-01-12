using Microsoft.AspNetCore.SignalR.Client;
using Notification.Domain.Models;
using System.Text.Json;

const string id = "ec7fba44-624f-4bfb-a5b7-4096e2c067f9";


var connection = new HubConnectionBuilder()
         .WithUrl($"http://localhost:5074/notification")
         .Build();

connection.On<TransactionMessage>("ReceiveMessage", message =>
{
    Console.WriteLine($"Received message: {JsonSerializer.Serialize(message)}");
});


await connection.StartAsync();

await connection.SendAsync("JoinTransactionEvent", id);

Console.WriteLine($"Connected to SignalR Hub - Group: {id}");

Console.ReadLine();

await connection.StopAsync();

Console.WriteLine("Disconnected from SignalR hub.");