using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Purple.Model;

namespace Purple.DataProcessor.DxTrade;

public class DxTradeOrderProcessor(ServiceBusClient client) : BackgroundService
{
    private readonly ServiceBusSender sender = client.CreateSender("dealcreated");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        var processor = CreateProcessor();
        processor.ProcessMessageAsync += OnMessageReceived;
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private ServiceBusProcessor CreateProcessor()
    {
        const string queueName = "dxtradeordercreated"; 
        
        return client.CreateProcessor(queueName,
            new ServiceBusProcessorOptions { AutoCompleteMessages = false });
    }

    private async Task OnMessageReceived(ProcessMessageEventArgs eventArgs)
    {
        Console.WriteLine("Parsing message...");
        var sampleDeal = new Deal();
        
        var body = JsonSerializer.SerializeToUtf8Bytes(sampleDeal, sampleDeal.GetType());
        
        var dealCreatedMessage = new ServiceBusMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Body = new BinaryData(body),
            Subject = "newdeal",
        };
        
        Console.WriteLine("Inserting into database...");

        await sender.SendMessageAsync(dealCreatedMessage);
    }
}