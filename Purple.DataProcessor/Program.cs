using Azure.Messaging.ServiceBus;
using Purple.DataProcessor.DxTrade;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(_ => new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"), new ServiceBusClientOptions { TransportType = ServiceBusTransportType.AmqpWebSockets }));
builder.Services.AddHostedService<DxTradeOrderProcessor>(x => new DxTradeOrderProcessor(x.GetRequiredService<ServiceBusClient>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
