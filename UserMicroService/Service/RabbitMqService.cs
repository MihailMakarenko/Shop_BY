using RabbitMQ.Client;
using Service.Contract;
using System.Text;
using System.Text.Json;

public class RabbitMqService : IRabbitMqService
{

    public async Task SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        await SendMessage(message);
    }

    public async Task SendMessage(string message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "message", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message!.ToString()!);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "message", body: body);
    }
}