using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Purple.Model;

namespace Purple.Watchdog.Deals;

public class DealProcessor(IServiceProvider provider, ServiceBusClient client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var processor = CreateProcessor();
        processor.ProcessMessageAsync += OnMessageReceived;
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
    
    private ServiceBusProcessor CreateProcessor()
    {
        const string queueName = "dealcreated"; 
        
        return client.CreateProcessor(queueName,
            new ServiceBusProcessorOptions { AutoCompleteMessages = false });
    }

    private async Task OnMessageReceived(ProcessMessageEventArgs eventArgs)
    {
        var scope = provider.CreateScope();
        var watchdogService = scope.ServiceProvider.GetRequiredService<IWatchdogService>();
        var deal = JsonSerializer.Deserialize<Deal>(eventArgs.Message.Body.ToString());
        await watchdogService.ValidateOrderAsync(deal!);
    }
}