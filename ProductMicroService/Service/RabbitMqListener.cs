using Contracts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class ProductStatusConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ProductStatusConsumerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await DeclareQueues(channel);

        var activationConsumer = CreateConsumer(channel, ProcessActivationMessage);
        var deactivationConsumer = CreateConsumer(channel, ProcessDeactivationMessage);

        await channel.BasicConsumeAsync("user.activation", false, activationConsumer);
        await channel.BasicConsumeAsync("user.deactivation", false, deactivationConsumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task DeclareQueues(IChannel channel)
    {
        await channel.QueueDeclareAsync("user.activation", durable: true, exclusive: false, autoDelete: false);
        await channel.QueueDeclareAsync("user.deactivation", durable: true, exclusive: false, autoDelete: false);
    }

    private AsyncEventingBasicConsumer CreateConsumer(IChannel channel, Func<IServiceProvider, string, Task> processor)
    {
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                await processor(scope.ServiceProvider, message);
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch
            {
                await channel.BasicNackAsync(ea.DeliveryTag, false, true);
            }
        };
        return consumer;
    }

    private async Task ProcessActivationMessage(IServiceProvider serviceProvider, string message)
    {
        var repoManager = serviceProvider.GetRequiredService<IRepositoryManager>();
        var userId = Guid.Parse(message);

        await UpdateProductsStatus(repoManager, userId, true);
    }

    private async Task ProcessDeactivationMessage(IServiceProvider serviceProvider, string message)
    {
        var repoManager = serviceProvider.GetRequiredService<IRepositoryManager>();
        var userId = Guid.Parse(message);

        await UpdateProductsStatus(repoManager, userId, false);
    }

    private async Task UpdateProductsStatus(IRepositoryManager repositoryManager, Guid userId, bool isActive)
    {
        var products = repositoryManager.Product.GetProductsForChangedAvailable(userId, trackChanges: true);

        if (products is null)
            return;
        
        foreach (var product in products)
        {
            product.IsAvailable = isActive;
            product.UpdatedAt = DateTime.UtcNow;
        }

        await repositoryManager.SaveAsync();
    }
}