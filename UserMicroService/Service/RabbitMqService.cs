using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Service.Contract;
using System.Text;
using System.Text.Json;

public class RabbitMqService : IRabbitMqService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessage(object obj, string queue)
    {
        var message = JsonSerializer.Serialize(obj);
        await SendMessage(message, queue);
    }

    public async Task SendMessage(string message, string queue)
    {
        var rabbitSection = _configuration.GetSection($"RabbitMq");

        var factory = new ConnectionFactory
        {
            HostName = rabbitSection["Host"]!,
            Port = int.Parse(rabbitSection["Port"]!),
            UserName = rabbitSection["UserName"]!,
            Password = rabbitSection["Password"]!
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message!.ToString()!);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);
    }
}