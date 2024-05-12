using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace Purple.DataCollector.DxTrade;

public class DxTradeClientBackgroundTask(DxTradeClient dxTradeClient, ServiceBusClient client) : BackgroundService
{
    private ServiceBusSender sender = null!;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        CreateSender();

        dxTradeClient.OnMessageReceived += async (_, s) => await PublishAsync(s);
        
        await dxTradeClient.ConnectAsync();
        await dxTradeClient.ReceiveMessagesAsync();
    }

    private void CreateSender()
    {
        sender = client.CreateSender("dxtradeordercreated");
    }
    
    private async Task PublishAsync(string @event)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType());
        
        var message = new ServiceBusMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Body = new BinaryData(body),
            Subject = "dxtradeordercreated",
        };
        
        await sender.SendMessageAsync(message);
    }
}