using System.Net.WebSockets;

namespace Purple.DataCollector.DxTrade;

public class DxTradeClient
{
    private readonly ClientWebSocket client;
    private readonly Uri serverUri;
    
    public event EventHandler<string> OnMessageReceived;

    public DxTradeClient(string serverUrl)
    {
        client = new ClientWebSocket();
        serverUri = new Uri(serverUrl);
    }
    
    public async Task ConnectAsync()
    {
        const string initMessage = @"{
    ""type"": ""AccountPortfoliosSubscriptionRequest"",
    ""requestId"": ""1"",
    ""session"": ""12345"",
    ""timestamp"": ""2022-07-27T14:50:30.054Z"",
    ""payload"": {
        ""requestType"": ""LIST"",
        ""accounts"": [""default:demo1"", ""default:demo2""]
    }
}";
        await SendAsync(initMessage);
    }

    private Task SendAsync(string message)
    {
        return Task.CompletedTask;
    }
    
    public Task ReceiveMessagesAsync()
    {
        const string message = @"{
  ""account"": ""ABC123"",
  ""orderId"": 123456,
  ""orderCode"": ""ORD-789"",
  ""version"": 2,
  ""clientOrderId"": ""CLIENT-987"",
  ""actionCode"": ""MODIFY-001"",
  ""legCount"": 1,
  ""type"": ""LIMIT"",
  ""instrument"": ""AAPL"",
  ""status"": ""OPEN"",
  ""finalStatus"": false,
  ""legs"": [
    {
      ""side"": ""BUY"",
      ""tif"": ""GTC""
    }
  ],
  ""priceOffset"": 1.5,
  ""priceLink"": ""MARKET"",
  ""expireDate"": ""2024-06-01T12:00:00Z"",
  ""marginRate"": 0.03,
  ""issueTime"": ""2024-05-07T08:30:00Z"",
  ""transactionTime"": ""2024-05-07T10:15:00Z"",
  ""links"": [
    {
      ""relatedOrderId"": 987654,
      ""relationType"": ""HEDGE""
    }
  ],
  ""executions"": [
    {
      ""executionId"": ""EXEC-001"",
      ""quantity"": 100,
      ""price"": 150.25,
      ""timestamp"": ""2024-05-07T10:20:00Z""
    }
  ],
  ""cashTransactions"": [
    {
      ""transactionId"": ""CASH-001"",
      ""amount"": 5000,
      ""currency"": ""USD"",
      ""timestamp"": ""2024-05-07T08:35:00Z""
    }
  ],
  ""hedgedOrderId"": 654321,
  ""externalOrderId"": ""EXT-001""
}";
        
        while (true)
        {
            Thread.Sleep(2000);
            OnMessageReceived?.Invoke(this, message);
        }
    }
}