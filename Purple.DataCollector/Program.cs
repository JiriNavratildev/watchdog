using Azure.Messaging.ServiceBus;
using Purple.DataCollector.DxTrade;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(_ => new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"), new ServiceBusClientOptions { TransportType = ServiceBusTransportType.AmqpWebSockets }));

var dxTradeServers = builder.Configuration.GetSection("DxTradeServers").Get<string[]>() ?? [];
foreach (var dxTradeServer in dxTradeServers)
{
    builder.Services.AddSingleton<IHostedService>(x => new DxTradeClientBackgroundTask(new DxTradeClient(dxTradeServer), x.GetRequiredService<ServiceBusClient>()));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
