using RabbitMQ.Client;
using Service.Contract;
using System.Text;
using System.Text.Json;

public class RabbitMqService : IRabbitMqService
{

    public async Task SendMessage(object obj, string queue)
    {
        var message = JsonSerializer.Serialize(obj);
        await SendMessage(message, queue);
    }

    public async Task SendMessage(string message, string queue)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message!.ToString()!);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);
    }
}