using Azure.Messaging.ServiceBus;

namespace Purple.DataProcessor.DxTrade;

public class DxTradeOrderProcessor(ServiceBusClient client) : BackgroundService
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
        const string topicName = ""; 
        const string subscriptionClientName = "";
        
        return client.CreateProcessor(topicName, subscriptionClientName,
            new ServiceBusProcessorOptions { AutoCompleteMessages = false });
    }

    private async Task OnMessageReceived(ProcessMessageEventArgs eventArgs)
    {
        
    }
}